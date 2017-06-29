using System;
using Npgsql;

namespace NpgSqlRx.Core
{
    public class Connection : IDisposable
    {
        private readonly NpgsqlConnection _connection;
 
        public Connection(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            
        }

        public NpgsqlConnection Get()
        {
            return _connection;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
