using System.Data.SqlClient;
using MySqlConnector;

namespace HotelManagementApp.DAL
{
    public class DatabaseConnect
    {
        private static readonly string _connectionString = @"Server=LAPTOP-F4JF2AS7\SQLEXPRESS;Database=HotelDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}