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
            /// REGION AUTHORIZED 
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            } else
            {
                LiteralUserName.Text = User.Identity.Name;
            }
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
                PriceRatingValidator();
                // Save product picture
                string pictureFileName = SavePicture();

                // Insert product into database
                // AWAL MULA KONEKSI DATABASE
                ProductRepository productRepository = new ProductRepository("PostgresConnection");
                try {
                    int id = productRepository.GetLastProductId();
                    Product product = ModelFactory.CreateProduct(id, name, price, pictureFileName, rating);
                    productRepository.AddProduct(product);
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", "alert('Product inserted successfully!');", true);
                } catch (Exception ex) {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('{ex.Message}');", true);
                }
            }
            else
            {
                // Validation failed, stay on the same page to display error messages
            }
        }

        private void PriceRatingValidator(){
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