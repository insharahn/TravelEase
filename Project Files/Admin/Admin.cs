using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Data.SqlClient;

namespace TourOperator
{
    public partial class Admin : Form
    {
        int AdminId;
        private readonly string connectionString =
            "Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
            "Initial Catalog=\"Travel ease2\";" +
            "Integrated Security=True;Encrypt=False";
      
           
            public Admin(int Admin_Id)
        {
            InitializeComponent();
            AdminId= Admin_Id;
            LoadPendingUsers(); // Load data on form load
            LoadTripCategories();
            LoadReviews();
            LoadReportComboBox();
        }
        private void LoadReportComboBox()
        {

                ReportComboBox.Items.Clear();
            ReportComboBox.Items.Add("User Traffic");
            ReportComboBox.Items.Add("Booking Trends");
            ReportComboBox.Items.Add("Revenue Generated");

        }
        private void LoadPendingUsers()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query1 = "SELECT 'ServiceProvider' AS UserType, ProviderId AS ID, ProviderName AS Name, Email FROM ServiceProvider WHERE SpStatus = 'Pending'";
                    string query2 = "SELECT 'Operator' AS UserType, OperatorID AS ID, CompanyName AS Name, Email FROM Operator WHERE OpStatus = 'Pending'";
                    string query3 = "SELECT 'Traveler' AS UserType, TravelerID AS ID, FirstName + ' ' + LastName AS Name, Email FROM Traveler WHERE TravelerStatus = 'Pending'";

