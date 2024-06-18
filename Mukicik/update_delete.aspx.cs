using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mukicik
{
    public partial class update_delete : System.Web.UI.Page
    {   
        /// AKAN DIPANGGIL PERTAMA KALI KETIKA PAGE/HALAMAN DIAKSES
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /// REGION AUTHORIZED 
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    LiteralUserName.Text = User.Identity.Name;
                }
                GridView1.DataSource = GetProducts();
                GridView1.DataBind();
            }
        }

        private DataTable GetProducts()
        {
            DataTable dt = new DataTable();

            ///BUKA KONEKSI KE DATABASE
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

            using (NpgsqlConnection connection = new NpgsqlConnection(connString))
            {
                try
                {   
                    /// OPEN CONNECTION
                    connection.Open();
                    /// QUERY             id NAMANYA DIUBAH JADI ProductId
                    string query = "SELECT id AS ProductId, name AS ProductName, price AS ProductPrice, image AS ProductImage, rating AS ProductRating FROM products";
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log error, show message)
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('Failed to retrieve products. Error: {ex.Message}');", true);
                }
            }

            return dt;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            ProductId.Text = row.Cells[1].Text;
            ProductName.Text = row.Cells[2].Text;
            ProductPrice.Text = row.Cells[3].Text;
            ProductImage.Text = row.Cells[4].Text;
            ProductRating.Text = row.Cells[5].Text;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Implement deletion from the database
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            DeleteProduct(id);
            GridView1.DataSource = GetProducts();
            GridView1.DataBind();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            // Implement your update logic here
            UpdateProduct(ProductId.Text, ProductName.Text, ProductPrice.Text, ProductImage.Text, ProductRating.Text);
            GridView1.DataSource = GetProducts();
            GridView1.DataBind();
        }

        private void DeleteProduct(string id)
        {   
            // BUKA KONEKSI KE DATABASE
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM products WHERE id = @id";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", int.Parse(id));
                        command.ExecuteNonQuery();
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", "alert('Product deleted successfully!');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('Failed to delete product. Error: {ex.Message}');", true);
                }
            }
        }

        private void UpdateProduct(string id, string name, string price, string image, string rating)
        {   
            // DATABASE DIBUKA
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    /// QUERY DATA KE DATABASE
                    string query = "UPDATE products SET name = @name, price = @price, image = @image, rating = @rating WHERE id = @id";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", int.Parse(id));
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@price", decimal.Parse(price));
                        command.Parameters.AddWithValue("@image", image);
                        command.Parameters.AddWithValue("@rating", decimal.Parse(rating));
                        command.ExecuteNonQuery();
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", "alert('Product updated successfully!');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('Failed to update product. Error: {ex.Message}');", true);
                }
            }
        }
    }
}