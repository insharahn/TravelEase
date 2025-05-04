using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Data.SqlClient;


namespace dbfinalproject_interfaces
{
    public partial class travelerLogin : Form
    {
        public travelerLogin()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            con.Open();

            string querycheck = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if (!string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(username))
            {
                //email login
                querycheck = "SELECT * FROM Traveler WHERE Email = @email AND Password = @password AND TravelerStatus = 'Approved'";
                cmd.CommandText = querycheck;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
            }
            else if (!string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(email))
            {
                //username login
                querycheck = "SELECT * FROM Traveler WHERE Username = @username AND Password = @password AND TravelerStatus = 'Approved'";
                cmd.CommandText = querycheck;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
            }
            else if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(email))
            {
                //username + email login
                querycheck = "SELECT * FROM Traveler WHERE Username = @username AND Email = @email AND Password = @password AND TravelerStatus = 'Approved'";
                cmd.CommandText = querycheck;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
            }
            else
            {
                MessageBox.Show("Please enter a Username or Email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
                return;
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0) //success
            {
                // MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int travelerId = Convert.ToInt32(dt.Rows[0]["TravelerID"]); //get traveler id
                this.Hide();
                travelerHome home = new travelerHome();
                home.TravelerID = travelerId; //set
                home.Show();
            }
            else //fail
            {
                //check if user exists and verify the credentials first
                string loginQuery = "SELECT TravelerStatus FROM Traveler WHERE " +
                                    (!string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(username)
                                        ? "Email = @email AND Password = @password"
                                        : !string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(email)
                                            ? "Username = @username AND Password = @password"
                                            : "Email = @email AND Username = @username AND Password = @password");

                SqlCommand loginCmd = new SqlCommand(loginQuery, con);
                foreach (SqlParameter p in cmd.Parameters)
                    loginCmd.Parameters.AddWithValue(p.ParameterName, p.Value);

                object loginResult = loginCmd.ExecuteScalar();

                if (loginResult == null) //invalid login credentials
                {
                    MessageBox.Show("Invalid login credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //if credentials are valid, check account status
                    string status = loginResult.ToString();
                    if (status == "Approved")
                    {
                        //proceed with login
                        int travelerId = Convert.ToInt32(dt.Rows[0]["TravelerID"]); //get traveler id
                        this.Hide();
                        travelerHome home = new travelerHome();
                        home.TravelerID = travelerId; //set
                        home.Show();
                    }
                    else
                    {
                        //account exists but isn't approved
                        MessageBox.Show($"Login blocked: Your account is currently '{status}'.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                //clear fields after attempt to log in
                txtUsername.Clear();
                txtEmail.Clear();
                txtPassword.Clear();
                
            }

            con.Close();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            travelerSignUp signup = new travelerSignUp();
            signup.Show();
            this.Hide();
        }

        //unhide password hehe
        private bool passwordVisible = false;
        private void btnShowPwd_Click(object sender, EventArgs e)
        {
            if (passwordVisible)
            {
                //hide
                txtPassword.PasswordChar = '*';
                btnShowPwd.Text = "Show";
                passwordVisible = false;
            }
            else
            {
                //show
                txtPassword.PasswordChar = '\0'; //no masking character
                btnShowPwd.Text = "Hide";
                passwordVisible = true;
            }
        }

        private void travelerLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
