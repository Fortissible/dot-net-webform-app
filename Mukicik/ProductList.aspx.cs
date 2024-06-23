using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mukicik
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

                /// REGION PUBLIC/OPEN
                if (User.Identity.IsAuthenticated)
                {
                    // User is authenticated, show welcome panel
                    AuthDiv.Visible = true;
                    UnauthDiv.Visible = false;
                    LiteralUserName.Text = User.Identity.Name;
                }
                else
                {
                    // User is not authenticated, show login panel
                    AuthDiv.Visible = false;
                    UnauthDiv.Visible = true;
                }
            }
        }

        private void LoadData()
        {   
            StringBuilder html = new StringBuilder();

            ProductRepository productRepository = new ProductRepository("PostgresConnection");
            try {
                List<Product> listProduct = productRepository.GetAllProducts();
                foreach(var product in listProduct)
                {
                    string title = product.Name.ToString();
                    string rating = product.Rating.ToString();
                    string price = product.Price.ToString();
                    string imageUrl = product.Image.ToString();

                    html.Append("<div class=\"product\">");
                    html.Append($"<img src=\"{imageUrl}\" alt=\"{title}\"/>");
                    html.Append($"<div class=\"product-title\">{title}</div>");
                    html.Append($"<div class=\"product-rating\">{rating} ⭐</div>");
                    html.Append($"<div class=\"product-price\">{price}</div>");
                    html.Append("</div>");
                }
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('{ex.Message}');", true);
            }

            ProductContainer.InnerHtml = html.ToString();
        }
    }
}