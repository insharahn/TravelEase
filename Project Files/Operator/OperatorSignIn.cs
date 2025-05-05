using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TourOperator
{
    public partial class OperatorSignIn : Form
    {
        public OperatorSignIn()
        {
            InitializeComponent();
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            // Optional: you can add live validation here
        }

        private void Pass_TextChanged(object sender, EventArgs e)
        {
            // Optional
        }

        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogin();
                e.Handled = true;
                e.SuppressKeyPress = true; // prevents the ding sound
            }
        }

        private void PerformLogin()
        {
            string email = Email.Text.Trim();
            string password = Pass.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

           


            using (SqlConnection conn = new SqlConnection( "Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
            "Initial Catalog=\"Travel ease2\";" +
            "Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT OpStatus FROM Operator WHERE Email = @Email AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        CustomMessageBox.Show("Invalid email or password.");
                    }
                    else
                    {
                        string status = result.ToString();
                        if (status == "Approved")
                        {
                            CustomMessageBox.Show("Operator signed in successfully!");
                            // You can open a new form here
                        }
                        else
                        {
                            CustomMessageBox.Show("Please ask admin to approve your status.");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Pass_KeyDown_1(object sender, KeyEventArgs e)
        {
          
                if (e.KeyCode == Keys.Enter)
                {
                   // MessageBox.Show("Enter key pressed"); // TEST
                    PerformLogin();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }

        
    }
}
