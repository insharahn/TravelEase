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
    public partial class HotelDashboard : Form
    {
        private int _providerId;
        public HotelDashboard(int providerId)
        {
            InitializeComponent();
            _providerId = providerId;
        }
        // Ensure the default constructor is removed or calls the parameterized constructor:
        public HotelDashboard() : this(0) { } // Fallback for testing

        private void HotelDashboard_Load(object sender, EventArgs e)
        {
            LoadServiceIntegration();
            LoadServiceListing();
             LoadBookingManagement();
            LoadPerformanceReports();
        }
        private void LoadServiceIntegration()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT a.AccomodationID, a.RoomDesc, a.MaxCapacity, a.PricePerNight, 
                               a.AccommodationStatus, COALESCE(pkg.Duration, ct.Duration) AS Duration
                        FROM Accommodation a
                        JOIN Hotel h ON a.HotelID = h.HotelID
                        JOIN ServiceProvider sp ON h.ProviderID = sp.ProviderID
                        LEFT JOIN Package pkg ON a.AccomodationID = pkg.AccommodationID
                        LEFT JOIN CustomTrip ct ON a.AccomodationID = ct.AccommodationID
                        LEFT JOIN Request r ON pkg.PackageID = r.PackageID OR ct.CustomTripID = r.CustomTripID
                        WHERE sp.ProviderID = @ProviderID AND sp.ProviderType = 1 
                        AND a.AccommodationStatus = 'Pending' 
                        AND r.RequestID IS NOT NULL";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvAssignments.DataSource = dt;
                    dgvAssignments.Columns["AccomodationID"].HeaderText = "Accommodation ID";
                    dgvAssignments.Columns["RoomDesc"].HeaderText = "Room Description";
                    dgvAssignments.Columns["MaxCapacity"].HeaderText = "Max Capacity";
                    dgvAssignments.Columns["PricePerNight"].HeaderText = "Price Per Night ($)";
                    dgvAssignments.Columns["AccommodationStatus"].HeaderText = "Accommodation Status";
                    dgvAssignments.Columns["Duration"].HeaderText = "Duration (Days)";
                    dgvAssignments.Columns["PricePerNight"].DefaultCellStyle.Format = "N2";
                    //  dgvAssignments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    foreach (DataGridViewColumn column in dgvAssignments.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accommodations: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an accommodation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int accomodationId = Convert.ToInt32(dgvAssignments.SelectedRows[0].Cells["AccomodationID"].Value);
                string accommodationStatus = dgvAssignments.SelectedRows[0].Cells["AccommodationStatus"].Value?.ToString();
                if (accommodationStatus != "Pending")
                {
                    MessageBox.Show("Accommodation status is not pending.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string updateAccommodationQuery = @"
                        UPDATE Accommodation
                        SET AccommodationStatus = 'Approved'
                        WHERE AccomodationID = @AccomodationID AND AccommodationStatus = 'Pending'";
                    SqlCommand cmd = new SqlCommand(updateAccommodationQuery, conn);
                    cmd.Parameters.AddWithValue("@AccomodationID", accomodationId);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceIntegration();
                MessageBox.Show("Accommodation status approved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving accommodation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an accommodation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int accomodationId = Convert.ToInt32(dgvAssignments.SelectedRows[0].Cells["AccomodationID"].Value);
                string accommodationStatus = dgvAssignments.SelectedRows[0].Cells["AccommodationStatus"].Value?.ToString();
                if (accommodationStatus != "Pending")
                {
                    MessageBox.Show("Accommodation status is not pending.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string updateAccommodationQuery = @"
                        UPDATE Accommodation
                        SET AccommodationStatus = 'Rejected'
                        WHERE AccomodationID = @AccomodationID AND AccommodationStatus = 'Pending'";
                    SqlCommand cmd = new SqlCommand(updateAccommodationQuery, conn);
                    cmd.Parameters.AddWithValue("@AccomodationID", accomodationId);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceIntegration();
                MessageBox.Show("Accommodation status rejected!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error rejecting accommodation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /////////////////       SERVICE LISTING       ////////////////
        private void LoadServiceListing()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                        SELECT a.AccomodationID, a.RoomDesc, a.PricePerNight, a.MaxCapacity, 
                               sp.ProviderName AS HotelName, d.Name AS DestinationName
                        FROM Accommodation a
                        JOIN Hotel h ON a.HotelID = h.HotelID
                        JOIN ServiceProvider sp ON h.ProviderID = sp.ProviderID
                        JOIN Destination d ON sp.DestinationID = d.DestinationID
                        WHERE sp.ProviderID = @ProviderID AND sp.ProviderType = 1";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvServiceListing.DataSource = dt;
                    dgvServiceListing.Columns["AccomodationID"].HeaderText = "Accommodation ID";
                    dgvServiceListing.Columns["RoomDesc"].HeaderText = "Room Description";
                    dgvServiceListing.Columns["PricePerNight"].HeaderText = "Price Per Night ($)";
                    dgvServiceListing.Columns["MaxCapacity"].HeaderText = "Max Capacity";
                    dgvServiceListing.Columns["HotelName"].HeaderText = "Hotel Name";
                    dgvServiceListing.Columns["DestinationName"].HeaderText = "Destination";
                   dgvServiceListing.Columns["PricePerNight"].DefaultCellStyle.Format = "N2";
                    //dgvServiceListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    foreach (DataGridViewColumn column in dgvServiceListing.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accommodations: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        SELECT b.RequestID, b.BookingDate, COALESCE(pkg.Duration, ct.Duration) AS Duration, 
                               (COALESCE(pkg.Duration, ct.Duration) * a.PricePerNight) AS TotalCost,
                               COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize,
                               req.AccomodationPaidStatus
                        FROM Booking b
                        JOIN Request req ON b.RequestID = req.RequestID
                        LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                        LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                        JOIN Accommodation a ON pkg.AccommodationID = a.AccomodationID OR ct.AccommodationID = a.AccomodationID
                        JOIN Hotel h ON a.HotelID = h.HotelID
                        JOIN ServiceProvider sp ON h.ProviderID = sp.ProviderID
                        WHERE sp.ProviderID = @ProviderID AND sp.ProviderType = 1";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBookings.DataSource = dt;
                    dgvBookings.Columns["RequestID"].HeaderText = "Request ID";
                    dgvBookings.Columns["BookingDate"].HeaderText = "Booking Date";
                    dgvBookings.Columns["Duration"].HeaderText = "Duration (Days)";
                    dgvBookings.Columns["TotalCost"].HeaderText = "Total Cost ($)";
                    dgvBookings.Columns["GroupSize"].HeaderText = "Group Size";
                    dgvBookings.Columns["AccomodationPaidStatus"].HeaderText = "Accommodation Payment Status";
                    //dgvBookings.Columns["RidePaidStatus"].HeaderText = "Ride Payment Status";
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
                string accomodationPaidStatus = dgvBookings.SelectedRows[0].Cells["AccomodationPaidStatus"].Value?.ToString();
                if (accomodationPaidStatus == "Paid")
                {
                    MessageBox.Show("Payment already confirmed for Request ID " + requestId + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    // Get details for checking capacity and available rooms
                    string detailsQuery = @"
                        SELECT COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize, h.HotelID, OccupiedRooms, TotalRooms, MaxCapacity
                        FROM Booking b
                        JOIN Request req ON b.RequestID = req.RequestID
                        LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                        LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                        JOIN Accommodation a ON pkg.AccommodationID = a.AccomodationID OR ct.AccommodationID = a.AccomodationID
                        JOIN Hotel h ON a.HotelID = h.HotelID
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
                    int hotelId = Convert.ToInt32(reader["HotelID"]);
                    int occupiedRooms = Convert.ToInt32(reader["OccupiedRooms"]);
                    int totalRooms = Convert.ToInt32(reader["TotalRooms"]);
                    int capacity = Convert.ToInt32(reader["MaxCapacity"]);

                    reader.Close();

                    // Check room availability
                    if (totalRooms < occupiedRooms + 1)
                    {
                        MessageBox.Show("Not enough rooms available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check room capacity
                    if (capacity < groupSize)
                    {
                        MessageBox.Show("Not enough room capacity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update AccomodationPaidStatus in Request
                    string updateRequestQuery = @"
                        UPDATE Request
                        SET AccomodationPaidStatus = 'Paid'
                        WHERE RequestID = @RequestID AND AccomodationPaidStatus = 'Unpaid'";
                    cmd = new SqlCommand(updateRequestQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Payment already confirmed or request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    // Update Hotel.OccupiedRooms
                    string updateHotelQuery = @"
                        UPDATE Hotel
                        SET OccupiedRooms = OccupiedRooms + 1
                        WHERE HotelID = @HotelID";
                    cmd = new SqlCommand(updateHotelQuery, conn);
                    cmd.Parameters.AddWithValue("@HotelID", hotelId);
                    cmd.Parameters.AddWithValue("@GroupSize", groupSize);
                    cmd.ExecuteNonQuery();
                }
                LoadBookingManagement();
                MessageBox.Show("Payment confirmed and rooms allocated for Request ID " + requestId + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string accomodationPaidStatus = dgvBookings.SelectedRows[0].Cells["AccomodationPaidStatus"].Value?.ToString();
                if (accomodationPaidStatus != "Paid")
                {
                    MessageBox.Show("No payment confirmed to cancel for Request ID " + requestId + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    // Get GroupSize and HotelID
                    string detailsQuery = @"
                        SELECT COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize, h.HotelID
                        FROM Booking b
                        JOIN Request req ON b.RequestID = req.RequestID
                        LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                        LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                        JOIN Accommodation a ON pkg.AccommodationID = a.AccomodationID OR ct.AccommodationID = a.AccomodationID
                        JOIN Hotel h ON a.HotelID = h.HotelID
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
                    int hotelId = Convert.ToInt32(reader["HotelID"]);
                    reader.Close();

                    // Update AccomodationPaidStatus in Request
                    string updateRequestQuery = @"
                        UPDATE Request
                        SET AccomodationPaidStatus = 'Unpaid'
                        WHERE RequestID = @RequestID AND AccomodationPaidStatus = 'Paid'";
                    cmd = new SqlCommand(updateRequestQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("No payment confirmed or request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Decrease Hotel.OccupiedRooms
                    string updateHotelQuery = @"
                        UPDATE Hotel
                        SET OccupiedRooms = OccupiedRooms - @GroupSize
                        WHERE HotelID = @HotelID";
                    cmd = new SqlCommand(updateHotelQuery, conn);
                    cmd.Parameters.AddWithValue("@HotelID", hotelId);
                    cmd.Parameters.AddWithValue("@GroupSize", groupSize);
                    cmd.ExecuteNonQuery();
                }
                LoadBookingManagement();
                MessageBox.Show("Payment and room allocation cancelled for Request ID " + requestId + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling allocation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Integration_Click(object sender, EventArgs e)
        {

        }

        private void Bookings_Click(object sender, EventArgs e)
        {

        }

        private void roomDesc_Click(object sender, EventArgs e)
        {

            if (dgvServiceListing.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int accomodationId = Convert.ToInt32(dgvServiceListing.SelectedRows[0].Cells["AccomodationID"].Value);
                string roomDesc = dgvServiceListing.SelectedRows[0].Cells["RoomDesc"].Value?.ToString();

                // Open edit form
                EditRoomDesc editForm = new EditRoomDesc(accomodationId, roomDesc);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadServiceListing(); // Refresh grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening room description edit form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void price_Click(object sender, EventArgs e)
        {
            if (dgvServiceListing.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int accomodationId = Convert.ToInt32(dgvServiceListing.SelectedRows[0].Cells["AccomodationID"].Value);
                decimal pricePerNight = Convert.ToDecimal(dgvServiceListing.SelectedRows[0].Cells["PricePerNight"].Value);

                // Open edit form
                EditRoomPrice editForm = new EditRoomPrice(accomodationId, pricePerNight);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadServiceListing(); // Refresh grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening price per night edit form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            HotelLogin form = new HotelLogin();
            form.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void cmbGraphType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }
        private void LoadPerformanceReports()
        {
            try
            {
                // Populate ComboBox
                cmbGraphType.Items.Clear();
                cmbGraphType.Items.AddRange(new string[] { "Occupancy", "Ratings", "Revenue" });
                cmbGraphType.SelectedIndex = 0; // Default to Occupancy

                // Initial chart load
                UpdateChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading performance reports: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    if (selectedGraph == "Occupancy")
                    {
                        chartPerformance.Titles.Add("Room Occupancy");
                        series.ChartType = SeriesChartType.Pie;

                        string query = @"
                            SELECT OccupiedRooms, TotalRooms - OccupiedRooms AS AvailableRooms
                            FROM Hotel
                            WHERE ProviderID = @ProviderID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int occupied = reader.GetInt32(0);
                            int available = reader.GetInt32(1);
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

                        // Initialize ratings (mock if no data)
                      //  DataTable ratings = new DataTable();


                        // Try to fetch ratings from database
                        string query = @"
                                SELECT r.Rating, COUNT(*) AS Count
                                FROM Review r
	                                JOIN Accommodation a ON r.AccommodationID = a.AccomodationID
	                                JOIN Hotel h ON a.HotelID = h.HotelID
                                WHERE h.ProviderID = @ProviderID
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
                            DataRow row = dbRatings.Select("Rating = Rating").FirstOrDefault();
                            if (row != null)
                                row["Count"] = count;
                        }

                        foreach (DataRow row in dbRatings.Rows)
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
                                SUM(b.TotalCost) AS TotalRevenue
                                FROM Booking b
                                JOIN Request req ON b.RequestID = req.RequestID
                                LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                                LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                                JOIN Accommodation a ON pkg.AccommodationID = a.AccomodationID OR ct.AccommodationID = a.AccomodationID
                                JOIN Hotel h ON a.HotelID = h.HotelID
                                WHERE h.ProviderID = @ProviderID
                                AND b.BookingStatus = 'Confirmed'
                                AND b.BookingDate >= DATEADD(MONTH, -12, GETDATE())
                                GROUP BY FORMAT(b.BookingDate, 'yyyy-MM')
                                ORDER BY Month";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string month = reader.GetString(0);
                            double revenue = (double)reader.GetDecimal(1);
                            series.Points.AddXY(month, revenue);
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

        private void chartPerformance_Click(object sender, EventArgs e)
        {

        }

    }
}
