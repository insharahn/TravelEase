using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace TourOperator
{
    public partial class Form1 : Form
    {
        private int _operatorId;
        private int _selectedPackageId = -1;
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
            "Initial Catalog=\"Travel ease2\";" +
            "Integrated Security=True;Encrypt=False");

        public Form1(int operator_Id)
        {
            InitializeComponent();
            _operatorId = operator_Id;

            Load += Form1_Load;
            ADDTRIP.Click += ADDTRIP_Click;
            updateTrip.Click += updateTrip_Click;
            deleteTrip.Click += deleteTrip_Click;
            Mytrips.CellClick += Mytrips_CellClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // populate dropdowns
            destination.Items.Clear();
            for (int i = 1; i <= 51; i++)
                destination.Items.Add(i.ToString());
            destination.SelectedIndex = 0;

            triptype.Items.AddRange(new[]
            {
                "Leisure", "Adventure", "Education",
                "Business", "Spiritual/Religious", "Cultural", "Other"
            });
            triptype.SelectedIndex = -1;

            capacitytype.Items.AddRange(new[] { "Solo", "Group" });
            capacitytype.SelectedIndex = -1;

            // load everything
            LoadMyTrips();
            LoadAccommodations();
            LoadRides();
            LoadRequests();
        }

        private void LoadMyTrips()
        {
            try
            {
                const string sql = "SELECT * FROM Package WHERE OperatorID = @OperatorID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@OperatorID", _operatorId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Mytrips.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trips: " + ex.Message);
            }
        }

        private void Mytrips_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = Mytrips.Rows[e.RowIndex];
            // read PK
            _selectedPackageId = Convert.ToInt32(row.Cells["PackageID"].Value);

            // load into form
            title.Text = row.Cells["Title"].Value?.ToString() ?? "";
            description.Text = row.Cells["Description"].Value?.ToString() ?? "";
            destination.SelectedItem = row.Cells["DestinationID"].Value.ToString();
            triptype.SelectedItem = row.Cells["TripType"].Value?.ToString();
            capacitytype.SelectedItem = row.Cells["CapacityType"].Value?.ToString();
            groupsize.Text = row.Cells["GroupSize"].Value.ToString();
            duration.Text = row.Cells["Duration"].Value.ToString();
            baseprice.Text = row.Cells["BasePrice"].Value.ToString();
        }
        

        private void LoadAccommodations()
        {
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM Accommodation", con))
                {
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    accomodationgridview.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accommodations: " + ex.Message);
            }
        }

        private void LoadRides()
        {
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM Ride", con))
                {
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    ridesgridview.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rides: " + ex.Message);
            }
        }
        private void LoadRequests()
        {
            try
            {
                const string sql = "SELECT * FROM Request WHERE OperatorID = @OperatorID";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@OperatorID", _operatorId);
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        mybookings.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading booking requests: " + ex.Message);
            }
        }


        private void accomodationgridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_selectedPackageId < 0)
            {
                MessageBox.Show("Select a trip first.");
                return;
            }
            var row = accomodationgridview.Rows[e.RowIndex];
            int accId = Convert.ToInt32(row.Cells["AccommodationID"].Value);
            try
            {
                using (var cmd = new SqlCommand(
                    "UPDATE Package SET AccommodationID=@AccID, AccommodationStatusFlag=1 WHERE PackageID=@PID AND OperatorID=@OID", con))
                {
                    cmd.Parameters.AddWithValue("@AccID", accId);
                    cmd.Parameters.AddWithValue("@PID", _selectedPackageId);
                    cmd.Parameters.AddWithValue("@OID", _operatorId);
                    con.Open(); cmd.ExecuteNonQuery(); con.Close();
                }
                MessageBox.Show("Accommodation assigned to trip.");
                LoadMyTrips();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning accommodation: " + ex.Message);
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void ridesgridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_selectedPackageId < 0)
            {
                MessageBox.Show("Select a trip first.");
                return;
            }
            var row = ridesgridview.Rows[e.RowIndex];
            int rideId = Convert.ToInt32(row.Cells["RideID"].Value);
            try
            {
                using (var cmd = new SqlCommand(
                    "UPDATE Package SET RideID=@RideID, RideStatusFlag=1 WHERE PackageID=@PID AND OperatorID=@OID", con))
                {
                    cmd.Parameters.AddWithValue("@RideID", rideId);
                    cmd.Parameters.AddWithValue("@PID", _selectedPackageId);
                    cmd.Parameters.AddWithValue("@OID", _operatorId);
                    con.Open(); cmd.ExecuteNonQuery(); con.Close();
                }
                MessageBox.Show("Ride assigned to trip.");
                LoadMyTrips();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning ride: " + ex.Message);
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void ADDTRIP_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            const string sql = @"
INSERT INTO Package
  (Title, Description, DestinationID,
   AccommodationID, AccommodationStatusFlag,
   RideID, RideStatusFlag,
   ActivityStatusFlag,
   TripType, CapacityType, GroupSize, Duration, BasePrice,
   SustainabilityScore, OperatorID)
VALUES
  (@Title, @Desc, @DestID,
   @AccID, @AccFlag,
   @RideID, @RideFlag,
   @ActFlag,
   @TripType, @CapType, @GroupSize, @Duration, @BasePrice,
   @SustainScore, @OperatorID);";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    AddCommonParameters(cmd);
                    cmd.Parameters.AddWithValue("@OperatorID", _operatorId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Trip added successfully.");
                    ClearForm();
                    LoadMyTrips();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding trip: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        private void updateTrip_Click(object sender, EventArgs e)
        {
            if (_selectedPackageId < 0)
            {
                MessageBox.Show("Select a trip from the grid first.");
                return;
            }
            if (!ValidateForm()) return;

            const string sql = @"
UPDATE Package SET
    Title = @Title,
    Description = @Desc,
    DestinationID = @DestID,
    TripType = @TripType,
    CapacityType = @CapType,
    GroupSize = @GroupSize,
    Duration = @Duration,
    BasePrice = @BasePrice
WHERE
    PackageID = @PackageID AND OperatorID = @OperatorID;";

            try
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    AddCommonParameters(cmd);
                    cmd.Parameters.AddWithValue("@PackageID", _selectedPackageId);
                    cmd.Parameters.AddWithValue("@OperatorID", _operatorId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Trip updated successfully.");
                        // --- reload the grid ---
                        LoadMyTrips();

                        // --- re-select the just-updated row in the grid ---
                        foreach (DataGridViewRow r in Mytrips.Rows)
                        {
                            if ((int)r.Cells["PackageID"].Value == _selectedPackageId)
                            {
                                r.Selected = true;
                                Mytrips.FirstDisplayedScrollingRowIndex = r.Index;
                                break;
                            }
                        }
                        // At this point your form fields still show the updated values,
                        // and _selectedPackageId remains set so further edits work.
                    }
                    else
                    {
                        MessageBox.Show("Update failed (check permissions or data).");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating trip: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }


        private void deleteTrip_Click(object sender, EventArgs e)
        {
            if (_selectedPackageId < 0)
            {
                MessageBox.Show("Select a trip from the grid first.");
                return;
            }
            if (MessageBox.Show("Delete this trip?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            const string sql = @"
DELETE FROM Package
WHERE PackageID = @PackageID AND OperatorID = @OperatorID;";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@PackageID", _selectedPackageId);
                    cmd.Parameters.AddWithValue("@OperatorID", _operatorId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Trip deleted.");
                        ClearForm();
                        LoadMyTrips();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed (check permissions or data).");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting trip: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(title.Text))
            {
                MessageBox.Show("Please enter a Title."); return false;
            }
            if (!int.TryParse(groupsize.Text, out int grp) || grp < 1)
            {
                MessageBox.Show("Group Size must be ≥1."); return false;
            }
            if (!int.TryParse(duration.Text, out int dur) || dur <= 0)
            {
                MessageBox.Show("Duration must be >0."); return false;
            }
            if (!decimal.TryParse(baseprice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Base Price must be ≥0."); return false;
            }
            if (triptype.SelectedIndex < 0 || capacitytype.SelectedIndex < 0)
            {
                MessageBox.Show("Select both Trip Type and Capacity Type."); return false;
            }
            return true;
        }

        private void AddCommonParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Title", title.Text.Trim());

            if (string.IsNullOrWhiteSpace(description.Text))
                cmd.Parameters.Add("@Desc", SqlDbType.NVarChar).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@Desc", SqlDbType.NVarChar).Value = description.Text.Trim();

            cmd.Parameters.AddWithValue("@DestID", Convert.ToInt32(destination.SelectedItem));
            cmd.Parameters.AddWithValue("@TripType", triptype.Text);
            cmd.Parameters.AddWithValue("@CapType", capacitytype.Text);
            cmd.Parameters.AddWithValue("@GroupSize", Convert.ToInt32(groupsize.Text));
            cmd.Parameters.AddWithValue("@Duration", Convert.ToInt32(duration.Text));
            cmd.Parameters.AddWithValue("@BasePrice", Convert.ToDecimal(baseprice.Text));

            // placeholders for fields not on the form
            cmd.Parameters.AddWithValue("@AccID", DBNull.Value);
            cmd.Parameters.AddWithValue("@RideID", DBNull.Value);
            cmd.Parameters.AddWithValue("@AccFlag", false);
            cmd.Parameters.AddWithValue("@RideFlag", false);
            cmd.Parameters.AddWithValue("@ActFlag", false);
            cmd.Parameters.AddWithValue("@SustainScore", 0);
        }

        private void ClearForm()
        {
            _selectedPackageId = -1;
            title.Clear();
            description.Clear();
            destination.SelectedIndex = 0;
            triptype.SelectedIndex = -1;
            capacitytype.SelectedIndex = -1;
            groupsize.Clear();
            duration.Clear();
            baseprice.Clear();
        }

        // other empty handlers you already had...
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void title_TextChanged(object sender, EventArgs e) { }
        private void description_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void capacitytype_SelectedIndexChanged(object sender, EventArgs e) { }
        private void triptype_SelectedIndexChanged(object sender, EventArgs e) { }
        private void groupsize_TextChanged(object sender, EventArgs e) { }
        private void duration_TextChanged(object sender, EventArgs e) { }
        private void baseprice_TextChanged(object sender, EventArgs e) { }
        private void Mytrips_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void addAccomodation_Click(object sender, EventArgs e)
        {
            if (_selectedPackageId < 0)
            {
                MessageBox.Show("Select a trip first.");
                return;
            }
            try
            {
                con.Open();
                // duplicate into Accommodation
                string copySql = @"
INSERT INTO Accommodation (HotelID, MaxCapacity, PricePerNight, RoomDesc)
SELECT DestinationID, GroupSize, BasePrice, Title + ' room'
FROM Package WHERE PackageID=@PID;
SELECT SCOPE_IDENTITY();";
                int newAccId;
                using (var cmd = new SqlCommand(copySql, con))
                {
                    cmd.Parameters.AddWithValue("@PID", _selectedPackageId);
                    newAccId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                // update Package
                using (var cmd2 = new SqlCommand(
                    "UPDATE Package SET AccommodationID=@AID, AccommodationStatusFlag=1 WHERE PackageID=@PID", con))
                {
                    cmd2.Parameters.AddWithValue("@AID", newAccId);
                    cmd2.Parameters.AddWithValue("@PID", _selectedPackageId);
                    cmd2.ExecuteNonQuery();
                }
                MessageBox.Show($"New accommodation (ID={newAccId}) created and assigned.");
                LoadMyTrips();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating accommodation: " + ex.Message);
            }
            finally { if (con.State == ConnectionState.Open) con.Close(); }
        }

        private void addRide_Click(object sender, EventArgs e)
        {
            if (_selectedPackageId < 0)
            {
                MessageBox.Show("Select a trip first.");
                return;
            }
            try
            {
                con.Open();
                // duplicate into Ride
                string copySql = @"
INSERT INTO Ride (TransportID, OriginID, DestinationID, Price, TimeRequested, ArrivalTime)
SELECT DestinationID, DestinationID, DestinationID, BasePrice, GETDATE(), DATEADD(hour, Duration, GETDATE())
FROM Package WHERE PackageID=@PID;
SELECT SCOPE_IDENTITY();";
                int newRideId;
                using (var cmd = new SqlCommand(copySql, con))
                {
                    cmd.Parameters.AddWithValue("@PID", _selectedPackageId);
                    newRideId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                // update Package
                using (var cmd2 = new SqlCommand(
                    "UPDATE Package SET RideID=@RID, RideStatusFlag=1 WHERE PackageID=@PID", con))
                {
                    cmd2.Parameters.AddWithValue("@RID", newRideId);
                    cmd2.Parameters.AddWithValue("@PID", _selectedPackageId);
                    cmd2.ExecuteNonQuery();
                }
                MessageBox.Show($"New ride (ID={newRideId}) created and assigned.");
                LoadMyTrips();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating ride: " + ex.Message);
            }
            finally { if (con.State == ConnectionState.Open) con.Close(); }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void mybookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
//booking and payment
