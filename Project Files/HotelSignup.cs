using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


using Microsoft.Data.SqlClient;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Hotel_and_Transport
{
    public partial class HotelSignup : Form
    {
        public HotelSignup()
        {
            InitializeComponent();
            this.Load += new EventHandler(HotelSignup_Load); // Ensure Load event is subscribed
        }

        SqlConnection con = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        // Custom class for Destination ComboBox
        public class DestinationItem
        {
            public string DisplayName { get; set; } // Concatenated Name + CityName
            public int DestinationID { get; set; }
            public override string ToString() => DisplayName;
        }

        private void HotelSignup_Load(object sender, EventArgs e)
        {

            // Populate Destination ComboBox from database
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
                usrDestination.DataSource = destinations;
                usrDestination.DisplayMember = "DisplayName";
                usrDestination.ValueMember = "DestinationID";
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
           

            // Validate inputs
            // Hotel Name: Not empty, ≤ 100 characters
            if (string.IsNullOrEmpty(usrName.Text) || usrName.Text.Length > 100)
            {
                MessageBox.Show("Hotel Name is required and must be ≤ 100 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Email: Valid format and unique
            if (!Regex.IsMatch(usrEmail.Text, @"^[^@\s,;]+@[^@\s,;]+\.[^@\s,;]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (usrEmail.Text.Length > 100)
            {
                MessageBox.Show("Email must be ≤ 100 characters.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                con.Open();
                SqlCommand emailCheck = new SqlCommand("SELECT COUNT(*) FROM ServiceProvider WHERE Email = @email", con);
                emailCheck.Parameters.AddWithValue("@email", usrEmail.Text);
                int emailExists = (int)emailCheck.ExecuteScalar();
                if (emailExists > 0)
                {
                    MessageBox.Show("Email is already registered. Please use another.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error checking email: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            // Password: 8–15 characters
            if (usrPassword.Text.Length < 8 || usrPassword.Text.Length > 15)
            {
                MessageBox.Show("Password must be 8–15 characters.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Destination: Must be selected
            if (usrDestination.SelectedIndex == -1)
            {
                MessageBox.Show("Select a destination.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Total Rooms: ≥ 0 (ensured by NumericUpDown)
            if (usrTotalRooms.Value < 0)
            {
                MessageBox.Show("Total Rooms must be ≥ 0.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Average Price: Valid decimal, ≥ 0
            if (!decimal.TryParse(usrAvgPrice.Text, out decimal avgPrice) || avgPrice < 0)
            {
                MessageBox.Show("Average Price must be a valid number ≥ 0.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hotel Type: Must be selected
            if (usrHotelType.SelectedIndex == -1)
            {
                MessageBox.Show("Select a hotel type.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Database insertion
            try
            {
                con.Open();

                // Insert into ServiceProvider
                string queryProvider = "INSERT INTO ServiceProvider (ProviderName, Email, Password, RegistrationDate, SpStatus, DestinationID, ProviderType) " +
                                      "OUTPUT INSERTED.ProviderId " +
                                      "VALUES (@name, @email, @pwd, @regdate, @status, @destid, @type)";
                SqlCommand cmdProvider = new SqlCommand(queryProvider, con);
                cmdProvider.Parameters.AddWithValue("@name", usrName.Text);
                cmdProvider.Parameters.AddWithValue("@email", usrEmail.Text);
                cmdProvider.Parameters.AddWithValue("@pwd", usrPassword.Text);
                cmdProvider.Parameters.AddWithValue("@regdate", DateTime.Today);
                cmdProvider.Parameters.AddWithValue("@status", "Pending");
                cmdProvider.Parameters.AddWithValue("@destid", (int)usrDestination.SelectedValue);
                cmdProvider.Parameters.AddWithValue("@type", 1); // 1 = Hotel

                // Get the new ProviderId
                int providerId = (int)cmdProvider.ExecuteScalar();

                // Insert into Hotel
                string queryHotel = "INSERT INTO Hotel (ProviderID, TotalRooms, OccupiedRooms, AvgPrice, HotelType) " +
                                   "VALUES (@providerid, @totalrooms, @occupiedrooms, @avgprice, @hoteltype)";
                SqlCommand cmdHotel = new SqlCommand(queryHotel, con);
                cmdHotel.Parameters.AddWithValue("@providerid", providerId);
                cmdHotel.Parameters.AddWithValue("@totalrooms", (int)usrTotalRooms.Value);
                cmdHotel.Parameters.AddWithValue("@occupiedrooms", 0); // Fixed to 0
                cmdHotel.Parameters.AddWithValue("@avgprice", avgPrice);
                cmdHotel.Parameters.AddWithValue("@hoteltype", usrHotelType.SelectedItem.ToString());

                cmdHotel.ExecuteNonQuery();

                MessageBox.Show("Hotel registered successfully! Please wait for Admin to approve account.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void goBack_Click(object sender, EventArgs e)
        {
            HotelLogin login = new HotelLogin();
            login.Show();
            this.Hide();
        }
    }
}
