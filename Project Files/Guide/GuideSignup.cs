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
using static Hotel_and_Transport.HotelSignup;

namespace Hotel_and_Transport
{
    public partial class GuideSignup : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

        public GuideSignup()
        {
            InitializeComponent();
            this.Load += new EventHandler(GuideSignup_Load); // Ensure Load event is subscribed

        }
        private void GuideSignup_Load(object sender, EventArgs e)
        {

            // Populate Destination ComboBox
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT d.DestinationID, ISNULL(d.Name, '') + ', ' + l.CityName AS DisplayName " +
                    "FROM Destination d " +
                    "JOIN Location l ON d.LocationID = l.LocationID " +
                    "ORDER BY l.CityName, d.Name", con);
                SqlDataReader reader = cmd.ExecuteReader();
                List<DestinationItem> destinations = new List<DestinationItem>();
                while (reader.Read())
                {
                    destinations.Add(new DestinationItem
                    {
                        DisplayName = reader["DisplayName"].ToString(),
                        DestinationID = (int)reader["DestinationID"]
                    });
                }
                reader.Close();
                cmbLocation.DataSource = destinations;
                cmbLocation.DisplayMember = "DisplayName";
                cmbLocation.ValueMember = "DestinationID";
                cmbLocation.SelectedIndex = -1;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading destinations: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        private void signup_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter your name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtName.Text.Length > 100)
                {
                    MessageBox.Show("Name cannot exceed 100 characters.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text) || !Regex.IsMatch(txtEmail.Text, @"^[^@,\s;]+@[^@,\s;]+\.[^@,\s;]+$"))
                {
                    MessageBox.Show("Please enter a valid email address (e.g., example@domain.com).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 8 || txtPassword.Text.Length > 15)
                {
                    MessageBox.Show("Password must be between 8 and 15 characters.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbActivityType.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select an activity type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (nudCapacity.Value <= 0)
                {
                    MessageBox.Show("Capacity must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbLocation.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a location.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Please enter a valid non-negative price (e.g., 32.50).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!radSignLanguageYes.Checked && !radSignLanguageNo.Checked)
                {
                    MessageBox.Show("Please select sign language availability (Yes/No).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    // Check if email exists
                    string checkEmailQuery = "SELECT COUNT(*) FROM ServiceProvider WHERE Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    int emailCount = (int)checkCmd.ExecuteScalar();
                    if (emailCount > 0)
                    {
                        MessageBox.Show("Email already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Insert into ServiceProvider
                    string insertProviderQuery = @"
                INSERT INTO ServiceProvider (ProviderName, Email, Password, RegistrationDate, SpStatus, DestinationID, ProviderType)
                OUTPUT INSERTED.ProviderID
                VALUES (@ProviderName, @Email, @Password, @RegistrationDate, 'Pending', @DestinationID, 2)";
                    SqlCommand cmd = new SqlCommand(insertProviderQuery, conn);
                    cmd.Parameters.AddWithValue("@ProviderName", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Today);
                    cmd.Parameters.AddWithValue("@DestinationID", ((dynamic)cmbLocation.SelectedItem).DestinationID);
                    int providerId = (int)cmd.ExecuteScalar();

                    // Insert into Guide
                    string insertGuideQuery = @"
                INSERT INTO Guide (ProviderID, SignLanguageFlag, Price)
                OUTPUT INSERTED.GuideID
                VALUES (@ProviderID, @SignLanguageFlag, @Price)";
                    cmd = new SqlCommand(insertGuideQuery, conn);
                    cmd.Parameters.AddWithValue("@ProviderID", providerId);
                    cmd.Parameters.AddWithValue("@SignLanguageFlag", radSignLanguageYes.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Price", price);
                    int guideId = (int)cmd.ExecuteScalar();

                    // Insert into Activity
                    string insertActivityQuery = @"
                INSERT INTO Activity (ActivityName, GuideID, StartTime, EndTime, CapacityLimit, Price)
                VALUES (@ActivityName, @GuideID, @StartTime, @EndTime, @CapacityLimit, @Price)";
                    cmd = new SqlCommand(insertActivityQuery, conn);
                    cmd.Parameters.AddWithValue("@ActivityName", cmbActivityType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@GuideID", guideId);
                    cmd.Parameters.AddWithValue("@StartTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@EndTime", DateTime.Now.AddHours(2)); // Default end time 2 hours from start
                    cmd.Parameters.AddWithValue("@CapacityLimit", nudCapacity.Value);
                    cmd.Parameters.AddWithValue("@Price", price);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Signup successful! Please wait for admin approval.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Clear form
                    txtName.Clear();
                    txtEmail.Clear();
                    txtPassword.Clear();
                    cmbActivityType.SelectedIndex = -1;
                    nudCapacity.Value = 0;
                    cmbLocation.SelectedIndex = -1;
                    txtPrice.Clear();
                    radSignLanguageYes.Checked = false;
                    radSignLanguageNo.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during signup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void goBack_Click(object sender, EventArgs e)
        {
            GuideLogin login = new GuideLogin();
            login.Show();
            this.Hide();
        }

        private void GuideSignup_Load_1(object sender, EventArgs e)
        {

        }
    }
}
