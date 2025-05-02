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
#pragma warning disable IDE0005
// The code that's violating the rule is on this line.
using System.Windows.Forms.DataVisualization.Charting;
#pragma warning restore IDE0005


namespace Hotel_and_Transport
{
    public partial class TransportDashboard : Form
    {
        private int _providerId;

        public TransportDashboard(int providerId)
        {
            InitializeComponent();
            _providerId = providerId;

        }

        private void TransportDashboard_Load(object sender, EventArgs e)
        {
            LoadServiceIntegration();
            LoadServiceListing();
            LoadBookingManagement();
            LoadPerformanceReports();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            TransportLogin form = new TransportLogin();
            form.Show();
            this.Hide();
        }
        private void LoadServiceIntegration()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                            SELECT DISTINCT r.RideID, l.CityName AS Origin, d.Name AS Destination, r.Price, 
                                   r.RideStatus, COALESCE(pkg.Duration, ct.Duration) AS Duration
                            FROM Ride r
                            JOIN TransportService ts ON r.TransportID = ts.TransportID
                            JOIN ServiceProvider sp ON ts.ProviderID = sp.ProviderID
                            JOIN Location l ON r.OriginID = l.LocationID
                            JOIN Destination d ON r.DestinationID = d.DestinationID
                            LEFT JOIN Package pkg ON r.RideID = pkg.RideID
                            LEFT JOIN CustomTrip ct ON r.RideID = ct.RideID
                            LEFT JOIN Request req ON pkg.PackageID = req.PackageID OR ct.CustomTripID = req.CustomTripID
                            WHERE sp.ProviderID = @ProviderID
                            AND sp.ProviderType = 3
                            AND r.RideStatus = 'Pending'
                            AND req.RequestID IS NOT NULL";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvTrips.DataSource = dt;
                    dgvTrips.Columns["RideID"].HeaderText = "Ride ID";
                    dgvTrips.Columns["Origin"].HeaderText = "Origin City";
                    dgvTrips.Columns["Destination"].HeaderText = "Destination";
                    dgvTrips.Columns["Price"].HeaderText = "Price ($)";
                    dgvTrips.Columns["RideStatus"].HeaderText = "Ride Status";
                    dgvTrips.Columns["Duration"].HeaderText = "Duration (Days)";
                    dgvTrips.Columns["Price"].DefaultCellStyle.Format = "N2";
                    foreach (DataGridViewColumn column in dgvTrips.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No pending trip assignments available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip assignments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Ride.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int RideID = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["RideID"].Value);
                string RideStatus = dgvTrips.SelectedRows[0].Cells["RideStatus"].Value?.ToString();
                if (RideStatus != "Pending")
                {
                    MessageBox.Show("Ride Status is not pending.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string updateRideQuery = @"
                        UPDATE Ride
                        SET RideStatus = 'Approved'
                        WHERE RideID = @RideID AND RideStatus = 'Pending'";
                    SqlCommand cmd = new SqlCommand(updateRideQuery, conn);
                    cmd.Parameters.AddWithValue("@RideID", RideID);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceIntegration();
                MessageBox.Show("Ride status approved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving Ride: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Ride.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int RideID = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["RideID"].Value);
                string RideStatus = dgvTrips.SelectedRows[0].Cells["RideStatus"].Value?.ToString();
                if (RideStatus != "Pending")
                {
                    MessageBox.Show("Ride Status is not pending.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string updateRideQuery = @"
                        UPDATE Ride
                        SET RideStatus = 'Rejected'
                        WHERE RideID = @RideID AND RideStatus = 'Pending'";
                    SqlCommand cmd = new SqlCommand(updateRideQuery, conn);
                    cmd.Parameters.AddWithValue("@RideID", RideID);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceIntegration();
                MessageBox.Show("Ride status Rejected!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Rejected Ride: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadServiceListing()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                          SELECT r.RideID, t.Type, r.Price, t.TotalSeats, 
                                sp.ProviderName, d.Name AS DestinationName, r.TimeRequested, r.ArrivalTime
                         FROM Ride r
                         JOIN TransportService t ON r.TransportID = t.TransportID
                         JOIN ServiceProvider sp ON t.ProviderID = sp.ProviderID
                         JOIN Destination d ON sp.DestinationID = d.DestinationID
                         JOIN Location l ON r.OriginID = l.LocationID
                         WHERE sp.ProviderID = @ProviderID AND sp.ProviderType = 3";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvServiceListing.DataSource = dt;
                    dgvServiceListing.Columns["RideID"].HeaderText = "Ride ID";
                    dgvServiceListing.Columns["Type"].HeaderText = "Transport Type";
                    dgvServiceListing.Columns["Price"].HeaderText = "Price Per Person ($)";
                    dgvServiceListing.Columns["TotalSeats"].HeaderText = "Max Capacity";
                    dgvServiceListing.Columns["ProviderName"].HeaderText = "Name";
                    dgvServiceListing.Columns["DestinationName"].HeaderText = "Destination";
                    dgvServiceListing.Columns["TimeRequested"].HeaderText = "Time Requested";
                    dgvServiceListing.Columns["ArrivalTime"].HeaderText = "Arrival Time";
                    dgvServiceListing.Columns["Price"].DefaultCellStyle.Format = "N2";
                    //dgvServiceListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    foreach (DataGridViewColumn column in dgvServiceListing.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Rides: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditArrival_Click(object sender, EventArgs e)
        {
            if (dgvServiceListing.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a ride to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int RideID = Convert.ToInt32(dgvServiceListing.SelectedRows[0].Cells["RideID"].Value);
                string RideArrival = dgvServiceListing.SelectedRows[0].Cells["ArrivalTime"].Value?.ToString();

                // Open edit form
                EditRideArrival editForm = new EditRideArrival(RideID, RideArrival);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadServiceListing(); // Refresh grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening arrival time edit edit form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void price_Click(object sender, EventArgs e)
        {
            if (dgvServiceListing.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a ride to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int RideID = Convert.ToInt32(dgvServiceListing.SelectedRows[0].Cells["RideID"].Value);
                decimal price = Convert.ToDecimal(dgvServiceListing.SelectedRows[0].Cells["price"].Value);

                // Open edit form
                EditRidePrice editForm = new EditRidePrice(RideID, price);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadServiceListing(); // Refresh grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening price edit form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadBookingManagement()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                         SELECT b.RequestID, b.BookingDate, d.Name as Destination,l.CityName as Origin ,COALESCE(pkg.Duration, ct.Duration) AS Duration, 
                                (COALESCE(pkg.GroupSize, ct.GroupSize) * r.Price) AS TotalCost,
                                COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize,
                                req.RidePaidStatus
                         FROM Booking b
                         JOIN Request req ON b.RequestID = req.RequestID
                         LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                         LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                         JOIN Ride r ON pkg.RideID = r.RideID OR ct.RideID = r.RideID
                         JOIN TransportService ts ON r.TransportID = ts.TransportID
                         JOIN ServiceProvider sp ON ts.ProviderID = sp.ProviderID
                         JOIN Location l ON r.OriginID = l.LocationID
                         JOIN Destination d ON d.DestinationID = r.DestinationID 
                         WHERE sp.ProviderID = @ProviderID AND sp.ProviderType = 3";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBookings.DataSource = dt;
                    dgvBookings.Columns["RequestID"].HeaderText = "Request ID";
                    dgvBookings.Columns["BookingDate"].HeaderText = "Booking Date";
                    dgvBookings.Columns["Destination"].HeaderText = "Destination";
                    dgvBookings.Columns["Origin"].HeaderText = "PickUp Location";
                    dgvBookings.Columns["Destination"].HeaderText = "Destination";
                    dgvBookings.Columns["TotalCost"].HeaderText = "Total Cost ($)";
                    dgvBookings.Columns["GroupSize"].HeaderText = "Group Size";
                    dgvBookings.Columns["RidePaidStatus"].HeaderText = "Ride Payment Status";
                    //dgvBookings.Columns["ActivityPaidStatus"].HeaderText = "Activity Payment Status";
                    dgvBookings.Columns["TotalCost"].DefaultCellStyle.Format = "N2"; // for 2 d.p
                    dgvBookings.Columns["BookingDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                    foreach (DataGridViewColumn column in dgvBookings.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bookings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int requestId = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells["RequestID"].Value);
                string ridePaidStatus = dgvBookings.SelectedRows[0].Cells["RidePaidStatus"].Value?.ToString();
                if (ridePaidStatus == "Paid")
                {
                    MessageBox.Show("Payment already confirmed for Request ID " + requestId + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    // Get GroupSize, TransportID, and TotalSeats
                    string detailsQuery = @"
                            SELECT COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize, ts.TransportID, ts.TotalSeats, ts.OccupiedSeats
                            FROM Booking b
                            JOIN Request req ON b.RequestID = req.RequestID
                            LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                            LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                            JOIN Ride r ON pkg.RideID = r.RideID OR ct.RideID = r.RideID
                            JOIN TransportService ts ON r.TransportID = ts.TransportID
                            WHERE req.RequestID = @RequestID";
                    SqlCommand cmd = new SqlCommand(detailsQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        MessageBox.Show("Booking details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int groupSize = Convert.ToInt32(reader["GroupSize"]);
                    int transportId = Convert.ToInt32(reader["TransportID"]);
                    int totalSeats = Convert.ToInt32(reader["TotalSeats"]);
                    int occupiedSeats = Convert.ToInt32(reader["OccupiedSeats"]);
                    reader.Close();

                    // Check seat availability
                    if (totalSeats < occupiedSeats + groupSize)
                    {
                        MessageBox.Show("Not enough seats available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update RidePaidStatus in Request
                    string updateRequestQuery = @"
                UPDATE Request
                SET RidePaidStatus = 'Paid'
                WHERE RequestID = @RequestID AND RidePaidStatus = 'Unpaid'";
                    cmd = new SqlCommand(updateRequestQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Payment already confirmed or request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update TransportService.OccupiedSeats
                    string updateTransportQuery = @"
                UPDATE TransportService
                SET OccupiedSeats = OccupiedSeats + @GroupSize
                WHERE TransportID = @TransportID";
                    cmd = new SqlCommand(updateTransportQuery, conn);
                    cmd.Parameters.AddWithValue("@TransportID", transportId);
                    cmd.Parameters.AddWithValue("@GroupSize", groupSize);
                    cmd.ExecuteNonQuery();
                }
                LoadBookingManagement();
                MessageBox.Show("Payment confirmed and seats allocated for Request ID " + requestId + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelAllocation_Click(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int requestId = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells["RequestID"].Value);
                string ridePaidStatus = dgvBookings.SelectedRows[0].Cells["RidePaidStatus"].Value?.ToString();
                if (ridePaidStatus != "Paid")
                {
                    MessageBox.Show("No payment confirmed to cancel for Request ID " + requestId + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    // Get GroupSize and TransportID
                    string detailsQuery = @"
                SELECT COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize, ts.TransportID, ts.OccupiedSeats
                FROM Booking b
                JOIN Request req ON b.RequestID = req.RequestID
                LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                JOIN Ride r ON pkg.RideID = r.RideID OR ct.RideID = r.RideID
                JOIN TransportService ts ON r.TransportID = ts.TransportID
                WHERE req.RequestID = @RequestID";
                    SqlCommand cmd = new SqlCommand(detailsQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        MessageBox.Show("Booking details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int groupSize = Convert.ToInt32(reader["GroupSize"]);
                    int transportId = Convert.ToInt32(reader["TransportID"]);
                    int occupiedSeats = Convert.ToInt32(reader["OccupiedSeats"]);
                    reader.Close();

                    // Validate sufficient OccupiedSeats
                    if (occupiedSeats < groupSize)
                    {
                        MessageBox.Show("Invalid seat allocation to cancel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update RidePaidStatus to Unpaid
                    string updateRequestQuery = @"
                UPDATE Request
                SET RidePaidStatus = 'Unpaid'
                WHERE RequestID = @RequestID AND RidePaidStatus = 'Paid'";
                    cmd = new SqlCommand(updateRequestQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("No payment confirmed or request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Decrease TransportService.OccupiedSeats
                    string updateTransportQuery = @"
                UPDATE TransportService
                SET OccupiedSeats = OccupiedSeats - @GroupSize
                WHERE TransportID = @TransportID AND OccupiedSeats >= @GroupSize";
                    cmd = new SqlCommand(updateTransportQuery, conn);
                    cmd.Parameters.AddWithValue("@TransportID", transportId);
                    cmd.Parameters.AddWithValue("@GroupSize", groupSize);
                    cmd.ExecuteNonQuery();
                }
                LoadBookingManagement();
                MessageBox.Show("Payment and seat allocation cancelled for Request ID " + requestId + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPerformanceReports()
        {
            try
            {
                // Populate ComboBox
                cmbGraphType.Items.Clear();
                cmbGraphType.Items.AddRange(new string[] { "Seat Occupancy", "Ratings", "Revenue", "On-Time Arrivals" });
                cmbGraphType.SelectedIndex = 0; // Default to Seat Occupancy

                // Initial chart load
                UpdateChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading performance reports: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbGraphType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }


        private void UpdateChart()
        {
            try
            {
                chartPerformance.Series.Clear();
                chartPerformance.ChartAreas.Clear();
                chartPerformance.Titles.Clear();

                ChartArea chartArea = new ChartArea();
                chartPerformance.ChartAreas.Add(chartArea);

                Series series = new Series("Performance");
                chartPerformance.Series.Add(series);

                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string selectedGraph = cmbGraphType.SelectedItem.ToString();

                    if (selectedGraph == "Seat Occupancy")
                    {
                        chartPerformance.Titles.Add("Seat Occupancy");
                        series.ChartType = SeriesChartType.Pie;

                        string query = @"
                    SELECT SUM(OccupiedSeats) AS OccupiedSeats, SUM(TotalSeats - OccupiedSeats) AS AvailableSeats
                    FROM TransportService
                    WHERE ProviderID = @ProviderID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int occupied = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            int available = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            series.Points.AddXY("Occupied", occupied);
                            series.Points.AddXY("Available", available);
                            foreach (DataPoint point in series.Points)
                            {
                                point.Label = "#PERCENT{P0}";
                                point.LegendText = point.AxisLabel;
                            }
                        }
                        reader.Close();
                    }
                    else if (selectedGraph == "Ratings")
                    {
                        chartPerformance.Titles.Add("Rating Distribution");
                        series.ChartType = SeriesChartType.Column;
                        chartArea.AxisX.Title = "Rating | Stars";
                        chartArea.AxisY.Title = "Number of Ratings";

                        // Initialize ratings (1-5 stars)
                        DataTable ratings = new DataTable();
                        ratings.Columns.Add("Rating", typeof(int));
                        ratings.Columns.Add("Count", typeof(int));
                        for (int i = 1; i <= 5; i++)
                        {
                            ratings.Rows.Add(i, 0);
                        }

                        // Fetch ratings from database
                        string query = @"
                    SELECT r.Rating, COUNT(*) AS Count
                    FROM Review r
                    JOIN Ride ri ON r.RideID = ri.RideID
                    JOIN TransportService ts ON ri.TransportID = ts.TransportID
                    WHERE ts.ProviderID = @ProviderID
                    AND r.ModerationStatus = 'Approved'
                    GROUP BY r.Rating";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dbRatings = new DataTable();
                        adapter.Fill(dbRatings);

                        foreach (DataRow dbRow in dbRatings.Rows)
                        {
                            int rating = Convert.ToInt32(dbRow["Rating"]);
                            int count = Convert.ToInt32(dbRow["Count"]);
                            DataRow row = ratings.Select("Rating = " + rating).FirstOrDefault();
                            if (row != null)
                                row["Count"] = count;
                        }

                        foreach (DataRow row in ratings.Rows)
                        {
                            series.Points.AddXY(row["Rating"], row["Count"]);
                        }
                    }
                    else if (selectedGraph == "Revenue")
                    {
                        chartPerformance.Titles.Add("Monthly Revenue");
                        series.ChartType = SeriesChartType.Line;
                        chartArea.AxisX.Title = "Month";
                        chartArea.AxisY.Title = "Revenue | $";
                        chartArea.AxisY.LabelStyle.Format = "$#,##0";
                        series.MarkerStyle = MarkerStyle.Circle;
                        series.MarkerSize = 8;

                        string query = @"
                    SELECT 
                        FORMAT(b.BookingDate, 'yyyy-MM') AS Month,
                        SUM(COALESCE(pkg.GroupSize, ct.GroupSize) * ri.Price) AS TotalRevenue
                    FROM Booking b
                    JOIN Request req ON b.RequestID = req.RequestID
                    LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                    LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                    JOIN Ride ri ON pkg.RideID = ri.RideID OR ct.RideID = ri.RideID
                    JOIN TransportService ts ON ri.TransportID = ts.TransportID
                    WHERE ts.ProviderID = @ProviderID
                    AND req.RidePaidStatus = 'Paid'
                    AND b.BookingDate >= DATEADD(MONTH, -12, GETDATE())
                    GROUP BY FORMAT(b.BookingDate, 'yyyy-MM')
                    ORDER BY Month";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string month = reader.GetString(0);
                            double revenue = reader.IsDBNull(1) ? 0 : (double)reader.GetDecimal(1);
                            series.Points.AddXY(month, revenue);
                        }
                        reader.Close();
                    }

                    else if (selectedGraph == "On-Time Arrivals")
                    {
                        chartPerformance.Titles.Add("On-Time Arrivals");
                        series.ChartType = SeriesChartType.Pie;

                        string query = @"
                    SELECT 
                        SUM(CASE WHEN DATEDIFF(MINUTE, TimeRequested, ArrivalTime) <= 30 THEN 1 ELSE 0 END) AS OnTime,
                        SUM(CASE WHEN DATEDIFF(MINUTE, TimeRequested, ArrivalTime) > 30 THEN 1 ELSE 0 END) AS Delayed
                    FROM Ride r
                    JOIN TransportService ts ON r.TransportID = ts.TransportID
                    WHERE ts.ProviderID = @ProviderID
                    AND r.ArrivalTime IS NOT NULL";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int onTime = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            int delayed = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            series.Points.AddXY("On-Time", onTime);
                            series.Points.AddXY("Delayed", delayed);
                            foreach (DataPoint point in series.Points)
                            {
                                point.Label = "#PERCENT{P0}";
                                point.LegendText = point.AxisLabel;
                            }
                        }
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
