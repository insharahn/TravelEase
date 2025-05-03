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

namespace dbfinalproject_interfaces
{
    public partial class travelerReview : Form
    {
        public int TravelerID { get; set; }
        public travelerReview()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False";
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");
        public travelerReview(int tid)
        {
            TravelerID = tid;
            InitializeComponent();
        }

        private void loadAccommodations()
        {
            cmbAccommodation.Items.Clear();

            try
            {
                con.Open();

                //get accommodations for custom trip / package where payment is successful, status confirmed, and date in the past
                string ctQuery = @"
            SELECT a.AccomodationID, sp.ProviderName
            FROM Booking b
            INNER JOIN Request r ON b.RequestID = r.RequestID
            INNER JOIN Payment pay ON pay.BookingID = b.BookingID
            INNER JOIN CustomTrip c ON r.CustomTripID = c.CustomTripID
            INNER JOIN Accommodation a ON c.AccommodationID = a.AccomodationID
            INNER JOIN Hotel h ON h.HotelID = a.HotelID
            INNER JOIN ServiceProvider sp ON sp.ProviderId = h.ProviderID
            WHERE r.TripSourceType = 'Custom'
              AND b.BookingStatus = 'Confirmed'
              AND r.PreferredStartDate < GETDATE()
              AND pay.PaymentStatus = 'Success'
              AND r.TravelerID = @TravelerID";

                string pkgQuery = @"
            SELECT a.AccomodationID, sp.ProviderName
            FROM Booking b
            INNER JOIN Request r ON b.RequestID = r.RequestID
            INNER JOIN Payment pay ON pay.BookingID = b.BookingID
            INNER JOIN Package p ON r.PackageID = p.PackageID
            INNER JOIN Accommodation a ON p.AccommodationID = a.AccomodationID
            INNER JOIN Hotel h ON h.HotelID = a.HotelID
            INNER JOIN ServiceProvider sp ON sp.ProviderId = h.ProviderID
            WHERE r.TripSourceType = 'Package'
              AND b.BookingStatus = 'Confirmed'
              AND r.PreferredStartDate < GETDATE()
              AND pay.PaymentStatus = 'Success'
              AND r.TravelerID = @TravelerID";

                foreach (var query in new[] { ctQuery, pkgQuery })
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                cmbAccommodation.Items.Add(new KeyValuePair<int, string>(id, name));
                            }
                        }
                    }
                }

                cmbAccommodation.DisplayMember = "Value";
                cmbAccommodation.ValueMember = "Key";

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        private void loadRides()
        {
            cmbRide.Items.Clear();

            try
            {

                con.Open();

                string query = @"
            SELECT r.RideID, sp.ProviderName
            FROM Booking b
            INNER JOIN Request req ON b.RequestID = req.RequestID
            INNER JOIN Payment pay ON pay.BookingID = b.BookingID
            LEFT JOIN CustomTrip c ON req.CustomTripID = c.CustomTripID
            LEFT JOIN Package p ON req.PackageID = p.PackageID
            INNER JOIN Ride r ON r.RideID = ISNULL(c.RideID, p.RideID)
            INNER JOIN TransportService t ON t.TransportID = r.TransportID
            INNER JOIN ServiceProvider sp ON sp.ProviderId = t.ProviderID
            WHERE b.BookingStatus = 'Confirmed'
              AND req.PreferredStartDate < GETDATE()
              AND pay.PaymentStatus = 'Success'
              AND req.TravelerID = @TravelerID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbRide.Items.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }

                cmbRide.DisplayMember = "Value";
                cmbRide.ValueMember = "Key";

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void loadOperators()
        {
            cmbOperator.Items.Clear();

            try
            {
                con.Open();

                string query = @"
            SELECT o.OperatorID, o.CompanyName
            FROM Booking b
            INNER JOIN Request r ON b.RequestID = r.RequestID
            INNER JOIN Payment pay ON pay.BookingID = b.BookingID
            INNER JOIN Operator o ON r.OperatorID = o.OperatorID
            WHERE b.BookingStatus = 'Confirmed'
              AND r.PreferredStartDate < GETDATE()
              AND pay.PaymentStatus = 'Success' --paid, confirmed, in the past -> reviewable
              AND r.TravelerID = @TravelerID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbOperator.Items.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }

                cmbOperator.DisplayMember = "Value";
                cmbOperator.ValueMember = "Key";

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void loadGuides()
        {
            cmbGuide.Items.Clear();
            try
            {
                con.Open();

                string customQuery = @"
            SELECT DISTINCT g.GuideID, sp.ProviderName
            FROM Booking b
            INNER JOIN Request req ON b.RequestID = req.RequestID
            INNER JOIN CustomTrip c ON req.CustomTripID = c.CustomTripID
            INNER JOIN CustomTripActivities cta ON cta.CustomTripID = c.CustomTripID
            INNER JOIN Activity a ON a.ActivityID = cta.ActivityID
            INNER JOIN Guide g ON g.GuideID = a.GuideID
            INNER JOIN ServiceProvider sp ON sp.ProviderId = g.ProviderID
            INNER JOIN Payment p ON p.BookingID = b.BookingID
            WHERE req.TripSourceType = 'Custom' 
              AND p.PaymentStatus = 'Success'
              AND b.BookingStatus = 'Confirmed'
              AND req.PreferredStartDate < GETDATE()
              AND req.TravelerID = @TravelerID";

                string packageQuery = @"
             SELECT DISTINCT g.GuideID, sp.ProviderName
            FROM Booking b
            INNER JOIN Request req ON b.RequestID = req.RequestID
            INNER JOIN Package pack ON req.PackageID = pack.PackageID
            INNER JOIN PackageActivities pa ON pa.PackageID = pack.PackageID
            INNER JOIN Activity a ON a.ActivityID = pa.ActivityID
            INNER JOIN Guide g ON g.GuideID = a.GuideID
            INNER JOIN ServiceProvider sp ON sp.ProviderId = g.ProviderID
            INNER JOIN Payment p ON p.BookingID = b.BookingID
            WHERE req.TripSourceType = 'Package' 
              AND p.PaymentStatus = 'Success'
              AND b.BookingStatus = 'Confirmed'
              AND req.PreferredStartDate < GETDATE()
              AND req.TravelerID = @TravelerID";

                foreach (var query in new[] { customQuery, packageQuery })
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                cmbGuide.Items.Add(new KeyValuePair<int, string>(id, name));
                            }
                        }
                    }
                }

                cmbGuide.DisplayMember = "Value";
                cmbGuide.ValueMember = "Key";

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void loadReviewHistory()
        {
            flowLayoutPanel1.Controls.Clear();
            try
            {
                con.Open();

                string query = @"
            SELECT r.Rating, r.Comment, r.ReviewDate,
                ISNULL(spAccommodation.ProviderName, 
                    ISNULL(spRide.ProviderName,
                        ISNULL(spGuide.ProviderName, o.CompanyName))) AS ReviewTarget
            FROM Review r
            LEFT JOIN Accommodation a ON r.AccommodationID = a.AccomodationID
            LEFT JOIN Hotel h ON a.HotelID = h.HotelID
            LEFT JOIN ServiceProvider spAccommodation ON spAccommodation.ProviderId = h.ProviderID

            LEFT JOIN Ride ride ON r.RideID = ride.RideID
            LEFT JOIN TransportService ts ON ride.TransportID = ts.TransportID
            LEFT JOIN ServiceProvider spRide ON spRide.ProviderId = ts.ProviderID

            LEFT JOIN Guide g ON r.GuideID = g.GuideID
            LEFT JOIN ServiceProvider spGuide ON spGuide.ProviderId = g.ProviderID

            LEFT JOIN Operator o ON r.OperatorID = o.OperatorID

            WHERE r.TravelerID = @TravelerID AND r.ModerationStatus = 'Approved'
            ORDER BY r.ReviewDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rating = reader.GetInt32(0);
                            string comment = reader.IsDBNull(1) ? "No comment provided." : reader.GetString(1);
                            DateTime date = reader.GetDateTime(2);
                            string name = reader.IsDBNull(3) ? "Unknown" : reader.GetString(3);

                            //shorten long comments
                            if (comment.Length > 100)
                                comment = comment.Substring(0, 100) + "...";

                            //create panel
                            Panel reviewPanel = new Panel
                            {
                                Width = flowLayoutPanel1.Width - 25,
                                Height = 100,
                                BackColor = Color.WhiteSmoke,
                                Margin = new Padding(5),
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            Label lblName = new Label
                            {
                                Text = $"Name: {name}",
                                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                                Location = new Point(10, 10),
                                AutoSize = true
                            };

                            Label lblRating = new Label
                            {
                                Text = $"Rating: {rating}/5",
                                Font = new Font("Segoe UI", 10),
                                Location = new Point(10, 35),
                                AutoSize = true
                            };

                            Label lblDate = new Label
                            {
                                Text = $"Date: {date.ToShortDateString()}",
                                Font = new Font("Segoe UI", 9),
                                Location = new Point(150, 35),
                                AutoSize = true
                            };

                            Label lblComment = new Label
                            {
                                Text = $"Comment: {comment}",
                                Font = new Font("Segoe UI", 9),
                                Location = new Point(10, 60),
                                AutoSize = true,
                                MaximumSize = new Size(reviewPanel.Width - 20, 40)
                            };

                            reviewPanel.Controls.Add(lblName);
                            reviewPanel.Controls.Add(lblRating);
                            reviewPanel.Controls.Add(lblDate);
                            reviewPanel.Controls.Add(lblComment);

                            flowLayoutPanel1.Controls.Add(reviewPanel);
                        }
                    }
                }

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }





        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHistory_Click(object sender, EventArgs e) //open travel history
        {
            this.Hide();
            travelerBookings book = new travelerBookings();
            book.TravelerID = TravelerID;
            book.OpenTab(2); 
            book.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerHome home = new travelerHome(TravelerID);
            home.Show();
        }

        private void travelerReview_Load(object sender, EventArgs e)
        {
            loadAccommodations();
            loadGuides();
            loadRides();
            loadOperators();
            loadReviewHistory();

        }

        public void OpenTab(int tabIndex) //open form at specific tab
        {
            tabControl1.SelectedIndex = tabIndex;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private int? GetSelectedRating(GroupBox group) //hekper function to return 1-5 based on the radio button clicked
        {
            foreach (RadioButton rb in group.Controls.OfType<RadioButton>())
            {
                if (rb.Checked)
                    return int.Parse(rb.Text);
            }
            return null;
        }

        private void ClearReviewInputs(ComboBox combo, GroupBox group, TextBox commentBox) //helper function to clear review tab after review is submitted
        {
            combo.SelectedIndex = -1;
            foreach (RadioButton rb in group.Controls.OfType<RadioButton>())
                rb.Checked = false;
            commentBox.Clear();
        }


        private void btnReviewOperator_Click(object sender, EventArgs e)
        {
            if (cmbOperator.SelectedItem == null)
            {
                MessageBox.Show("Please select an operator to review.");
                return;
            }

            int? rating = GetSelectedRating(groupBoxOperRating);
            if (rating == null)
            {
                MessageBox.Show("Please select a rating.");
                return;
            }

            var selected = (KeyValuePair<int, string>)cmbOperator.SelectedItem;
            int operatorID = selected.Key;
            string comment = txtOpComment.Text.Trim();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Review (TravelerID, Rating, Comment, ReviewDate, OperatorID, ModerationStatus)
            VALUES (@TravelerID, @Rating, @Comment, GETDATE(), @OperatorID, 'Pending')", con);

                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@Comment", string.IsNullOrEmpty(comment) ? (object)DBNull.Value : comment);
                cmd.Parameters.AddWithValue("@OperatorID", operatorID);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Your operator review has been submitted!");
                ClearReviewInputs(cmbOperator, groupBoxOperRating, txtOpComment);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void btnReviewRide_Click(object sender, EventArgs e)
        {
            if (cmbRide.SelectedItem == null)
            {
                MessageBox.Show("Please select a ride to review.");
                return;
            }

            int? rating = GetSelectedRating(groupBoxRideRating);
            if (rating == null)
            {
                MessageBox.Show("Please select a rating.");
                return;
            }

            var selected = (KeyValuePair<int, string>)cmbRide.SelectedItem;
            int rideID = selected.Key;
            string comment = txtRideComment.Text.Trim();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Review (TravelerID, Rating, Comment, ReviewDate, RideID, ModerationStatus)
            VALUES (@TravelerID, @Rating, @Comment, GETDATE(), @RideID, 'Pending')", con);

                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@Comment", string.IsNullOrEmpty(comment) ? (object)DBNull.Value : comment);
                cmd.Parameters.AddWithValue("@RideID", rideID);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Your ride review has been submitted!");
                ClearReviewInputs(cmbRide, groupBoxRideRating, txtRideComment);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void btnReviewGuide_Click(object sender, EventArgs e)
        {
            if (cmbGuide.SelectedItem == null)
            {
                MessageBox.Show("Please select a guide to review.");
                return;
            }

            int? rating = GetSelectedRating(groupBoxGuideRating);
            if (rating == null)
            {
                MessageBox.Show("Please select a rating.");
                return;
            }

            var selected = (KeyValuePair<int, string>)cmbGuide.SelectedItem;
            int guideID = selected.Key;
            string comment = txtGuideComment.Text.Trim();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Review (TravelerID, Rating, Comment, ReviewDate, GuideID, ModerationStatus)
            VALUES (@TravelerID, @Rating, @Comment, GETDATE(), @GuideID, 'Pending')", con);

                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@Comment", string.IsNullOrEmpty(comment) ? (object)DBNull.Value : comment);
                cmd.Parameters.AddWithValue("@GuideID", guideID);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Your guide review has been submitted!");
                ClearReviewInputs(cmbGuide, groupBoxGuideRating, txtGuideComment);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void btnReviewAcc_Click(object sender, EventArgs e)
        {
            if (cmbAccommodation.SelectedItem == null)
            {
                MessageBox.Show("Please select an accommodation to review.");
                return;
            }

            int? rating = GetSelectedRating(groupBoxAccRating); // your groupbox with radio buttons
            if (rating == null)
            {
                MessageBox.Show("Please select a rating.");
                return;
            }

            var selected = (KeyValuePair<int, string>)cmbAccommodation.SelectedItem;
            int accID = selected.Key;
            string comment = txtAccComment.Text.Trim(); // your textbox

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Review (TravelerID, Rating, Comment, ReviewDate, AccommodationID, ModerationStatus)
            VALUES (@TravelerID, @Rating, @Comment, GETDATE(), @AccID, 'Pending')", con);

                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@Comment", string.IsNullOrEmpty(comment) ? (object)DBNull.Value : comment);
                cmd.Parameters.AddWithValue("@AccID", accID);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Your review has been submitted for moderation!");
                ClearReviewInputs(cmbAccommodation, groupBoxAccRating, txtAccComment);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
