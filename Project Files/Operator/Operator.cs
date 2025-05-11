using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.IdentityModel.Tokens;

namespace TourOperator
{ 
    public partial class Operator : Form
    {
        private int _operatorId;
        private int _selectedPackageId = -1;
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
            "Initial Catalog=\"Travel ease2\";" +
            "Integrated Security=True;Encrypt=False");

        public Operator(int operator_Id)
        {
            InitializeComponent();
            _operatorId = operator_Id;

            
            ADDTRIP.Click += ADDTRIP_Click;
            updateTrip.Click += updateTrip_Click;
            deleteTrip.Click += deleteTrip_Click;
            Mytrips.CellClick += Mytrips_CellClick;
            AddItineraries.Click += AddItineraries_Click; // Add this line
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // populate dropdowns
            destination.Items.Clear();
            for (int i = 1; i <= 51; i++)
                destination.Items.Add(i.ToString());
            destination.SelectedIndex = 0;

          

            capacitytype.Items.AddRange(new[] { "Solo", "Group" });
            capacitytype.SelectedIndex = -1;

            // load everything
            LoadMyTrips();
            LoadAccommodations();
            LoadRides();
            LoadRequests();
            LoadReportComboBox();       //Updated
            LoadExistingItineraries();  // New
            LoadGuides();               //  New
            LoadTripCategories();       // New

            //   OperatorReports_Load();
        }
        private void LoadTripCategories()
        {
            triptype.Items.Clear();
            try
            {
                const string sql = "SELECT TripType FROM TripCategory";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            triptype.Items.Add(reader["TripType"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip types: " + ex.Message);
                if (con.State == ConnectionState.Open) con.Close();
            }
            triptype.SelectedIndex = -1;
        }
        private void LoadReportComboBox()
        {
            reportComboBox.Items.Add("Booking Rates");
            reportComboBox.Items.Add("Revenue");
            reportComboBox.Items.Add("Review Summary");
            reportComboBox.Items.Add("Response Time"); // Added Response Time option
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
            // Add validation for Solo capacity type
            if (capacitytype.SelectedItem?.ToString() == "Solo" && grp != 1)
            {
                MessageBox.Show("Group Size must be exactly 1 for Solo trips.");
                return false;
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


        private void LoadBookingRatesChart()
        {
            string query = @"
            SELECT FORMAT(b.BookingDate, 'yyyy-MM') AS Month, COUNT(*) AS Bookings
            FROM Booking b
            INNER JOIN Request r ON r.RequestID = b.RequestID
            WHERE r.OperatorID = @OperatorID
              AND b.BookingStatus = 'Confirmed'
              AND r.RequestStatus = 'Accepted'
              AND b.CancellationReason IS NULL
            GROUP BY FORMAT(b.BookingDate, 'yyyy-MM')
            ORDER BY Month";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
            "Initial Catalog=\"Travel ease2\";" +
            "Integrated Security=True;Encrypt=False"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OperatorID", _operatorId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                reportChart.Series.Clear();
                Series series = new Series("Bookings");
                series.ChartType = SeriesChartType.Column;

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["Month"], row["Bookings"]);
                }

                reportChart.Series.Add(series);
                reportChart.Titles.Clear();
                reportChart.Titles.Add("Monthly Confirmed Booking Rates");
            }
        }

        private void LoadRevenueChart()
        {
            string query = @"
        SELECT FORMAT(b.BookingDate, 'yyyy') AS Year, SUM(b.TotalCost) AS Revenue
        FROM Booking b
        INNER JOIN Request r ON r.RequestID = b.RequestID
        WHERE r.OperatorID = @OperatorID
          AND b.BookingStatus = 'Confirmed'
          AND r.RequestStatus = 'Accepted'
          AND b.CancellationReason IS NULL
        GROUP BY FORMAT(b.BookingDate, 'yyyy')
        ORDER BY Year";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
                "Initial Catalog=\"Travel ease2\";" +
                "Integrated Security=True;Encrypt=False"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OperatorID", _operatorId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                reportChart.Series.Clear();
                Series series = new Series("Yearly Revenue");
                series.ChartType = SeriesChartType.Column;
                series.Color = System.Drawing.Color.SeaGreen;
                series.BorderWidth = 2;

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["Year"].ToString(), Convert.ToDouble(row["Revenue"]));
                }

