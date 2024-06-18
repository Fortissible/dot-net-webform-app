using Npgsql;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mukicik
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /// REGION UNAUTHENTICATED
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("index.aspx");
            }
        }

        protected void CalendarDOB_SelectionChanged(object sender, EventArgs e)
        {
            TextBoxDOB.Text = CalendarDOB.SelectedDate.ToString("dd MMMM yyyy");
        }

        protected void ValidateEmail(object source, ServerValidateEventArgs args)
        {
            string email = args.Value;
            if (email.Contains('@') && email.Contains('.'))
            {
                int atIndex = email.IndexOf('@');
                int dotIndex = email.LastIndexOf('.');

                if (atIndex > 0 && dotIndex > atIndex + 1 && dotIndex < email.Length - 1)
                {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        protected void ValidateGender(object source, ServerValidateEventArgs args)
        {
            if (RadioButtonMale.Checked || RadioButtonFemale.Checked)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ValidateTerms(object source, ServerValidateEventArgs args)
        {
            args.IsValid = CheckBoxTerms.Checked;
        }

        protected void RegisterToDB(string name, string email)
        {   
            string password = TextBoxPassword.Text;
            bool isMale = RadioButtonMale.Checked;
            DateTime dob = DateTime.Parse(TextBoxDOB.Text);
            string gender;
            if (isMale)
            {
                gender = "Pria";
            }
            else
            {
                gender = "Wanita";
            }
            // Insert user baru ke database
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

                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO users VALUES (@id, @username, @email, @password, @gender, @dob);", connection))
                    {
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@id", defaultId);
                        cmd.Parameters.AddWithValue("@username", name);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@dob", dob);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Show error message using JavaScript
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('Failed to insert product. Error: {ex.Message}');", true);
                }
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
                string email = TextBoxEmail.Text;
                string name = TextBoxName.Text;
                if (email.Contains('@') && email.Contains('.'))
                {
                    int atIndex = email.IndexOf('@');
                    int dotIndex = email.LastIndexOf('.');

                    if (atIndex > 0 && dotIndex > atIndex + 1 && dotIndex < email.Length - 1)
                    {
                        CustomValidatorEmail.IsValid = true;
                    }

                    RegisterToDB(name, email);

                    Response.Redirect("./Index.aspx");
                } else
                {
                    CustomValidatorEmail.IsValid = false;
                    return;
                }
            }
            else
            {
                // Validation failed, stay on the same page to display error messages
            }
        }
    }
}