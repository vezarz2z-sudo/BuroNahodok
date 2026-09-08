using Microsoft.Data.SqlClient;
using System.Data;

namespace BuroNahodok
{
    public static class Database
    {
        private static readonly string connectionString =
            "Server=.\\SQLEXPRESS;" +
            "Database=BuroNahodok;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;" +
            "MultipleActiveResultSets=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using SqlConnection connection = GetConnection();
                connection.Open();

                return connection.State == ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckUser(
            string login,
            string password,
            out string role)
        {
            role = string.Empty;

            try
            {
                using SqlConnection connection = GetConnection();
                connection.Open();

                string query = @"
                    SELECT Role
                    FROM Users
                    WHERE Login = @Login
                      AND PasswordHash = @Password
                      AND IsActive = 1";

                using SqlCommand checkUserCommand =
                    new SqlCommand(query, connection);

                checkUserCommand.Parameters.AddWithValue(
                    "@Login",
                    login);

                checkUserCommand.Parameters.AddWithValue(
                    "@Password",
                    password);

                object? result = checkUserCommand.ExecuteScalar();

                if (result != null)
                {
                    role = result.ToString() ?? string.Empty;
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}