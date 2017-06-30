using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Npgsql;

namespace NpgSqlRx.Core
{
    public static class Operation
    {
        public static IObservable<T> QueryToObservable<T>(this string query, string connectionString) where T : new()
        {
            var result = Observable.Create<T>(observer =>
            {
                Connection connection = null;
                try
                {
                    connection = new Connection(connectionString);
                    var props = typeof (T).GetProperties();
                    using (var cmd = new NpgsqlCommand(query, connection.Get()))
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
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
                return Disposable.Create(() =>
                {
                    if (connection != null) connection.Dispose();
                });
            });
            
            return result;
        }
    }
}
