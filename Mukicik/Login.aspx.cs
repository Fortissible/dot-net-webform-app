﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mukicik
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            /// REGION UNAUTHENTICATED
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("index.aspx");
            }

            if (Request.Cookies["RememberMe"] != null)
            {
                TextBoxEmail.Text = Request.Cookies["RememberMe"].Value;
                CheckBoxRememberMe.Checked = true;
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            string email = TextBoxEmail.Text;
            string password = TextBoxPassword.Text;

            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connString))
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

                                // Handle Remember Me checkbox
                                if (CheckBoxRememberMe.Checked)
                                {
                                    HttpCookie rememberMeCookie = new HttpCookie("RememberMe");
                                    rememberMeCookie.Value = email;
                                    rememberMeCookie.Expires = DateTime.Now.AddDays(30); // Set expiration as needed
                                    Response.Cookies.Add(rememberMeCookie);
                                }
                                else
                                {
                                    HttpCookie rememberMeCookie = new HttpCookie("RememberMe");
                                    rememberMeCookie.Expires = DateTime.Now.AddDays(-9999); // Expire the cookie
                                    Response.Cookies.Add(rememberMeCookie);
                                }

                                /// KODE UNTUK MEMBUAT USER SESSION LOGIN
                                FormsAuthentication.SetAuthCookie(userName, false);
                                Response.Redirect("Index.aspx");
                            }
                            else
                            {
                                LabelErrorMessage.Text = "Invalid email or password.";
                                LabelErrorMessage.Visible = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LabelErrorMessage.Text = $"Error: {ex.Message}";
                    LabelErrorMessage.Visible = true;
                }
            }
        }

        private void AddActiveCounter(string connString, NpgsqlConnection connection, string email, NpgsqlDataReader reader)
        {
            int activeUsersDefault = 1;
            using (NpgsqlCommand cmdRetrieveCounter = new NpgsqlCommand("SELECT * FROM active_users", connection))
            {
                while (cmdRetrieveCounter.ExecuteReader().Read())
                {
                    activeUsersDefault += int.Parse(reader["counter"].ToString());
                }
            }
            using (NpgsqlCommand cmdCounter = new NpgsqlCommand())
            {
                cmdCounter.Connection = connection;
                cmdCounter.CommandText = "INSERT INTO active_users VALUES (1, @counter)";
                cmdCounter.Parameters.AddWithValue("@counter", activeUsersDefault);
            }
        }
    }
}