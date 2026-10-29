using System.Data.SqlClient;
using MySqlConnector;
namespace InventoryManagementApp.DAL
{
    public class DatabaseConnect
    {
        private static readonly string _connectionString = @"Server=LAPTOP-F4JF2AS7\SQLEXPRESS;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}