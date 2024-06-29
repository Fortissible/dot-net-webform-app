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
            ProductRepository productRepository = new ProductRepository("PostgresConnection");
            
            try {
                DataTable dt = productRepository.GetAllProductsDataTable();
                return dt;
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('{ex.Message}');", true);
            }
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
            ProductRepository productRepository = new ProductRepository("PostgresConnection");
            try {
                productRepository.DeleteProduct(id);
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", "alert('Product deleted successfully!');", true);
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('{ex.Message}');", true);
            }
        }

        private void UpdateProduct(string id, string name, string price, string image, string rating)
        {   
            Product product = ModelFactory.CreateProduct(id, name, price, image, rating);

            ProductRepository productRepository = new ProductRepository("PostgresConnection");

            try {
                productRepository.UpdateProduct(product);
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", "alert('Product updated successfully!');", true);
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('{ex.Message}');", true);
            }
        }
    }
}