using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mukicik
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /// REGION AUTHORIZED 
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }
            RemoveActiveUserCounter();
            // Log the user out
            FormsAuthentication.SignOut();
            // Redirect to the login page
            Response.Redirect("Login.aspx");
        }

        private void RemoveActiveUserCounter()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    int activeUsersDefault = -1;
                    using (NpgsqlCommand cmdRetrieveCounter = new NpgsqlCommand("SELECT * FROM active_users", connection))
                    {
                        using (NpgsqlDataReader reader = cmdRetrieveCounter.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                activeUsersDefault += int.Parse(reader["counter"].ToString());
                            }
                        }
                    }
                    using (NpgsqlCommand cmdCounter = new NpgsqlCommand())
                    {
                        cmdCounter.Connection = connection;
                        cmdCounter.CommandText = "INSERT INTO active_users VALUES (1, @counter)";
                        cmdCounter.Parameters.AddWithValue("@counter", activeUsersDefault);
                    }
                }
                catch (Exception ex)
                {
                    LabelErrorMessage.Text = $"Error: {ex.Message}";
                    LabelErrorMessage.Visible = true;
                }
            }
        }
    }
}