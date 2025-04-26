using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Data.SqlClient;


namespace Hotel_and_Transport
{
    public partial class HotelLogin : Form
    {
        public HotelLogin()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HotelSignup signup = new HotelSignup();
            signup.Show();
            this.Hide();

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            // Validate inputs
            // Email: Not empty, valid format
            string email = usrEmail.Text;
            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s,;]+@[^@\s,;]+\.[^@\s,;]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Password: Not empty
            string password = usrPassword.Text;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password is required.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Database authentication
            try
            {
                con.Open();
                string queryCheck = "SELECT * FROM ServiceProvider " +
                                   "WHERE Email = @email AND Password = @password " +
                                   "AND ProviderType = 1 AND SpStatus = 'Approved'";
                SqlCommand cmd = new SqlCommand(queryCheck, con);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Login successful!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Navigate to dashboard (replace 'HotelDashboardForm' with your form name, e.g., 'home')
                    //dashboard.Show();
                    //this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid email, password, or account not approved.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usrEmail.Clear();
                    usrPassword.Clear();
                }

                cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

    }
}
