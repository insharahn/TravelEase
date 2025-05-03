using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Data.SqlClient;

namespace dbfinalproject_interfaces
{
    public partial class travelerCustomTrip : Form
    {
        public int TravelerID { get; set; }
        private Dictionary<string, int> activityMap = new Dictionary<string, int>(); //activity disctionary list bc u need activity id, date, guide, etc etc 

        public travelerCustomTrip()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False";
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");

        public travelerCustomTrip(int tid)
        {
            TravelerID = tid;
            InitializeComponent();
        }

        private void travelerCustomTrip_Load(object sender, EventArgs e)
        {
            lblSustainabilityScore.Text = "?"; //set sus score

            //populate data for destination
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //city, country format
                string query = @"SELECT distinct d.DestinationID, l.CityName + ', ' + l.CountryName AS Location FROM Destination d INNER JOIN Location l ON d.LocationID = l.LocationID";
                
                SqlCommand cmd = new SqlCommand(query, conn); 
                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<int, string> destinations = new Dictionary<int, string>();
                while (reader.Read())
                {
                    destinations.Add(reader.GetInt32(0), reader.GetString(1));
                }

                cmbDestination.DataSource = new BindingSource(destinations, null);
                cmbDestination.DisplayMember = "Value";
                cmbDestination.ValueMember = "Key";

                //event handlers
                cmbDestination.SelectedIndexChanged += cmbDestination_SelectedIndexChanged; //destination chosen, reload indexes
                cmbAccommodation.SelectedIndexChanged += AccommodationOrRide_Changed; //accommodation changed, sustainabilty score changes
                cmbRide.SelectedIndexChanged += AccommodationOrRide_Changed; //ride changed, sustainabilty score changes


                conn.Close();
            }
        }