                    using (SqlCommand cmd1 = new SqlCommand(query1, con))
                    using (SqlCommand cmd2 = new SqlCommand(query2, con))
                    using (SqlCommand cmd3 = new SqlCommand(query3, con))
                    using (SqlDataAdapter da1 = new SqlDataAdapter(cmd1))
                    using (SqlDataAdapter da2 = new SqlDataAdapter(cmd2))
                    using (SqlDataAdapter da3 = new SqlDataAdapter(cmd3))
                    {
                        DataTable dt1 = new DataTable();
                        DataTable dt2 = new DataTable();
                        DataTable dt3 = new DataTable();

                        da1.Fill(dt1);
                        da2.Fill(dt2);
                        da3.Fill(dt3);

                        dt1.Merge(dt2);
                        dt1.Merge(dt3);

                        PendingUsers.DataSource = dt1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading pending users: " + ex.Message);
                }
            }
        }


        private void PendingUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell clicks here
        }

        private void ApproveStatus_Click(object sender, EventArgs e)
        {
            if (PendingUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to approve.");
                return;
            }

            DataGridViewRow selectedRow = PendingUsers.SelectedRows[0];
            string userType = selectedRow.Cells["UserType"].Value.ToString();
            int userId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            string updateQuery = "";
            string idParam = "";
            string statusColumn = "";

            // Determine the query based on the user type
            if (userType == "ServiceProvider")
            {
                updateQuery = "UPDATE ServiceProvider SET SpStatus = 'Approved' WHERE ProviderId = @Id";
                idParam = "@Id";
            }
            else if (userType == "Operator")
            {
                updateQuery = "UPDATE Operator SET OpStatus = 'Approved' WHERE OperatorID = @Id";
                idParam = "@Id";
            }
            else if (userType == "Traveler")
            {
                updateQuery = "UPDATE Traveler SET TravelerStatus = 'Approved' WHERE TravelerID = @Id";
                idParam = "@Id";
            }
            else
            {
                MessageBox.Show("Unknown user type.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", userId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"{userType} approved successfully.");
                            LoadPendingUsers(); // Refresh the grid
                        }
                        else
                        {
                            MessageBox.Show("Approval failed. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while approving: " + ex.Message);
                }
            }
        }
        private void LoadTripCategories()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM TripCategory";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        TripCategoriesGridView.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading trip categories: " + ex.Message);
                }
            }
        }



        private void TripCategoriesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void AddTripCategory_Click(object sender, EventArgs e)
        {
            if (TripCategoriesGridView.Rows.Count == 0)
            {
                MessageBox.Show("No row to insert.");
                return;
            }

            // Get the last row (newly added row)
            DataGridViewRow lastRow = TripCategoriesGridView.Rows[TripCategoriesGridView.Rows.Count - 2]; // -2 because the last row is empty row

            if (lastRow.Cells["TripType"].Value == null)
            {
                MessageBox.Show("Please enter a TripType in the grid.");
                return;
            }

            string newCategory = lastRow.Cells["TripType"].Value.ToString().Trim();

            if (string.IsNullOrWhiteSpace(newCategory))
            {
                MessageBox.Show("TripType cannot be empty.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Check if category exists
                    string checkQuery = "SELECT COUNT(*) FROM TripCategory WHERE TripType = @TripType";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@TripType", newCategory);
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            MessageBox.Show("TripType already exists.");
                            return;
                        }
                    }

                    // Insert
                    string insertQuery = "INSERT INTO TripCategory (TripType) VALUES (@TripType)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@TripType", newCategory);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("TripType added successfully.");
                        LoadTripCategories();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding category: " + ex.Message);
                }
            }
        }
        private void LoadReviews()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Load pending reviews
                    string pendingQuery = "SELECT ReviewID, Comment FROM Review WHERE ModerationStatus = 'Pending'";
                    using (SqlCommand cmdPending = new SqlCommand(pendingQuery, con))
                    using (SqlDataAdapter daPending = new SqlDataAdapter(cmdPending))
                    {
                        DataTable dtPending = new DataTable();
                        daPending.Fill(dtPending);
                        pendingReviewDataGrid.DataSource = dtPending;
                    }

                    // Load rejected (inappropriate) reviews
                    string rejectedQuery = "SELECT ReviewID, Comment FROM Review WHERE ModerationStatus = 'Rejected'";
                    using (SqlCommand cmdRejected = new SqlCommand(rejectedQuery, con))
                    using (SqlDataAdapter daRejected = new SqlDataAdapter(cmdRejected))
                    {
                        DataTable dtRejected = new DataTable();
                        daRejected.Fill(dtRejected);
                        inappropriateReviewDataGrid.DataSource = dtRejected;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading reviews: " + ex.Message);
                }
            }
        }


        private void pendingReviewDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void inappropriateReviewDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void approveReview_Click(object sender, EventArgs e)
        {
            if (pendingReviewDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a review to approve.");
                return;
            }

            int reviewId = Convert.ToInt32(pendingReviewDataGrid.SelectedRows[0].Cells["ReviewID"].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string updateQuery = "UPDATE Review SET ModerationStatus = 'Approved' WHERE ReviewID = @ReviewID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Review approved successfully.");
                            LoadReviews(); // Refresh the grids
                        }
                        else
                        {
                            MessageBox.Show("Failed to approve review.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving review: " + ex.Message);
                }
            }
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {

            if (pendingReviewDataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a review to Reject.");
                return;
            }

            int reviewId = Convert.ToInt32(pendingReviewDataGrid.SelectedRows[0].Cells["ReviewID"].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string updateQuery = "UPDATE Review SET ModerationStatus = 'Rejected' WHERE ReviewID = @ReviewID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Review Rejected successfully.");
                            LoadReviews(); // Refresh the grids
                        }
                        else
                        {
                            MessageBox.Show("Failed to Reject review.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving review: " + ex.Message);
                }
            }

        }

        private void AdminReports_Click(object sender, EventArgs e)
        {

        }
        private void LoadUserTrafficChart()
        {
            string query = @"
    SELECT FORMAT(RegistrationDate, 'yyyy-MM') AS Month, COUNT(*) AS UserCount
    FROM Traveler
    GROUP BY FORMAT(RegistrationDate, 'yyyy-MM')
    ORDER BY Month";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
                 "Initial Catalog=\"Travel ease2\";" +
                 "Integrated Security=True;Encrypt=False"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                AdminReports.Series.Clear();
                Series series = new Series("User Traffic");
                series.ChartType = SeriesChartType.Column;

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["Month"], row["UserCount"]);
                }

                AdminReports.Series.Add(series);
                AdminReports.Titles.Clear();
                AdminReports.Titles.Add("Monthly User Registration Traffic");
            }
        }
        private void LoadRevenueGenerated()
        {
            string query = @"
    SELECT FORMAT(b.BookingDate, 'yyyy-MM') AS Month, SUM(b.TotalCost) AS Revenue
    FROM Booking b
    INNER JOIN Request r ON r.RequestID = b.RequestID
    WHERE b.BookingDate BETWEEN '2024-01-01' AND '2024-12-31'
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

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                AdminReports.Series.Clear();
                Series series = new Series("Monthly Revenue");
                series.ChartType = SeriesChartType.Column;
                series.Color = System.Drawing.Color.SeaGreen;
                series.BorderWidth = 2;

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["Month"].ToString(), Convert.ToDouble(row["Revenue"]));
                }

                AdminReports.Series.Add(series);
                AdminReports.Titles.Clear();
                AdminReports.Titles.Add("Monthly Revenue from Confirmed Bookings (2024)");
            }
        }

        private void LoadTotalBookingsChart()
        {
            string query = @"
    SELECT FORMAT(b.BookingDate, 'yyyy-MM') AS Month, COUNT(*) AS TotalBookings
    FROM Booking b
    WHERE b.BookingStatus = 'Confirmed'
      AND b.CancellationReason IS NULL
    GROUP BY FORMAT(b.BookingDate, 'yyyy-MM')
    ORDER BY Month";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;" +
                  "Initial Catalog=\"Travel ease2\";" +
                  "Integrated Security=True;Encrypt=False"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                AdminReports.Series.Clear();
                Series series = new Series("Total Bookings");
                series.ChartType = SeriesChartType.Column;
                series.Color = System.Drawing.Color.Coral;  // Set color for the series

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["Month"].ToString(), Convert.ToInt32(row["TotalBookings"]));
                }

                AdminReports.Series.Add(series);
                AdminReports.Titles.Clear();
                AdminReports.Titles.Add("Monthly Total Bookings (Confirmed) for 2024");
            }
        }



        private void reportComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReport = ReportComboBox.SelectedItem.ToString();

            switch (selectedReport)
            {
                case "User Traffic":
                    LoadUserTrafficChart();
                    break;
                case "Revenue Generated":
                    LoadRevenueGenerated();
                    break;
                case "Booking Trends":
                    LoadTotalBookingsChart();
                    break;
                default:
                    break;
            }
          
        }

        private void AdminReports_Click_1(object sender, EventArgs e)
        {

        }

        private void ReportComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void AddTripCategory_Click_1(object sender, EventArgs e)
        {

        }
    }
}
