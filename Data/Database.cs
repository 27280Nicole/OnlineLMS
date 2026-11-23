using Npgsql;

namespace OnlineLMS.Data
{
    public class Database
    {
        private static string connectionString =
            "Host=localhost;Port=5433;Database=olms_db;Username=postgres;Password=" + Secrets.DbPassword;

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}