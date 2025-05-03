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

namespace dbfinalproject_interfaces
{
    public partial class travelerUpdateProfile : Form
    {
        public int TravelerID { get; set; }

    
        public travelerUpdateProfile(int tid)
        {
            TravelerID = tid;
            InitializeComponent();
            LoadNationalityDropdown();
        }

        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");


        private void travelerUpdateProfile_Load(object sender, EventArgs e)
        {
            LoadTravelerProfile();

        }

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

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private bool oldPasswordVisible = false;
        private void btnShowOldPwd_Click(object sender, EventArgs e)
        {
            if (oldPasswordVisible)
            {
                //hide
                txtOldPassword.PasswordChar = '*';
                btnShowOldPwd.Text = "Show";
                oldPasswordVisible = false;
            }
            else
            {
                //show
                txtOldPassword.PasswordChar = '\0'; //no masking character
                btnShowOldPwd.Text = "Hide";
                oldPasswordVisible = true;
            }
        }

        private bool newPasswordVisible = false;
        private void btnShowNewPwd_Click(object sender, EventArgs e)
        {
            if (newPasswordVisible)
            {
                //hide
                txtPasswordNew.PasswordChar = '*';
                btnShowNewPwd.Text = "Show";
                newPasswordVisible = false;
            }
            else
            {
                //show
                txtPasswordNew.PasswordChar = '\0'; //no masking character
                btnShowNewPwd.Text = "Hide";
                newPasswordVisible = true;
            }
        }

        private void clbPreferences_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadTravelerProfile()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT 
                FirstName, LastName, Email, Username, Preferences, DateOfBirth, Contact,
                L.CityName, L.CountryName
            FROM 
                Traveler T
            JOIN 
                Location L ON T.Nationality = L.LocationID
            WHERE 
                TravelerID = @TravelerID", con);
                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string fname = reader["FirstName"].ToString();
                    string lname = reader["LastName"].ToString();
                    lblName.Text = fname + " " + lname;
                    lblEmail.Text = reader["Email"].ToString();
                    lblUser.Text = reader["Username"].ToString();
                    lblPreferences.Text = reader["Preferences"].ToString();
                    lblDOB.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    lblContact.Text = reader["Contact"].ToString();
                    lblNationality.Text = reader["CityName"] + ", " + reader["CountryName"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading traveler profile: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //gather data
            string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? lblEmail.Text : txtEmail.Text.Trim();
            string username = string.IsNullOrWhiteSpace(txtUser.Text) ? lblUser.Text : txtUser.Text.Trim();
            string contact = string.IsNullOrWhiteSpace(txtContact.Text) ? lblContact.Text : txtContact.Text.Trim();
            string newPassword = txtPasswordNew.Text.Trim();
            string oldPassword = txtOldPassword.Text.Trim();
            int nationalityId = (int)cmbNationality.SelectedValue;
            string preferences; //preserve old preferences
            if (clbPreferences.CheckedItems.Count == 0)
            {
                preferences = lblPreferences.Text; // Preserve existing preferences if none are selected
            }
            else
            {
                //concatenate preferences based on checked preferences in list
                List<string> selectedPreferences = new List<string>();
                foreach (var item in clbPreferences.CheckedItems)
                    selectedPreferences.Add(item.ToString());
                preferences = string.Join(", ", selectedPreferences);
            }

            //validate rules and let the user know whats wrong

            //email format + uniqueness
            if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, @"^[^@\s,;]+@[^@\s,;]+\.[^@\s,;]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //password length
            if (!string.IsNullOrEmpty(newPassword) && newPassword.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //contact only digits
            if (!string.IsNullOrEmpty(contact) && !Regex.IsMatch(contact, @"^\d+$"))
            {
                MessageBox.Show("Contact number must contain only digits.", "Invalid Contact", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try //try-catch in case of an exception; database wont crash
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                //check email is unique
                SqlCommand emailCheck = new SqlCommand("SELECT COUNT(*) FROM Traveler WHERE Email = @em AND TravelerID <> @id", con);
                emailCheck.Parameters.AddWithValue("@em", email);
                emailCheck.Parameters.AddWithValue("@id", TravelerID);
                if ((int)emailCheck.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Email is already in use.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //unique username
                SqlCommand usernameCheck = new SqlCommand("SELECT COUNT(*) FROM Traveler WHERE Username = @un AND TravelerID <> @id", con);
                usernameCheck.Parameters.AddWithValue("@un", username);
                usernameCheck.Parameters.AddWithValue("@id", TravelerID);
                if ((int)usernameCheck.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Username is already taken.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //old password must be right to change new password
                if (!string.IsNullOrEmpty(newPassword))
                {
                    SqlCommand oldPwdCmd = new SqlCommand("SELECT Password FROM Traveler WHERE TravelerID = @id", con);
                    oldPwdCmd.Parameters.AddWithValue("@id", TravelerID);
                    string currentPassword = (string)oldPwdCmd.ExecuteScalar();

                    if (currentPassword != oldPassword)
                    {
                        MessageBox.Show("Old password is incorrect.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                //update query
                SqlCommand updateCmd = new SqlCommand(@"
                UPDATE Traveler 
                SET 
                    Contact = @contact,
                    Email = @em,
                    Username = @un,
                    Password = COALESCE(NULLIF(@pwd, ''), Password),
                    Nationality = @nat,
                    Preferences = @prefs
                WHERE TravelerID = @id", con);

                updateCmd.Parameters.AddWithValue("@contact", contact);
                updateCmd.Parameters.AddWithValue("@em", email);
                updateCmd.Parameters.AddWithValue("@un", username);
                updateCmd.Parameters.AddWithValue("@pwd", string.IsNullOrEmpty(newPassword) ? (object)DBNull.Value : newPassword);
                updateCmd.Parameters.AddWithValue("@nat", nationalityId);
                updateCmd.Parameters.AddWithValue("@prefs", preferences);
                updateCmd.Parameters.AddWithValue("@id", TravelerID);

                updateCmd.ExecuteNonQuery();

                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                con.Close();

                //clear the input fields
                txtEmail.Clear();
                txtUser.Clear();
                txtContact.Clear();
                txtPasswordNew.Clear();
                txtOldPassword.Clear();
                clbPreferences.ClearSelected();

                for (int i = 0; i < clbPreferences.Items.Count; i++)
                {
                    clbPreferences.SetItemChecked(i, false);
                }


                LoadTravelerProfile(); //reload information


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();  
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerHome home = new travelerHome(TravelerID);
            home.Show();    
        }
    }
}
