using Mukicik.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;

namespace Mukicik.Repositories
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionStringKeys)
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringKeys].ConnectionString;
        }

        public void AddUser(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO users VALUES (@id, @username, @email, @password, @gender, @dob);", connection))
                    {
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@gender", user.Gender);
                        cmd.Parameters.AddWithValue("@dob", user.DOB);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Throw error
                    ex.Data.Add("UserRepository", "Failed to register new user. Error: {ex.Message}");
                    throw;
                }
            }
        }

        public int GetLastUserId()
        {
            try
            {
                string id;
                int intId;
                int defaultId = 1;

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    /// BUKA KONEKSI KE DATABASE
                    connection.Open();

                    /// AMBIL DATA ID TERAKHIR TERBARU
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT id FROM users ORDER BY id DESC LIMIT 1;", connection))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = reader["id"].ToString();
                                int.TryParse(id, out intId);
                                defaultId += intId;
                            }
                        }
                    }
                }

                return defaultId;
            }
            catch (Exception ex)
            {
                // Throw error
                ex.Data.Add("UserRepository", "Failed to get last user id. Error: {ex.Message}");
                throw;
            }
        }

        public string GetUserByEmailAndPassword(string email, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM users WHERE email = @Email AND password = @Password;", connection))
                    {
                        cmd.Parameters.AddWithValue("Email", email);
                        cmd.Parameters.AddWithValue("Password", password);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows) /// KALO ADA DATA YANG KETEMU
                            {

                                reader.Read();
                                string userName = reader["username"].ToString();
                                return userName;
                            }
                            else
                            {
                                // Throw error
                                Exception exception = new Exception();
                                exception.Data.Add("UserRepository", "Failed to login. Error: {ex.Message}");
                                throw exception;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int GetLoggedInUserCount()
        {
            // This would typically be implemented using session management and stored in the application's state
            // For simplicity, we'll assume a method that retrieves this count
            return HttpContext.Current.Application["LoggedInUserCount"] != null ? (int)HttpContext.Current.Application["LoggedInUserCount"] : 0;
        }

        public void IncreaseLoggedInUserCount()
        {
            HttpContext.Current.Application["LoggedInUserCount"] = GetLoggedInUserCount() + 1;
        }

        public void DecreaseLoggedInUserCount()
        {
            HttpContext.Current.Application["LoggedInUserCount"] = GetLoggedInUserCount() - 1;
        }
    }
}