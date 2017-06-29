using Npgsql;

namespace NpgSqlRx.Core
{
    class Insert
    {
        public Insert(string connectionString)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO data (some_field) VALUES (@p)";
                    cmd.Parameters.AddWithValue("p", "Hello world");
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
