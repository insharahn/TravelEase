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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace dbfinalproject_interfaces
{
    public partial class travelerSignUp : Form
    {
        public travelerSignUp()
        {
            InitializeComponent();
            LoadNationalityDropdown(); //load dropdown options for nationality

        }

        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            travelerLogin login = new travelerLogin();
            login.Show();
            this.Hide();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        //this function ensures the city (country) options are loaded when the traveler picks their nationality
        private void LoadNationalityDropdown()
        {
            SqlCommand cmd = new SqlCommand("SELECT LocationID, CityName + ' (' + CountryName + ')' AS DisplayText FROM Location", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbNationality.DataSource = dt;
            cmbNationality.DisplayMember = "DisplayText";  //what user sees
            cmbNationality.ValueMember = "LocationID";      //what gets saved
        }


        private void cmbNationality_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //concatenate preferences based on checked preferences in list
            List<string> selectedPreferences = new List<string>();
            foreach (var item in clbPreferences.CheckedItems)
            {
                selectedPreferences.Add(item.ToString());
            }
            string preferences = string.Join(", ", selectedPreferences);

            //validate rules and let the user know whats wrong

            //email format + uniqueness
            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s,;]+@[^@\s,;]+\.[^@\s,;]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //password length
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //contact only digits
            if (!Regex.IsMatch(txtContact.Text, @"^\d+$"))
            {
                MessageBox.Show("Contact number must contain only digits.", "Invalid Contact", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try //try-catch in case of an exception; database wont crash
            {
                con.Open(); //open connection after validation

                string query = "INSERT INTO Traveler (FirstName, LastName, Contact, Email, Username, Password, Nationality, RegistrationDate, Preferences, DateOfBirth) " +
                               "VALUES (@fname, @lname, @phone, @email, @user, @pwd, @nationality, @regdate, @preferences, @dob)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                cmd.Parameters.AddWithValue("@pwd", txtPassword.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@fname", txtFName.Text);
                cmd.Parameters.AddWithValue("@lname", txtLName.Text);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@phone", txtContact.Text);
                cmd.Parameters.AddWithValue("@nationality", cmbNationality.SelectedValue);
                cmd.Parameters.AddWithValue("@regdate", DateTime.Today); //todays date for registration
                cmd.Parameters.AddWithValue("@preferences", preferences);


                //uniqueness
                SqlCommand emailCheck = new SqlCommand("SELECT COUNT(*) FROM Traveler WHERE Email = @email", con);
                emailCheck.Parameters.AddWithValue("@email", txtEmail.Text);
                int emailExists = (int)emailCheck.ExecuteScalar();
                if (emailExists > 0)
                {
                    MessageBox.Show("Email is already registered. Please use another.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                    return;
                }
                //unique usernmae
                SqlCommand usernameCheck = new SqlCommand("SELECT COUNT(*) FROM Traveler WHERE Username = @username", con);
                usernameCheck.Parameters.AddWithValue("@username", txtUsername.Text);
                int usernameExists = (int)usernameCheck.ExecuteScalar();
                if (usernameExists > 0)
                {
                    MessageBox.Show("Username is already taken. Choose a different one.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                    return;
                }

                cmd.ExecuteNonQuery();

                //fetch the TravelerID
                string getTravelerIdQuery = "SELECT TOP 1 TravelerID FROM Traveler ORDER BY TravelerID DESC";
                SqlCommand getIdCmd = new SqlCommand(getTravelerIdQuery, con);
                int travelerID = (int)getIdCmd.ExecuteScalar(); //get the last inserted TravelerID

                MessageBox.Show("Registered successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                travelerHome home = new travelerHome();
                home.TravelerID = Convert.ToInt32(travelerID); //set the TravelerID
                home.Show();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close(); //close the connection
            }
        }



        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtLName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void travelerSignUp_Load(object sender, EventArgs e)
        {

        }
    }
}

