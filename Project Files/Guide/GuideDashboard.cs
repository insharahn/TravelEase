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
using Microsoft.VisualBasic;
#pragma warning restore IDE0005

namespace Hotel_and_Transport
{
    public partial class GuideDashboard : Form
    {
        private int _providerId;

        public GuideDashboard(int providerId)
        {
            InitializeComponent();
            _providerId = providerId;
        }

        private void GuideDashboard_Load(object sender, EventArgs e)
        {
            LoadServiceIntegration();
            LoadServiceListing();
            LoadBookingManagement();
            LoadPerformanceReports();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Reports_Click(object sender, EventArgs e)
        {

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
            GuideLogin form = new GuideLogin();
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
                            SELECT DISTINCT a.ActivityID, g.GuideID, a.ActivityName, d.Name,  a.Price as PricePerPerson, StartTime, EndTime, CapacityLimit,
                                   a.ActivityStatus
                            FROM Activity a
                            JOIN Guide g ON a.GuideID = g.GuideID
                            JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
							JOIN PackageActivities pkgA ON pkgA.ActivityID = a.ActivityID
                            JOIN Package pkg ON pkgA.PackageID = pkg.PackageID
							JOIN CustomTripActivities ctA ON ctA.ActivityID = a.ActivityID
                            JOIN CustomTrip ct ON ct.CustomTripID = ctA.CustomTripID
							JOIN Destination d ON sp.DestinationID = d.DestinationID
                            LEFT JOIN Request req ON pkg.PackageID = req.PackageID OR ct.CustomTripID = req.CustomTripID
                            WHERE sp.ProviderID = @ProviderID
                            AND sp.ProviderType = 2
                            AND a.ActivityStatus = 'Pending'
                            AND req.RequestID IS NOT NULL";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId); // Use the logged-in guide's ProviderID
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvActivityRequests.DataSource = dt;

                    // Customize DataGridView columns
                    dgvActivityRequests.Columns["ActivityID"].HeaderText = "Activity ID";
                    dgvActivityRequests.Columns["GuideID"].HeaderText = "Guide ID";
                    dgvActivityRequests.Columns["ActivityName"].HeaderText = "Activity Name";
                    dgvActivityRequests.Columns["Name"].HeaderText = "Destination";
                    dgvActivityRequests.Columns["PricePerPerson"].HeaderText = "Price Per Person ($)";
                    dgvActivityRequests.Columns["StartTime"].HeaderText = "Start Time";
                    dgvActivityRequests.Columns["EndTime"].HeaderText = "End Time";
                    dgvActivityRequests.Columns["CapacityLimit"].HeaderText = "Capacity Limit";
                    dgvActivityRequests.Columns["ActivityStatus"].HeaderText = "Status";

                    // Format Price and DateTime columns
                    dgvActivityRequests.Columns["PricePerPerson"].DefaultCellStyle.Format = "N2";
                    dgvActivityRequests.Columns["StartTime"].DefaultCellStyle.Format = "g"; // Short date and time (e.g., 5/2/2025 1:30 PM)
                    dgvActivityRequests.Columns["EndTime"].DefaultCellStyle.Format = "g";

                    // Disable sorting
                    foreach (DataGridViewColumn column in dgvActivityRequests.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No pending activity requests available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading activity requests: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTrips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (dgvActivityRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an activity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int ActivityID = Convert.ToInt32(dgvActivityRequests.SelectedRows[0].Cells["ActivityID"].Value);
                string ActivityStatus = dgvActivityRequests.SelectedRows[0].Cells["ActivityStatus"].Value?.ToString();
                if (ActivityStatus != "Pending")
                {
                    MessageBox.Show("Activity Status is not pending.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string updateActivityQuery = @"
                        UPDATE Activity
                        SET ActivityStatus = 'Approved'
                        WHERE ActivityID = @ActivityID AND ActivityStatus = 'Pending'";
                    SqlCommand cmd = new SqlCommand(updateActivityQuery, conn);
                    cmd.Parameters.AddWithValue("@ActivityID", ActivityID);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceIntegration();
                MessageBox.Show("Activity status approved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving Activity: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvActivityRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an activity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int ActivityID = Convert.ToInt32(dgvActivityRequests.SelectedRows[0].Cells["ActivityID"].Value);
                string ActivityStatus = dgvActivityRequests.SelectedRows[0].Cells["ActivityStatus"].Value?.ToString();
                if (ActivityStatus != "Pending")
                {
                    MessageBox.Show("Activity Status is not pending.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string updateActivityQuery = @"
                        UPDATE Activity
                        SET ActivityStatus = 'Rejected'
                        WHERE ActivityID = @ActivityID AND ActivityStatus = 'Pending'";
                    SqlCommand cmd = new SqlCommand(updateActivityQuery, conn);
                    cmd.Parameters.AddWithValue("@ActivityID", ActivityID);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceIntegration();
                MessageBox.Show("Activity status Rejected!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Rejected Activity: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SELECT DISTINCT 
                    a.ActivityID,
                    g.GuideID,
                    sp.ProviderName as GuideName,
                    a.ActivityName,
                    d.Name AS Destination,
                    a.Price AS PricePerPerson,
                    a.StartTime,
                    a.EndTime,
                    a.CapacityLimit,
                    a.CurrentParticipants
                FROM Activity a
                JOIN Guide g ON a.GuideID = g.GuideID
                JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
                JOIN Destination d ON sp.DestinationID = d.DestinationID
                WHERE sp.ProviderID = @ProviderID
                AND sp.ProviderType = 2";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId); // Use the logged-in guide's ProviderID
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvActivityListing.DataSource = dt;

                    // Customize DataGridView columns
                    dgvActivityListing.Columns["ActivityID"].HeaderText = "Activity ID";
                    dgvActivityListing.Columns["GuideID"].HeaderText = "Guide ID";
                    dgvActivityListing.Columns["ActivityName"].HeaderText = "Activity Name";
                    dgvActivityListing.Columns["GuideName"].HeaderText = "Guide";
                    dgvActivityListing.Columns["Destination"].HeaderText = "Destination";
                    dgvActivityListing.Columns["PricePerPerson"].HeaderText = "Price Per Person ($)";
                    dgvActivityListing.Columns["StartTime"].HeaderText = "Start Time";
                    dgvActivityListing.Columns["EndTime"].HeaderText = "End Time";
                    dgvActivityListing.Columns["CapacityLimit"].HeaderText = "Capacity Limit";
                    dgvActivityListing.Columns["CurrentParticipants"].HeaderText = "Current Participants";

                    // Format Price and DateTime columns
                    dgvActivityListing.Columns["PricePerPerson"].DefaultCellStyle.Format = "N2";
                    dgvActivityListing.Columns["StartTime"].DefaultCellStyle.Format = "g"; // Short date and time
                    dgvActivityListing.Columns["EndTime"].DefaultCellStyle.Format = "g";

                    // Disable sorting
                    foreach (DataGridViewColumn column in dgvActivityListing.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No activities available for listing.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading activity listing: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvServiceListing_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditTimes_Click(object sender, EventArgs e)
        {
            if (dgvActivityListing.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an activity to edit the times.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int activityId = Convert.ToInt32(dgvActivityListing.SelectedRows[0].Cells["ActivityID"].Value);
            DateTime? startTime = dgvActivityListing.SelectedRows[0].Cells["StartTime"].Value != DBNull.Value ? Convert.ToDateTime(dgvActivityListing.SelectedRows[0].Cells["StartTime"].Value) : (DateTime?)null;
            DateTime? endTime = dgvActivityListing.SelectedRows[0].Cells["EndTime"].Value != DBNull.Value ? Convert.ToDateTime(dgvActivityListing.SelectedRows[0].Cells["EndTime"].Value) : (DateTime?)null;

            using (var form = new EditActivityTimes(activityId, startTime, endTime))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadServiceListing(); // Refresh the grid
                }
            }
        }

        private void btnEditCapacity_Click(object sender, EventArgs e)
        {
            if (dgvActivityListing.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an activity to edit the capacity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            try
            {
                int activityId = Convert.ToInt32(dgvActivityListing.SelectedRows[0].Cells["ActivityID"].Value);
                int currentCapacity = Convert.ToInt32(dgvActivityListing.SelectedRows[0].Cells["CapacityLimit"].Value);

                // Open edit form
                EditActivityCapacity editForm = new EditActivityCapacity(activityId, currentCapacity);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadServiceListing(); // Refresh grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening capacity edit form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditPrice_Click(object sender, EventArgs e)
        {
            if (dgvActivityListing.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an activity to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int ActivityID = Convert.ToInt32(dgvActivityListing.SelectedRows[0].Cells["ActivityID"].Value);
                decimal price = Convert.ToDecimal(dgvActivityListing.SelectedRows[0].Cells["PricePerPerson"].Value);

                // Open edit form
                EditActivityPrice editForm = new EditActivityPrice(ActivityID, price);
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
                SELECT DISTINCT 
                    b.RequestID, 
                    b.BookingDate, 
                    d.Name AS Destination, 
                    a.StartTime,
                    a.EndTime,
                    (COALESCE(pkg.GroupSize, ct.GroupSize) * a.Price) AS TotalCost, 
                    COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize, 
                    req.ActivityPaidStatus
                FROM Booking b
                JOIN Request req ON b.RequestID = req.RequestID
                LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                LEFT JOIN PackageActivities pa ON pkg.PackageID = pa.PackageID
                LEFT JOIN CustomTripActivities cta ON ct.CustomTripID = cta.CustomTripID
                JOIN Activity a ON pa.ActivityID = a.ActivityID OR cta.ActivityID = a.ActivityID
                JOIN Guide g ON a.GuideID = g.GuideID
                JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
                JOIN Destination d ON sp.DestinationID = d.DestinationID
                WHERE sp.ProviderID = @ProviderID 
                AND sp.ProviderType = 2";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBookingManagement.DataSource = dt;

                    // Customize column headers
                    dgvBookingManagement.Columns["RequestID"].HeaderText = "Request ID";
                    dgvBookingManagement.Columns["BookingDate"].HeaderText = "Booking Date";
                    dgvBookingManagement.Columns["Destination"].HeaderText = "Destination";
                    dgvBookingManagement.Columns["TotalCost"].HeaderText = "Total Cost ($)";
                    dgvBookingManagement.Columns["GroupSize"].HeaderText = "Group Size";
                    dgvBookingManagement.Columns["StartTime"].HeaderText = "Start Time";
                    dgvBookingManagement.Columns["EndTime"].HeaderText = "End Time";
                    dgvBookingManagement.Columns["ActivityPaidStatus"].HeaderText = "Activity Payment Status";

                    // Format columns
                    dgvBookingManagement.Columns["TotalCost"].DefaultCellStyle.Format = "N2"; // 2 decimal places
                    dgvBookingManagement.Columns["BookingDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                    dgvBookingManagement.Columns["StartTime"].DefaultCellStyle.Format = "g";
                    dgvBookingManagement.Columns["EndTime"].DefaultCellStyle.Format = "g";


                    // Disable sorting
                    foreach (DataGridViewColumn column in dgvBookingManagement.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No bookings available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dgvBookingManagement.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int requestId = Convert.ToInt32(dgvBookingManagement.SelectedRows[0].Cells["RequestID"].Value);
                string activityPaidStatus = dgvBookingManagement.SelectedRows[0].Cells["ActivityPaidStatus"].Value?.ToString();
                if (activityPaidStatus == "Paid")
                {
                    MessageBox.Show("Payment already confirmed for Request ID " + requestId + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    // Get GroupSize, ActivityID, CapacityLimit, and CurrentParticipants
                    string detailsQuery = @"
                SELECT COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize, 
                       a.ActivityID, 
                       a.CapacityLimit, 
                       a.CurrentParticipants
                FROM Booking b
                JOIN Request req ON b.RequestID = req.RequestID
                LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                LEFT JOIN PackageActivities pa ON pkg.PackageID = pa.PackageID
                LEFT JOIN CustomTripActivities cta ON ct.CustomTripID = cta.CustomTripID
                JOIN Activity a ON pa.ActivityID = a.ActivityID OR cta.ActivityID = a.ActivityID
                JOIN Guide g ON a.GuideID = g.GuideID
                JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
                WHERE req.RequestID = @RequestID 
                AND sp.ProviderID = @ProviderID 
                AND sp.ProviderType = 2";
                    SqlCommand cmd = new SqlCommand(detailsQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        MessageBox.Show("Booking details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int groupSize = Convert.ToInt32(reader["GroupSize"]);
                    int activityId = Convert.ToInt32(reader["ActivityID"]);
                    int capacityLimit = Convert.ToInt32(reader["CapacityLimit"]);
                    int currentParticipants = Convert.ToInt32(reader["CurrentParticipants"]);
                    reader.Close();

                    // Check capacity availability
                    if (capacityLimit < currentParticipants + groupSize)
                    {
                        MessageBox.Show("Not enough capacity available for this activity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update ActivityPaidStatus in Request
                    string updateRequestQuery = @"
                UPDATE Request
                SET ActivityPaidStatus = 'Paid'
                WHERE RequestID = @RequestID AND ActivityPaidStatus = 'Unpaid'";
                    cmd = new SqlCommand(updateRequestQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Payment already confirmed or request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update Activity.CurrentParticipants
                    string updateActivityQuery = @"
                UPDATE Activity
                SET CurrentParticipants = CurrentParticipants + @GroupSize
                WHERE ActivityID = @ActivityID";
                    cmd = new SqlCommand(updateActivityQuery, conn);
                    cmd.Parameters.AddWithValue("@ActivityID", activityId);
                    cmd.Parameters.AddWithValue("@GroupSize", groupSize);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceListing();
                LoadBookingManagement();
                MessageBox.Show("Payment confirmed and participants allocated for Request ID " + requestId + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelAllocation_Click(object sender, EventArgs e)
        {
            if (dgvBookingManagement.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int requestId = Convert.ToInt32(dgvBookingManagement.SelectedRows[0].Cells["RequestID"].Value);
                string activityPaidStatus = dgvBookingManagement.SelectedRows[0].Cells["ActivityPaidStatus"].Value?.ToString();
                if (activityPaidStatus != "Paid")
                {
                    MessageBox.Show("No payment confirmed to cancel for Request ID " + requestId + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    // Get GroupSize, ActivityID, and CurrentParticipants
                    string detailsQuery = @"
                SELECT COALESCE(pkg.GroupSize, ct.GroupSize) AS GroupSize, 
                       a.ActivityID, 
                       a.CurrentParticipants
                FROM Booking b
                JOIN Request req ON b.RequestID = req.RequestID
                LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                LEFT JOIN PackageActivities pa ON pkg.PackageID = pa.PackageID
                LEFT JOIN CustomTripActivities cta ON ct.CustomTripID = cta.CustomTripID
                JOIN Activity a ON pa.ActivityID = a.ActivityID OR cta.ActivityID = a.ActivityID
                JOIN Guide g ON a.GuideID = g.GuideID
                JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
                WHERE req.RequestID = @RequestID 
                AND sp.ProviderID = @ProviderID 
                AND sp.ProviderType = 2";
                    SqlCommand cmd = new SqlCommand(detailsQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        MessageBox.Show("Booking details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int groupSize = Convert.ToInt32(reader["GroupSize"]);
                    int activityId = Convert.ToInt32(reader["ActivityID"]);
                    int currentParticipants = Convert.ToInt32(reader["CurrentParticipants"]);
                    reader.Close();

                    // Validate sufficient CurrentParticipants
                    if (currentParticipants < groupSize)
                    {
                        MessageBox.Show("Invalid participant allocation to cancel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update ActivityPaidStatus to Unpaid
                    string updateRequestQuery = @"
                UPDATE Request
                SET ActivityPaidStatus = 'Unpaid'
                WHERE RequestID = @RequestID AND ActivityPaidStatus = 'Paid'";
                    cmd = new SqlCommand(updateRequestQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("No payment confirmed or request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Decrease Activity.CurrentParticipants
                    string updateActivityQuery = @"
                UPDATE Activity
                SET CurrentParticipants = CurrentParticipants - @GroupSize
                WHERE ActivityID = @ActivityID AND CurrentParticipants >= @GroupSize";
                    cmd = new SqlCommand(updateActivityQuery, conn);
                    cmd.Parameters.AddWithValue("@ActivityID", activityId);
                    cmd.Parameters.AddWithValue("@GroupSize", groupSize);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceListing();
                LoadBookingManagement();
                MessageBox.Show("Payment and participant allocation cancelled for Request ID " + requestId + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling allocation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadPerformanceReports()
        {
            try
            {
                // Populate ComboBox
                cmbGraphType.Items.Clear();
                cmbGraphType.Items.AddRange(new string[] { "Earnings by Activity", "Participant Count", "Booking Status Summary", "Activity Participation Status" });
                cmbGraphType.SelectedIndex = 0; // Default to Earnings by Activity

                // Initial chart load
                UpdateChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reports: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                chartReports.Series.Clear();
                chartReports.ChartAreas.Clear();
                chartReports.Titles.Clear();

                ChartArea chartArea = new ChartArea();
                chartReports.ChartAreas.Add(chartArea);

                Series series = new Series("Performance");
                chartReports.Series.Add(series);

                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string selectedGraph = cmbGraphType.SelectedItem.ToString();

                    if (selectedGraph == "Earnings by Activity")
                    {
                        chartReports.Titles.Add("Earnings by Activity");
                        series.ChartType = SeriesChartType.Column;

                        string query = @"
                    SELECT 
                        a.ActivityName, 
                        SUM(COALESCE(pkg.GroupSize, ct.GroupSize) * a.Price) AS TotalEarnings
                    FROM Activity a
                    JOIN Guide g ON a.GuideID = g.GuideID
                    JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
                    LEFT JOIN PackageActivities pa ON a.ActivityID = pa.ActivityID
                    LEFT JOIN CustomTripActivities cta ON a.ActivityID = cta.ActivityID
                    LEFT JOIN Package pkg ON pa.PackageID = pkg.PackageID
                    LEFT JOIN CustomTrip ct ON cta.CustomTripID = ct.CustomTripID
                    LEFT JOIN Request req ON (pkg.PackageID = req.PackageID OR ct.CustomTripID = req.CustomTripID)
                    LEFT JOIN Booking b ON req.RequestID = b.RequestID
                    WHERE sp.ProviderID = @ProviderID 
                    AND sp.ProviderType = 2
                    GROUP BY a.ActivityName";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string activityName = reader["ActivityName"].ToString();
                            double earnings = reader.IsDBNull(1) ? 0 : Convert.ToDouble(reader["TotalEarnings"]);
                            series.Points.AddXY(activityName, earnings);
                        }
                        reader.Close();
                        chartArea.AxisX.Title = "Activity";
                        chartArea.AxisY.Title = "Earnings ($)";
                        chartArea.AxisY.LabelStyle.Format = "$#,##0";
                    }
                    else if (selectedGraph == "Participant Count")
                    {
                        chartReports.Titles.Add("Participant Count");
                        series.ChartType = SeriesChartType.Column;

                        string query = @"
                    SELECT 
                        a.ActivityName, 
                        a.CurrentParticipants AS Participants
                    FROM Activity a
                    JOIN Guide g ON a.GuideID = g.GuideID
                    JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
                    WHERE sp.ProviderID = @ProviderID 
                    AND sp.ProviderType = 2";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string activityName = reader["ActivityName"].ToString();
                            int participants = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            series.Points.AddXY(activityName, participants);
                        }
                        reader.Close();
                        chartArea.AxisX.Title = "Activity";
                        chartArea.AxisY.Title = "Participants";
                    }
                    else if (selectedGraph == "Booking Status Summary")
                    {
                        chartReports.Titles.Add("Booking Status Summary");
                        series.ChartType = SeriesChartType.Pie;

                        string query = @"
                            SELECT 
                                b.BookingStatus, 
                                COUNT(b.RequestID) AS BookingCount
                            FROM Booking b
                            JOIN Request req ON b.RequestID = req.RequestID
                            LEFT JOIN Package pkg ON req.PackageID = pkg.PackageID
                            LEFT JOIN CustomTrip ct ON req.CustomTripID = ct.CustomTripID
                            LEFT JOIN PackageActivities pa ON pkg.PackageID = pa.PackageID
                            LEFT JOIN CustomTripActivities cta ON ct.CustomTripID = cta.CustomTripID
                            JOIN Activity a ON pa.ActivityID = a.ActivityID OR cta.ActivityID = a.ActivityID
                            JOIN Guide g ON a.GuideID = g.GuideID
                            JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
                            WHERE sp.ProviderID = @ProviderID 
                            AND sp.ProviderType = 2
                            GROUP BY b.BookingStatus";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string status = reader["BookingStatus"].ToString();
                            int count = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            series.Points.AddXY(status, count);
                        }
                        reader.Close();
                        foreach (DataPoint point in series.Points)
                        {
                            point.Label = "#PERCENT{P0}";
                            point.LegendText = point.AxisLabel;
                        }
                    }
                    else if (selectedGraph == "Activity Participation Status")
                    {
                        chartReports.Titles.Add("Activity Participation Status");
                        series.ChartType = SeriesChartType.Pie;

                        string query = @"
                            SELECT 
                                SUM(a.CurrentParticipants) AS BookedParticipants,
                                (SUM(a.CapacityLimit) - SUM(a.CurrentParticipants)) AS UnbookedParticipants
                            FROM Activity a
                            JOIN Guide g ON a.GuideID = g.GuideID
                            JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID
                            WHERE sp.ProviderID = @ProviderID 
                            AND sp.ProviderType = 2";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProviderID", _providerId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int booked = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            int unbooked = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            series.Points.AddXY("Booked", booked);
                            series.Points.AddXY("Unbooked", unbooked);
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

        private void chartPerformance_Click(object sender, EventArgs e)
        {

        }
    }
    

}

