using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Electricity_Billing.ElectricityBillingWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Always use parameterized queries to avoid SQL injection
            string connStr = ConfigurationManager.ConnectionStrings["ElectricityBillDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT COUNT(*) FROM Admin WHERE Username = @username AND [Password] = @password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                con.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    
                    Response.Redirect("BillEntry.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid credentials!";
                }
            }
        }
    }
}
