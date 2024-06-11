using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mukicik
{
    public partial class insert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ValidateRating(object source, ServerValidateEventArgs args)
        {
            double rating;
            /// Coba parsing (Mengubah tipe data)
            ///                 // INPUT    // OUTPUT
            if (double.TryParse(args.Value, out rating))
            {
                if (rating >= 0 && rating <= 5)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            // Call Page.Validate() to trigger server-side validation
            if (Page.IsValid)
            {
                // Your form processing logic here, assuming validated data
                // (e.g., register user, store information in database)

                // Example: Accessing validated data
                string name = TextBoxName.Text;
                double rating;
                double price;
                if (double.TryParse(TextBoxPrice.Text, out price))
                {
                    if (price >= 0)
                    {
                        CustomValidatorPrice.IsValid = true;
                    }
                    else
                    {
                        CustomValidatorPrice.IsValid = false;
                        return;
                    }
                }
                else
                {
                    CustomValidatorPrice.IsValid = false;
                    return;
                }

                if (double.TryParse(TextBoxRating.Text, out rating))
                {
                    if (rating >= 0 && rating <= 5)
                    {
                        CustomValidatorRating.IsValid = true;
                    }
                    else
                    {
                        CustomValidatorRating.IsValid = false;
                        return;
                    }
                }
                else
                {
                    CustomValidatorRating.IsValid = false;
                    return;
                }

                // Save product picture
                string pictureFileName = SavePicture();

                // Insert product into database
                // AWAL MULA KONEKSI DATABASE
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
                using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                {
                    try
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

                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "INSERT INTO products (id, name, price, image, rating) VALUES (@id, @name, @price, @image, @rating)";
                            command.Parameters.AddWithValue("@id", defaultId);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@price", price);
                            command.Parameters.AddWithValue("@rating", rating);
                            command.Parameters.AddWithValue("@image", pictureFileName);
                            command.ExecuteNonQuery();


                            // Show success message using JavaScript
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", "alert('Product inserted successfully!');", true);
                            Response.Redirect("./Index.aspx");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Show error message using JavaScript
                        ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('Failed to insert product. Error: {ex.Message}');", true);
                    }
                }
            }
            else
            {
                // Validation failed, stay on the same page to display error messages
            }
        }

        private string SavePicture()
        {
            string pictureFileName = Path.GetFileName(FileUploadProfilePicture.FileName);
            string pictureFilePath = Server.MapPath("~/") + pictureFileName;
            FileUploadProfilePicture.SaveAs(pictureFilePath);
            return pictureFileName;
        }
    }
}