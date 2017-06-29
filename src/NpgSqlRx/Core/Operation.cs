using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Npgsql;

namespace NpgSqlRx.Core
{
    public class Operation
    {
        private readonly Connection _connection;

        public Operation(Connection connection)
        {
            _connection = connection;
        }

        public IObservable<T> Read<T>(string query) where T : new()
        {
            var result = Observable.Create<T>(observer =>
            {
                var props = typeof(T).GetProperties();
                using (var cmd = new NpgsqlCommand(query, _connection.Get()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new T();
                            foreach (var propertyInfo in props)
                            {
                                var col = reader[propertyInfo.Name];
                                propertyInfo.SetValue(data, Convert.ChangeType(col, propertyInfo.PropertyType), null);
                            }
                            observer.OnNext(data);
                        }
                        observer.OnCompleted();
                    }
                }
                return Disposable.Create(() =>
                {
                    _connection.Dispose();
                });
            });
            
            return result;
        }
    }
}