        //detination chosen, load accommodations
        private void cmbDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            int destinationId = ((KeyValuePair<int, string>)cmbDestination.SelectedItem).Key;
            LoadAccommodationsForDestination(destinationId); //load accommodations according to dest
            LoadRidesForDestination(destinationId); //do the same for rides
            LoadActivitiesForDestination(destinationId); //and activities
        }

        private void LoadAccommodationsForDestination(int destinationId) //helper function to load accommdodations
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT a.AccomodationID, s.ProviderName
                         FROM Accommodation a
                         INNER JOIN Hotel h ON h.HotelID = a.HotelID
                         INNER JOIN ServiceProvider s ON s.ProviderId = h.ProviderID
                         WHERE s.DestinationID = @destId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@destId", destinationId);

                SqlDataReader reader = cmd.ExecuteReader();
                Dictionary<int, string> accommodations = new Dictionary<int, string>
                {
                    { -1, "None" } //none option for null w/ index == -1
                };

                while (reader.Read())
                    accommodations.Add(reader.GetInt32(0), reader.GetString(1));

                cmbAccommodation.DataSource = new BindingSource(accommodations, null);
                cmbAccommodation.DisplayMember = "Value";
                cmbAccommodation.ValueMember = "Key";
            }
        }

        private void LoadRidesForDestination(int destinationID) //load on the basis of destination id, as in the service provider or ride's destination matches
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT r.RideID, sp.ProviderName FROM Ride r " +
                    "INNER JOIN TransportService t ON r.TransportID = t.TransportID " +
                    "INNER JOIN ServiceProvider sp ON sp.ProviderID = t.ProviderID " +
                    "WHERE sp.DestinationID = @destID OR r.DestinationID = @destID";

                SqlCommand cmd = new SqlCommand(query, conn);
                               
                cmd.Parameters.AddWithValue("@destID", destinationID);

                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<int, string> rides = new Dictionary<int, string>()
                {
                    { -1, "None" } //none at -1
                };

                //add to the collection
                while (reader.Read())
                {
                    rides.Add(reader.GetInt32(0), reader.GetString(1));
                }

                cmbRide.DataSource = new BindingSource(rides, null);
                cmbRide.DisplayMember = "Value";
                cmbRide.ValueMember = "Key";
            }
        }

        private void LoadActivitiesForDestination(int destinationID)
        {
            //clear the old activities and mapping
            clbActivities.Items.Clear();
            activityMap.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //load activities in from the activity table based on dest id
                string activityQuery = @"
                 SELECT A.ActivityID, A.ActivityName, A.StartTime, SP.ProviderName AS GuideName
                 FROM Activity A
                 INNER JOIN Guide G ON A.GuideID = G.GuideID
                 INNER JOIN ServiceProvider SP on SP.ProviderId = G.ProviderID 
                 WHERE SP.DestinationID = @destID
                 ";

                SqlCommand cmd = new SqlCommand(activityQuery, conn);
                cmd.Parameters.AddWithValue("@destID", destinationID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["ActivityID"]);
                    string display = $"{row["ActivityName"]} - {Convert.ToDateTime(row["StartTime"]).ToShortTimeString()} - Guide: {row["GuideName"]}";
                    activityMap[display] = id;
                    clbActivities.Items.Add(display);
                }

                conn.Close();   
            }
        }

        private void AccommodationOrRide_Changed(object sender, EventArgs e)
        {
            UpdateSustainabilityScore(); //change sus score when acc/ride changes
        }

        private void UpdateSustainabilityScore()
        {
            try
            {
                if (cmbAccommodation.SelectedValue == null || cmbRide.SelectedValue == null)
                {
                    lblSustainabilityScore.Text = "?"; //nothing selected, no score
                    return;
                }

                if (!int.TryParse(cmbAccommodation.SelectedValue.ToString(), out int accommodationID) ||
                    !int.TryParse(cmbRide.SelectedValue.ToString(), out int rideID))
                {
                    lblSustainabilityScore.Text = "?";
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //get corresponding sus score
                    string query = @"
                    SELECT TOP 1 p.SustainabilityScore
                    FROM Package p
                    INNER JOIN CustomTrip ct 
                        ON ct.AccommodationID = p.AccommodationID AND ct.RideID = p.RideID
                    WHERE ct.AccommodationID = @accID AND ct.RideID = @rideID
                    GROUP BY ct.AccommodationID, ct.RideID, p.SustainabilityScore, p.Title";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@accID", accommodationID);
                    cmd.Parameters.AddWithValue("@rideID", rideID);

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int score))
                    {
                        lblSustainabilityScore.Text = score.ToString();
                    }
                    else
                    {
                        lblSustainabilityScore.Text = "3"; //default to 3 if no acc-ride match found
                    }
                }
            }
            catch (Exception ex)
            {
                lblSustainabilityScore.Text = "?"; //fallback
                MessageBox.Show("An error occurred while updating the score: " + ex.Message);
            }
        }



        private void lblDestination_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerHome home = new travelerHome(TravelerID);
            home.Show();

        }

        private void btnBook_Click(object sender, EventArgs e) //insert into custom trip AND request
        {
            try
            {
                con.Open();

                //get a random existing operator the the request
                int operatorId = -1;
                string operatorQuery = "SELECT TOP 1 OperatorID FROM Operator ORDER BY NEWID()";
                SqlCommand operatorCmd = new SqlCommand(operatorQuery, con);
                object result = operatorCmd.ExecuteScalar();

                if (result != null)
                {
                    operatorId = Convert.ToInt32(result);
                }
                else
                {
                    MessageBox.Show("No operators found.");
                    return;
                }

                //---------------- CUSTOM TRIP INSERTION------------------
                //get the values
                int destinationId = ((KeyValuePair<int, string>)cmbDestination.SelectedItem).Key;
                int accommodationId = ((KeyValuePair<int, string>)cmbAccommodation.SelectedItem).Key;
                int rideId = ((KeyValuePair<int, string>)cmbRide.SelectedItem).Key;
                int groupSize = Convert.ToInt32(cmbGroupSize.SelectedItem);
                int duration = Convert.ToInt32(cmbDuration.SelectedItem);
                string tripType = cmbTripType.SelectedItem?.ToString();
                int sustainabilityScore = int.Parse(lblSustainabilityScore.Text);
                string capacityType = groupSize == 1 ? "Solo" : "Group";

                //flags
                int accommodationStatus = (accommodationId == -1) ? 0 : 1;
                int rideStatus = (rideId == -1) ? 0 : 1;
                int activityStatus = (clbActivities.CheckedItems.Count == 0) ? 0 : 1;

                //custom trip insertion
                string insertTripQuery = @"
                INSERT INTO CustomTrip 
                (TravelerID, AccommodationID, AccommodationStatusFlag, ActivitiesStatusFlag, TripType, CapacityType, GroupSize, Duration, BasePrice, RideID, RideStatusFlag, DestinationID, SustainabilityScore)
                OUTPUT INSERTED.CustomTripID
                VALUES 
                (@TravelerID, @AccommodationID, @AccommodationStatusFlag, @ActivitiesStatusFlag, @TripType, @CapacityType, @GroupSize, @Duration, @BasePrice, @RideID, @RideStatusFlag, @DestinationID, @SustainabilityScore)";

                SqlCommand cmd = new SqlCommand(insertTripQuery, con);
                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                cmd.Parameters.AddWithValue("@AccommodationID", accommodationId == -1 ? (object)DBNull.Value : accommodationId);
                cmd.Parameters.AddWithValue("@AccommodationStatusFlag", accommodationStatus);
                cmd.Parameters.AddWithValue("@TripType", tripType);
                cmd.Parameters.AddWithValue("@CapacityType", capacityType);
                cmd.Parameters.AddWithValue("@GroupSize", groupSize);
                cmd.Parameters.AddWithValue("@Duration", duration);
                cmd.Parameters.AddWithValue("@BasePrice", 1000); //fixed to 1000 for custom
                cmd.Parameters.AddWithValue("@RideID", rideId == -1 ? (object)DBNull.Value : rideId);
                cmd.Parameters.AddWithValue("@RideStatusFlag", rideStatus);
                cmd.Parameters.AddWithValue("@DestinationID", destinationId);
                cmd.Parameters.AddWithValue("@SustainabilityScore", sustainabilityScore);
                cmd.Parameters.AddWithValue("@ActivitiesStatusFlag", activityStatus);


                int customTripId = (int)cmd.ExecuteScalar(); //get generated CustomTripID

                //insert activities
                foreach (string selected in clbActivities.CheckedItems)
                {
                    if (activityMap.TryGetValue(selected, out int activityId))
                    {
                        string insertActivity = "INSERT INTO CustomTripActivities (CustomTripID, ActivityID) VALUES (@CustomTripID, @ActivityID)";
                        SqlCommand activityCmd = new SqlCommand(insertActivity, con);
                        activityCmd.Parameters.AddWithValue("@CustomTripID", customTripId);
                        activityCmd.Parameters.AddWithValue("@ActivityID", activityId);
                        activityCmd.ExecuteNonQuery();
                    }
                }


            //------------------- REQUEST INSERTION -------------
            string requestQuery = @"
                INSERT INTO Request 
                (OperatorID, TravelerID, TripSourceType, PackageID, CustomTripID, DateRequested, RequestStatus, 
                 PreferredStartDate, AccomodationPaidStatus, RidePaidStatus, ActivityPaidStatus)
                VALUES 
                (@OperatorID, @TravelerID, 'Custom', NULL, @CustomTripID, GETDATE(), 'Pending', 
                 @PreferredStartDate, 'Unpaid', 'Unpaid', 'Unpaid')";

                SqlCommand requestCmd = new SqlCommand(requestQuery, con);
                requestCmd.Parameters.AddWithValue("@OperatorID", operatorId);
                requestCmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                requestCmd.Parameters.AddWithValue("@CustomTripID", customTripId);
                requestCmd.Parameters.AddWithValue("@PreferredStartDate", dtpPreferredStartDate.Value.Date);

                int rowsAffected = requestCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Custom trip registered successfully! Request sent for approval.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //change forms
                    this.Hide();
                    travelerBookings booking = new travelerBookings(TravelerID);
                    booking.Show();
                } 
                else
                {
                    MessageBox.Show("Custom trip inserted, but failed to submit request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting custom trip: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //open bookings
            this.Hide();
            travelerBookings booking = new travelerBookings(TravelerID);
            booking.Show();
        }

        private void clbActivities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpPreferredStartDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
