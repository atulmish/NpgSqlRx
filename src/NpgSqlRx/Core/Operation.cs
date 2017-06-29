using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Npgsql;

namespace NpgSqlRx.Core
{
    public class Operation<T> where T : new()
    {
        private readonly Connection _connection;

        public Operation(Connection connection)
        {
            _connection = connection;
        }

        public IObservable<T> Read(string query)
        {
            IObserver<T> observer = null;
            var result = Observable.Create<T>(o =>
            {
                observer = o;
                return Disposable.Create(() =>
                {
                    _connection.Dispose();
                });
            });
            var props = typeof (T).GetProperties();
            using (var cmd = new NpgsqlCommand(query, _connection.Get()))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new T();
                        foreach (var propertyInfo in props)
                        {
                            var col = reader. GetOrdinal(propertyInfo.Name);
                            propertyInfo.SetValue(data, Convert.ChangeType(col, propertyInfo.PropertyType), null);
                            observer.OnNext(data);
                        }
                        Console.WriteLine(reader.GetString(0));
                    }
                    observer.OnCompleted();
                }
            }
            return result;
        }
    }
}
