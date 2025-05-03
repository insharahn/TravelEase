using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Data.SqlClient;

namespace dbfinalproject_interfaces
{
    public partial class PackageCard : UserControl
    {
        private int travelerId;
        private int packageId;

        public PackageCard(int travelerId, int packageId)
        {
            InitializeComponent();

            this.travelerId = travelerId;
            this.packageId = packageId;
        }

        public PackageCard()
        {
            InitializeComponent();
        }
        
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                //fetch a RANDOM (newid() generates unique value, order by shuffles and that creates randomness) but EXISTING operatorid
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

                //insert request into table (custom trip always null)
                string query = @"
                INSERT INTO Request 
                (OperatorID, TravelerID, TripSourceType, PackageID, CustomTripID, DateRequested, RequestStatus, PreferredStartDate, AccomodationPaidStatus, RidePaidStatus, ActivityPaidStatus)
                VALUES 
                (@OperatorID, @TravelerID, 'Package', @PackageID, NULL, GETDATE(), 'Pending', DATEADD(MONTH, 2, GETDATE()), 'Unpaid', 'Unpaid', 'Unpaid')";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@OperatorID", operatorId);
                cmd.Parameters.AddWithValue("@TravelerID", travelerId);
                cmd.Parameters.AddWithValue("@PackageID", packageId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Request successfully submitted!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting request: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }



        //method to set data every time a new card is made (like a parameterized constructor)
        public void SetData(string title, string description, string destinationCity, string destinationCountry, string accommodation, 
                            string transport, string activities, string groupSize, string tripType, 
                            string susScore, string duration, string price)
        {
            lblTitle.Text = title;
            lblDesc.Text = description.Length > 50 ? description.Substring(0, 50) + "..." : description;
            lblDestination.Text = destinationCity + ", " + destinationCountry;
            lblAccommodation.Text = "Accommodation Name: " + accommodation;
            lblTransport.Text = "Transport Service: " + transport;
            lblActivities.Text = "Itinerary Includes " + activities + "!";
            lblGroupSize.Text = "Maximum Group Size: " + groupSize;
            lblTripType.Text = "Trip Type: " + tripType;
            lblSusScore.Text = "Sustainaibilty Score: " + susScore;
            lblDuration.Text = "Duration: " + duration + " days";
            lblBasePrice.Text = "Base Price: $" + price;

        }


        private void PackageCard_Load(object sender, EventArgs e)
        {

        }

        private void btnWishlist_Click(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                string query = @"
                INSERT INTO Wishlist (TravelerID, PackageID, AddedDate)
                VALUES (@TravelerID, @PackageID, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Connection = con;

                Debug.WriteLine($"TravelerID={travelerId}, PackageID={packageId}");

                cmd.Parameters.AddWithValue("@TravelerID", travelerId);
                cmd.Parameters.AddWithValue("@PackageID", packageId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Added to wishlist successfully!");
                }


            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL Error {ex.Number}: {ex.Message}");

                // Handle duplicate entries (if traveler already has this package)
                if (ex.Number == 2627) // Primary key violation
                {
                    MessageBox.Show("This package is already in your wishlist!");
                }
                else
                {
                    MessageBox.Show("Unable to perform this action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                con.Close();
            }
        }

        private void lblActivities_Click(object sender, EventArgs e)
        {

        }
    }
}
