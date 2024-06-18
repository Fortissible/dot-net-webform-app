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
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
            StringBuilder html = new StringBuilder();

            // INTI KONEKSI KE DATABASE
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // BUKA KONEKSI KE DATABASE
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT name, rating, price, image FROM products", conn))
                {
                    // BACA PERINTAH NpgsqlCommand
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        // SELAGI PROSES MEMBACA BERJALAN
                        while (reader.Read())
                        {
                            string title = reader["name"].ToString();
                            string rating = reader["rating"].ToString();
                            string price = string.Format("Rp{0:N0}", reader["price"]);
                            string imageUrl = reader["image"].ToString();

                            html.Append("<div class=\"product\">");
                            html.Append($"<img src=\"{imageUrl}\" alt=\"{title}\"/>");
                            html.Append($"<div class=\"product-title\">{title}</div>");
                            html.Append($"<div class=\"product-rating\">{rating} ⭐</div>");
                            html.Append($"<div class=\"product-price\">{price}</div>");
                            html.Append("</div>");
                        }
                    }
                }
            }

            ProductContainer.InnerHtml = html.ToString();
        }
    }
}