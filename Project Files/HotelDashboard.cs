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
        }
        private void LoadServiceIntegration()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                SELECT r.RequestID, r.OperatorID, r.TripSourceType, 
                       COALESCE(r.PackageID, r.CustomTripID) AS TripID, 
                       r.PreferredStartDate, a.AccomodationID
                FROM Request r
                LEFT JOIN Package p ON r.PackageID = p.PackageID
                LEFT JOIN CustomTrip ct ON r.CustomTripID = ct.CustomTripID
                JOIN Accommodation a ON p.AccommodationID = a.AccomodationID OR ct.AccommodationID = a.AccomodationID
                JOIN Hotel h ON a.HotelID = h.HotelID
                WHERE h.ProviderID = @ProviderID AND r.RequestStatus = 'Pending'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvAssignments.DataSource = dt;
                    // Customize column headers
                    dgvAssignments.Columns["RequestID"].HeaderText = "Request ID";
                    dgvAssignments.Columns["OperatorID"].HeaderText = "Operator ID";
                    dgvAssignments.Columns["TripSourceType"].HeaderText = "Trip Type";
                    dgvAssignments.Columns["TripID"].HeaderText = "Trip ID";
                    dgvAssignments.Columns["PreferredStartDate"].HeaderText = "Start Date";
                    dgvAssignments.Columns["AccomodationID"].HeaderText = "Accommodation ID";
                }
            }
            catch (Exception ex)
            {
                // lblError.Text = "Error loading requests: " + ex.Message;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int requestId = Convert.ToInt32(dgvAssignments.SelectedRows[0].Cells["RequestID"].Value);
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    // Update Request
                    string updateQuery = "UPDATE Request SET RequestStatus = 'Accepted' WHERE RequestID = @RequestID";
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    cmd.ExecuteNonQuery();

                }
                LoadServiceIntegration();
                MessageBox.Show("Request accepted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accepting request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int requestId = Convert.ToInt32(dgvAssignments.SelectedRows[0].Cells["RequestID"].Value);
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = "UPDATE Request SET RequestStatus = 'Rejected' WHERE RequestID = @RequestID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    cmd.ExecuteNonQuery();
                }
                LoadServiceIntegration();
                MessageBox.Show("Request rejected!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error rejecting request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /////////////////       SERVICE LISTING       ////////////////
        /////////////// FOR THE APPROVAL/REJECTION JUST IGNORE THIS PART
        
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
                    // Customize columns
                    dgvServiceListing.Columns["AccomodationID"].HeaderText = "Accommodation ID";
                    dgvServiceListing.Columns["RoomDesc"].HeaderText = "Room Description";
                    dgvServiceListing.Columns["PricePerNight"].HeaderText = "Price Per Night";
                    dgvServiceListing.Columns["MaxCapacity"].HeaderText = "Max Capacity";
                    dgvServiceListing.Columns["HotelName"].HeaderText = "Hotel Name";
                    dgvServiceListing.Columns["DestinationName"].HeaderText = "Destination";
                    // Format PricePerNight as currency
                    dgvServiceListing.Columns["PricePerNight"].DefaultCellStyle.Format = "C2";
                    // dgvServiceListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
    }
}