                reportChart.Series.Add(series);
                reportChart.Titles.Clear();
                reportChart.Titles.Add("Yearly Revenue from Confirmed Bookings");
            }
        }




        private void LoadReviewSummaryChart()
        {
            string query = @"
        SELECT Rating, COUNT(*) AS TotalReviews
        FROM Review
        WHERE OperatorID = @OperatorID AND ModerationStatus = 'Approved'
        GROUP BY Rating
        ORDER BY Rating";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
                 "Initial Catalog=\"Travel ease2\";" +
                 "Integrated Security=True;Encrypt=False"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OperatorID", _operatorId);

                SqlDataAdapter da = new SqlDataAdapter(cmd); // ✅ FIXED
                DataTable dt = new DataTable();
                da.Fill(dt);

                reportChart.Series.Clear();
                Series series = new Series("Review Ratings");
                series.ChartType = SeriesChartType.Pie;

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY("Rating " + row["Rating"], row["TotalReviews"]);
                }

                reportChart.Series.Add(series);
                reportChart.Titles.Clear();
                reportChart.Titles.Add("Customer Review Distribution");
            }
        }



        private void reportComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReport = reportComboBox.SelectedItem.ToString();

            switch (selectedReport)
            {
                case "Booking Rates":
                    LoadBookingRatesChart();
                    break;
                case "Revenue":
                    LoadRevenueChart();
                    break;
                case "Review Summary":
                    LoadReviewSummaryChart();
                    break;
                case "Response Time":
                    LoadResponseTimeChart();
                    break;
                default:
                    break;
            }
        }
        private void LoadGuides()
        {
            try
            {
                const string sql = @"
            SELECT s.ProviderName, g.GuideID 
            FROM Guide g
            INNER JOIN ServiceProvider s ON s.ProviderID = g.ProviderID
            WHERE s.ProviderType = 2";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        GuideComboBox.Items.Clear();
                        while (reader.Read())
                        {
                            // Add ProviderName to the combo box, store ProviderID in Tag
                            GuideComboBox.Items.Add(new ComboBoxItem
                            {
                                Text = reader["ProviderName"].ToString(),
                                Value = (int)reader["GuideID"]
                            });
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading guides: " + ex.Message);
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // Helper class to store both Text and Value in the combo box
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        private void AddItineraries_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that a package is selected
                if (_selectedPackageId < 0)
                {
                    MessageBox.Show("Please select a trip from the Mytrips grid first.");
                    return;
                }

                // Validate form inputs
                if (string.IsNullOrWhiteSpace(ActivityName.Text))
                {
                    MessageBox.Show("Please enter an Activity Name.");
                    return;
                }
                if (!decimal.TryParse(Price.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Price must be a valid number ≥ 0.");
                    return;
                }
                if (GuideComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a Guide.");
                    return;
                }
                if (!int.TryParse(CurrentParticipants.Text, out int currentParticipants) || currentParticipants < 0)
                {
                    MessageBox.Show("Current Participants must be a valid number ≥ 0.");
                    return;
                }
                if (!DateTime.TryParse(StartTime.Text, out DateTime startTime))
                {
                    MessageBox.Show("Please enter a valid Start Time.");
                    return;
                }
                if (!DateTime.TryParse(EndTime.Text, out DateTime endTime))
                {
                    MessageBox.Show("Please enter a valid End Time.");
                    return;
                }
                if (endTime <= startTime)
                {
                    MessageBox.Show("End Time must be after Start Time.");
                    return;
                }

                // Get the selected Guide's ProviderID
                var selectedGuide = (ComboBoxItem)GuideComboBox.SelectedItem;
                int guideId = selectedGuide.Value;

                // SQL query to insert into Activity table and get the new ActivityID
                const string insertActivitySql = @"
        INSERT INTO Activity (ActivityName, GuideID, StartTime, EndTime, CapacityLimit, Price, ActivityStatus, CurrentParticipants)
        VALUES (@ActivityName, @GuideID, @StartTime, @EndTime, @CapacityLimit, @Price, @ActivityStatus, @CurrentParticipants);
        SELECT SCOPE_IDENTITY();";

                int newActivityId;
                using (SqlCommand cmd = new SqlCommand(insertActivitySql, con))
                {
                    cmd.Parameters.AddWithValue("@ActivityName", ActivityName.Text.Trim());
                    cmd.Parameters.AddWithValue("@GuideID", guideId);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.Parameters.AddWithValue("@CapacityLimit", currentParticipants + 10);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@ActivityStatus", "Pending");
                    cmd.Parameters.AddWithValue("@CurrentParticipants", currentParticipants);

                    con.Open();
                    newActivityId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }

                // Insert into PackageActivities table with the selected PackageID and new ActivityID
                const string insertPackageActivitiesSql = @"
        INSERT INTO PackageActivities (PackageID, ActivityID)
        VALUES (@PackageID, @ActivityID);";

                using (SqlCommand cmd = new SqlCommand(insertPackageActivitiesSql, con))
                {
                    cmd.Parameters.AddWithValue("@PackageID", _selectedPackageId);
                    cmd.Parameters.AddWithValue("@ActivityID", newActivityId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Activity added successfully and linked to the selected package.");
                // Clear the form fields after adding
                ActivityName.Clear();
                Price.Clear();
                GuideComboBox.SelectedIndex = -1;
                CurrentParticipants.Clear();
                StartTime.Clear();
                EndTime.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding activity or linking to package: " + ex.Message);
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void LoadExistingItineraries()
        {
            try
            {
                const string sql = @"
        SELECT ActivityID, ActivityName, GuideID, StartTime, EndTime, 
               CapacityLimit, Price, CurrentParticipants 
        FROM Activity";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        existingItineraries.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading existing itineraries: " + ex.Message);
            }
        }
   
        private void AddExistingItineraryButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Validate that a package is selected
                if (_selectedPackageId < 0)
                {
                    MessageBox.Show("Please select a trip from the Mytrips grid first.");
                    return;
                }

                // Validate that a row is selected in existingItineraries
                if (existingItineraries.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an activity from the existing itineraries grid.");
                    return;
                }

                // Get the selected activity row
                DataGridViewRow selectedRow = existingItineraries.SelectedRows[0];
                string activityName = selectedRow.Cells["ActivityName"].Value.ToString();
                int guideId = Convert.ToInt32(selectedRow.Cells["GuideID"].Value);
                DateTime startTime = Convert.ToDateTime(selectedRow.Cells["StartTime"].Value);
                DateTime endTime = Convert.ToDateTime(selectedRow.Cells["EndTime"].Value);
                int capacityLimit = Convert.ToInt32(selectedRow.Cells["CapacityLimit"].Value);
                decimal price = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
                int currentParticipants = Convert.ToInt32(selectedRow.Cells["CurrentParticipants"].Value);

                // Duplicate the activity in the Activity table
                const string insertActivitySql = @"
        INSERT INTO Activity (ActivityName, GuideID, StartTime, EndTime, CapacityLimit, Price, ActivityStatus, CurrentParticipants)
        VALUES (@ActivityName, @GuideID, @StartTime, @EndTime, @CapacityLimit, @Price, @ActivityStatus, @CurrentParticipants);
        SELECT SCOPE_IDENTITY();";

                int newActivityId;
                using (SqlCommand cmd = new SqlCommand(insertActivitySql, con))
                {
                    cmd.Parameters.AddWithValue("@ActivityName", activityName);
                    cmd.Parameters.AddWithValue("@GuideID", guideId);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.Parameters.AddWithValue("@CapacityLimit", capacityLimit);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@ActivityStatus", "Pending");
                    cmd.Parameters.AddWithValue("@CurrentParticipants", currentParticipants);

                    con.Open();
                    newActivityId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }

                // Insert into PackageActivities table with the selected PackageID and new ActivityID
                const string insertPackageActivitiesSql = @"
        INSERT INTO PackageActivities (PackageID, ActivityID)
        VALUES (@PackageID, @ActivityID);";

                using (SqlCommand cmd = new SqlCommand(insertPackageActivitiesSql, con))
                {
                    cmd.Parameters.AddWithValue("@PackageID", _selectedPackageId);
                    cmd.Parameters.AddWithValue("@ActivityID", newActivityId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Existing activity duplicated and linked to the selected package successfully.");
                LoadExistingItineraries(); // Refresh the grid to show the new activity
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error duplicating activity or linking to package: " + ex.Message);
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void LoadRequests()
        {
            try
            {
                const string sql = "SELECT b.BookingID, b.BookingStatus, r.TripSourceType, b.BookingDate " +
                                  "FROM Booking b " +
                                  "INNER JOIN Request r ON r.RequestID = b.RequestID " +
                                  "WHERE OperatorID = @OperatorID AND b.BookingStatus = 'Pending'";
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

        private void ConfirmBookingButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (mybookings.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a booking to confirm.");
                    return;
                }

                int bookingId = Convert.ToInt32(mybookings.SelectedRows[0].Cells["BookingID"].Value);
                // Update Booking status and set BookingDate to current date
                const string updateBookingSql = @"
            UPDATE Booking 
            SET BookingStatus = 'Confirmed', BookingDate = GETDATE() 
            WHERE BookingID = @BookingID AND BookingStatus = 'Pending'";
                // Sync Request status
                const string updateRequestSql = @"
            UPDATE Request 
            SET RequestStatus = 'Accepted' 
            WHERE RequestID = (SELECT RequestID FROM Booking WHERE BookingID = @BookingID)";

                using (var cmd = new SqlCommand(updateBookingSql, con))
                {
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Execute the request status update
                        using (var cmdRequest = new SqlCommand(updateRequestSql, con))
                        {
                            cmdRequest.Parameters.AddWithValue("@BookingID", bookingId);
                            cmdRequest.ExecuteNonQuery();
                        }
                        MessageBox.Show("Booking confirmed successfully.");
                        LoadRequests(); // Refresh the grid to show only pending bookings
                    }
                    else
                    {
                        MessageBox.Show("Booking could not be confirmed (already processed or invalid).");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming booking: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void rejectBookingButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (mybookings.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a booking to reject.");
                    return;
                }

                int bookingId = Convert.ToInt32(mybookings.SelectedRows[0].Cells["BookingID"].Value);
                // Update Booking status and set BookingDate to current date
                const string updateBookingSql = @"
            UPDATE Booking 
            SET BookingStatus = 'Rejected', BookingDate = GETDATE() 
            WHERE BookingID = @BookingID AND BookingStatus = 'Pending'";
                // Sync Request status
                const string updateRequestSql = @"
            UPDATE Request 
            SET RequestStatus = 'Rejected' 
            WHERE RequestID = (SELECT RequestID FROM Booking WHERE BookingID = @BookingID)";

                using (var cmd = new SqlCommand(updateBookingSql, con))
                {
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Execute the request status update
                        using (var cmdRequest = new SqlCommand(updateRequestSql, con))
                        {
                            cmdRequest.Parameters.AddWithValue("@BookingID", bookingId);
                            cmdRequest.ExecuteNonQuery();
                        }
                        MessageBox.Show("Booking rejected successfully.");
                        LoadRequests(); // Refresh the grid to show only pending bookings
                    }
                    else
                    {
                        MessageBox.Show("Booking could not be rejected (already processed or invalid).");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error rejecting booking: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private void LoadResponseTimeChart()
        {
            string query = @"
        SELECT 
            AVG(DATEDIFF(MINUTE, r.DateRequested, b.BookingDate)) / 1440 AS AverageResponseTimeDays,
            AVG(DATEDIFF(MINUTE, r.DateRequested, b.BookingDate)) % 1440 AS AverageResponseTimeMinutes
        FROM Request r
        JOIN Booking b ON r.RequestID = b.RequestID
        WHERE 
            r.OperatorID = @OperatorID
            AND b.BookingStatus IN ('Confirmed', 'Rejected')
            AND r.RequestStatus IN ('Accepted', 'Rejected')";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
                 "Initial Catalog=\"Travel ease2\";" +
                 "Integrated Security=True;Encrypt=False"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OperatorID", _operatorId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                reportChart.Series.Clear();
                Series seriesDays = new Series("Average Response Time (Days)");
                seriesDays.ChartType = SeriesChartType.Column;
                seriesDays.Color = System.Drawing.Color.Blue;

                Series seriesMinutes = new Series("Average Response Time (Minutes)");
                seriesMinutes.ChartType = SeriesChartType.Column;
                seriesMinutes.Color = System.Drawing.Color.Green;

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    seriesDays.Points.AddXY("Days", Convert.ToDouble(row["AverageResponseTimeDays"]));
                    seriesMinutes.Points.AddXY("Minutes", Convert.ToDouble(row["AverageResponseTimeMinutes"]));
                }

                reportChart.Series.Add(seriesDays);
                reportChart.Series.Add(seriesMinutes);
                reportChart.Titles.Clear();
                reportChart.Titles.Add("Average Response Time for Operator");
            }
        }
    }
}
//booking and payment