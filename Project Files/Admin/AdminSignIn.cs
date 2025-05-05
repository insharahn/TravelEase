
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TourOperator
{
    public partial class AdminSignIn : Form
    {
        public AdminSignIn()
        {
            InitializeComponent();
        }

        private void AdminSignIn_Load(object sender, EventArgs e) { }

        private void Username_TextChanged(object sender, EventArgs e) { }

        private void Pass_TextChanged(object sender, EventArgs e) { }

        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogin();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void PerformLogin()
        {
            string username = Username.Text.Trim();
            string password = Pass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                CustomMessageBox.Show("Please enter both username and password.");
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;Initial Catalog=\"Travel ease2\";Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Admin WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int result = (int)cmd.ExecuteScalar();

                    if (result == 1)
                    {
                        CustomMessageBox.Show("Admin login successful!"); // You can redirect to admin panel here
                    }
                    else
                    {
                        CustomMessageBox.Show("Incorrect Admin details.");
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Error: " + ex.Message);
                }
            }
          

        }
    }
}

