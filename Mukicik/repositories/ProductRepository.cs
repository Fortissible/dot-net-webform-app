using Mukicik.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Mukicik.Repositories
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionStringKeys)
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringKeys].ConnectionString;
        }

        public void AddProduct(Product product)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    /// BUKA KONEKSI KE DATABASE
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO products (id, name, price, image, rating) VALUES (@id, @name, @price, @image, @rating)";
                        command.Parameters.AddWithValue("@id", product.Id);
                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@price", product.Price);
                        command.Parameters.AddWithValue("@rating", product.Rating);
                        command.Parameters.AddWithValue("@image", product.Image);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Throw error
                ex.Data.Add("ProductRepository", "Failed to insert product. Error: {ex.Message}");
                throw;
            }
        }

        public int GetLastProductId()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    string id;
                    int intId;
                    int defaultId = 1;
                    /// BUKA KONEKSI KE DATABASE
                    connection.Open();

                    /// AMBIL DATA ID TERAKHIR TERBARU
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT id FROM products ORDER BY id DESC LIMIT 1;", connection))
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

                    return defaultId;
                }
            }
            catch (Exception ex)
            {
                // Throw error
                ex.Data.Add("ProductRepository", "Failed to get last product id. Error: {ex.Message}");
                throw;
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    // BUKA KONEKSI KE DATABASE
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT id, name, rating, price, image FROM products", conn))
                    {
                        // BACA PERINTAH NpgsqlCommand
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            // SELAGI PROSES MEMBACA BERJALAN
                            while (reader.Read())
                            {
                                Product product = ModelFactory.CreateProduct(
                                        id: Convert.ToInt32(reader["id"]),
                                        name: reader["name"].ToString(),
                                        price: Convert.ToInt32(reader["price"]),
                                        image: reader["image"].ToString(),
                                        rating: Convert.ToSingle(reader["rating"])
                                    );
                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Throw error
                ex.Data.Add("ProductRepository", "Failed to get products. Error: {ex.Message}");
                throw;
            }
            return products;
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    /// QUERY DATA KE DATABASE
                    string query = "UPDATE products SET name = @name, price = @price, image = @image, rating = @rating WHERE id = @id";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", product.Id);
                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@price", product.Price);
                        command.Parameters.AddWithValue("@image", product.Image);
                        command.Parameters.AddWithValue("@rating", product.Rating);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Throw error
                ex.Data.Add("ProductRepository", "Failed to update product. Error: {ex.Message}");
                throw;
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM products WHERE id = @id";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", productId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Throw error
                ex.Data.Add("ProductRepository", "Failed to delete product. Error: {ex.Message}");
                throw;
            }
        }
    }
}