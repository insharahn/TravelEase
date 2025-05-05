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
    public partial class travelerBookings : Form
    {
        public int TravelerID { get; set; }
        public travelerBookings()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False";
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");


        public travelerBookings(int tid)
        {
            TravelerID = tid;
            InitializeComponent();
        }

        public void OpenTab(int tabIndex) //open form at specific tab
        {
            tabControl1.SelectedIndex = tabIndex;
        }


        private void travelerBookings_Load(object sender, EventArgs e)
        {
            //make sure all of travelers requests have been made bookings
            InsertMissingBookings();

            //load the flow layout panels
            LoadPendingBookings();
            LoadConfirmedBookings();
            LoadTravelHistory();

        }

        private void InsertMissingBookings() //REQUEST -> BOOKING WALI INSERTION ON THE BASIS OF TRAVELER ID
        {
            try
            {
                con.Open();

                //insert the missing bookings
                    string insertQuery = @"
                INSERT INTO Booking (RequestID, TotalCost, BookingDate, BookingStatus, CancellationReason)
                SELECT r.RequestID, 0, NULL, 'Pending', NULL
                FROM Request r
                LEFT JOIN Booking b ON r.RequestID = b.RequestID
                WHERE r.TravelerID = @TravelerID AND b.RequestID IS NULL";

                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                insertCmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting bookings: " + ex.Message);
                con.Close();
            }
        }

        private void LoadPendingBookings() //pending bookings!
        {
            try
            {
                con.Open();

                string query = @"
                SELECT b.BookingID, r.TripSourceType,
                       ISNULL(p.Title, 'Custom Package') AS Title,
                       l.CityName, l.CountryName,
                       r.PreferredStartDate,
                       b.TotalCost, pay.PaymentStatus
                FROM Booking b
                JOIN Request r ON b.RequestID = r.RequestID
                LEFT JOIN Package p ON r.PackageID = p.PackageID
                LEFT JOIN CustomTrip c ON r.CustomTripID = c.CustomTripID
                JOIN Destination d ON 
                    (r.TripSourceType = 'Package' AND p.DestinationID = d.DestinationID)
                    OR (r.TripSourceType = 'Custom' AND c.DestinationID = d.DestinationID)
                JOIN Location l ON d.LocationID = l.LocationID
                LEFT JOIN Payment pay ON b.BookingID = pay.BookingID
                WHERE r.TravelerID = @TravelerID AND b.BookingStatus = 'Pending'";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                flpPendingBookings.Controls.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    //format the layout panel
                    Panel panel = new Panel
                    {
                        Width = 500,
                        Height = 110,
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(10),
                        Margin = new Padding(10)
                    };

                    string destination = $"{row["CityName"]}, {row["CountryName"]}";
                    string cost = $"Total Cost: ${Convert.ToDecimal(row["TotalCost"]):0.00}";
                    //string startDate = row["PreferredStartDate"] == DBNull.Value ? "" :
                    //                  $"Start Date: {Convert.ToDateTime(row["PreferredStartDate"]).ToShortDateString()}";
                    string startDate = row["PreferredStartDate"] == DBNull.Value ? "" :
                   $"Start Date: {((DateTime)row["PreferredStartDate"]).ToString("yyyy-MM-dd")}";




                    Label lblTitle = new Label
                    {
                        Text = row["Title"].ToString(),
                        Font = new Font("Sitka Banner", 14, FontStyle.Bold),
                        AutoSize = true,
                        ForeColor = Color.FromArgb(43, 53, 53),
                        Location = new Point(10, 5)
                    };

                    Label lblDest = new Label
                    {
                        Text = destination,
                        Font = new Font("Sitka Banner", 11, FontStyle.Regular),
                        AutoSize = true,
                        Location = new Point(10, 35)
                    };

                    Label lblDate = new Label
                    {
                        Text = startDate,
                        Font = new Font("Sitka Banner", 10, FontStyle.Italic),
                        AutoSize = true,
                        Location = new Point(10, 60)
                    };

                    Label lblCost = new Label
                    {
                        Text = cost,
                        Font = new Font("Sitka Banner", 10, FontStyle.Italic),
                        AutoSize = true,
                        Location = new Point(200, 60)
                    };


                    Button btnPay = new Button
                    {
                        Text = "Pay Now",
                        Size = new Size(90, 35),
                        BackColor = Color.FromArgb(43, 53, 53),
                        ForeColor = Color.White,
                        Location = new Point(400, 20),
                        Tag = row["BookingID"]
                    };

                    string paymentStatus = row["PaymentStatus"]?.ToString();
                    bool isPaid = paymentStatus == "Success";
                    btnPay.Visible = !isPaid; //only visible if not paid
                    Label lblStatus = new Label
                    {
                        Text = isPaid ? "Payment confirmed, booking confirmation pending" : "Pending payment",
                        Font = new Font("Sitka Banner", 9, FontStyle.Italic),
                        AutoSize = true,
                        ForeColor = isPaid ? Color.Black : Color.FromArgb(34, 53, 53),
                        Location = new Point(10, 90)
                    };

                    
                    Button btnCancel = new Button
                    {
                        Text = "Cancel",
                        Size = new Size(90, 35),
                        BackColor = Color.FromArgb(43, 53, 53),
                        ForeColor = Color.White,
                        Location = new Point(400, 60),
                        Tag = row["BookingID"]
                    };

                    //event handlers for when our buttons r clicked
                    btnCancel.Click += BtnCancel_Click;
                    btnPay.Click += BtnPay_Click;

                    panel.Controls.Add(lblTitle);
                    panel.Controls.Add(lblDest);
                    panel.Controls.Add(lblDate);
                    panel.Controls.Add(lblCost);
                    panel.Controls.Add(btnPay);
                    panel.Controls.Add(btnCancel);
                    panel.Controls.Add(lblStatus);

                    flpPendingBookings.Controls.Add(panel);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading pending bookings: " + ex.Message);
                con.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e) //cancel clicked, ask for reason & remove from view
        {
            Button btn = sender as Button;
            int bookingId = (int)btn.Tag;

            ComboBox reasonBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 250
            };

            reasonBox.Items.AddRange(new object[]
            {
            "High Price", "Change of Plans", "Poor Service / Quality", "Technical Issues",
            "Found a Better Deal", "Schedule Conflict", "Insufficient Information", "Other", "--No Reason--"
            }); //no reason cause i cant have null and other != null

            DialogResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel Booking",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            using (Form reasonForm = new Form())
            {
                reasonForm.Text = "Cancellation Reason";
                reasonForm.Width = 350;
                reasonForm.Height = 150;
                reasonForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                reasonForm.StartPosition = FormStartPosition.CenterParent;
                reasonForm.MinimizeBox = false;
                reasonForm.MaximizeBox = false;

                Label lbl = new Label
                {
                    Text = "Reason for cancellation:",
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                reasonBox.Location = new Point(10, 35);

                Button confirmBtn = new Button
                {
                    Text = "Confirm",
                    DialogResult = DialogResult.OK,
                    Location = new Point(10, 70),
                    Width = 100
                };

                reasonForm.Controls.Add(lbl);
                reasonForm.Controls.Add(reasonBox);
                reasonForm.Controls.Add(confirmBtn);
                reasonForm.AcceptButton = confirmBtn;

                if (reasonForm.ShowDialog() == DialogResult.OK) //update booking table w reason
                {
                    string reason = reasonBox.SelectedItem?.ToString();
                    if (reason == "--No Reason--") 
                           reason = null;


                    try
                    {
                        con.Open();
                        SqlCommand update = new SqlCommand(
                            "UPDATE Booking SET BookingStatus = 'Cancelled', CancellationReason = @reason WHERE BookingID = @id", con);
                        update.Parameters.AddWithValue("@reason", (object)reason ?? DBNull.Value);
                        update.Parameters.AddWithValue("@id", bookingId);
                        update.ExecuteNonQuery();
                        con.Close();

                        // Remove the booking panel from view
                        Control panel = btn.Parent;
                        flpPendingBookings.Controls.Remove(panel);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to cancel booking: " + ex.Message);
                        con.Close();
                    }
                }
            }
        }
        private void BtnPay_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int bookingId = (int)btn.Tag;

            Panel panel = btn.Parent as Panel;

            string title = "";
            string city = "";
            string country = "";
            string date = "";
            double cost = 0;

            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is Label lbl)
                {
                    if (lbl.Font.Style == FontStyle.Bold) //title
                        title = lbl.Text;
                    else if (lbl.Text.Contains(", ")) //destination
                    {
                        string[] parts = lbl.Text.Split(',');
                        if (parts.Length == 2)
                        {
                            city = parts[0].Trim();
                            country = parts[1].Trim();
                        }
                    }
                    else if (lbl.Text.StartsWith("Start Date:")) //date
                        date = lbl.Text.Replace("Start Date: ", "").Trim();  //in yyyy-MM-dd format
                    else if (lbl.Text.StartsWith("Total Cost:")) //cost
                        cost = double.Parse(lbl.Text.Replace("Total Cost: $", ""));
                }
            }

            //send to payment
            this.Hide();
            travelerPayment payment = new travelerPayment(TravelerID, title, city, country, date, cost, bookingId);
            payment.Show();

            //refresh pending bookings after payment
            LoadPendingBookings();
        }



        private void LoadConfirmedBookings()
        {
            try
            {
                con.Open();

                string query = @"
                SELECT b.BookingID, ISNULL(p.Title, 'Custom Package') AS Title,
                       l.CityName, l.CountryName,
                       r.PreferredStartDate,
                       b.TotalCost
                FROM Booking b
                JOIN Request r ON b.RequestID = r.RequestID
                LEFT JOIN Package p ON r.PackageID = p.PackageID
                LEFT JOIN CustomTrip c ON r.CustomTripID = c.CustomTripID
                JOIN Destination d ON 
                    (r.TripSourceType = 'Package' AND p.DestinationID = d.DestinationID)
                    OR (r.TripSourceType = 'Custom' AND c.DestinationID = d.DestinationID)
                JOIN Location l ON d.LocationID = l.LocationID
                WHERE r.TravelerID = @TravelerID AND b.BookingStatus = 'Confirmed'";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                flpConfirmedBookings.Controls.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    Panel panel = new Panel
                    {
                        Width = 500,
                        Height = 150,
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(10),
                        Margin = new Padding(10),
                        AutoScroll = true
                    };

                    int bookingID = Convert.ToInt32(row["BookingID"]);
                    string destination = $"{row["CityName"]}, {row["CountryName"]}";
                    string startDate = row["PreferredStartDate"] == DBNull.Value ? "" :
                                       $"Start Date: {Convert.ToDateTime(row["PreferredStartDate"]).ToShortDateString()}";
                    string cost = $"Total Cost: ${Convert.ToDecimal(row["TotalCost"]):0.00}";

                    Label lblTitle = new Label
                    {
                        Text = row["Title"].ToString(),
                        Font = new Font("Sitka Banner", 14, FontStyle.Bold),
                        AutoSize = true,
                        Location = new Point(10, 5)
                    };

                    Label lblDest = new Label
                    {
                        Text = destination,
                        Font = new Font("Sitka Banner", 11, FontStyle.Regular),
                        AutoSize = true,
                        Location = new Point(10, 35)
                    };

                    Label lblDate = new Label
                    {
                        Text = startDate,
                        Font = new Font("Sitka Banner", 10, FontStyle.Italic),
                        AutoSize = true,
                        Location = new Point(10, 60)
                    };

                    Label lblCost = new Label
                    {
                        Text = cost,
                        Font = new Font("Sitka Banner", 10, FontStyle.Italic),
                        AutoSize = true,
                        Location = new Point(200, 60)
                    };

                    //fetch digital passes
                    List<string> passes = new List<string>();
                    using (SqlCommand passCmd = new SqlCommand(@"
                    SELECT PassType, ExpiryDate 
                    FROM DigitalPasses 
                    WHERE BookingID = @BookingID", con))
                    {
                        passCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        using (SqlDataReader reader = passCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string passType = reader["PassType"].ToString();
                                DateTime expiry = Convert.ToDateTime(reader["ExpiryDate"]);
                                passes.Add($"{passType} (Valid before {expiry:dd MMM yyyy})");
                            }
                        }
                    }

                    string passText = passes.Count > 0
                        ? "Digital Passes:\n- " + string.Join("\n- ", passes)
                        : "No digital passes";

                    Label lblPasses = new Label
                    {
                        Text = passText,
                        Font = new Font("Sitka Banner", 9, FontStyle.Regular),
                        AutoSize = true,
                        Location = new Point(10, 85)
                    };

                    panel.Controls.Add(lblTitle);
                    panel.Controls.Add(lblDest);
                    panel.Controls.Add(lblDate);
                    panel.Controls.Add(lblCost);
                    panel.Controls.Add(lblPasses);

                    flpConfirmedBookings.Controls.Add(panel);
                }


                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading confirmed bookings: " + ex.Message);
                con.Close();
            }
        }

        private void LoadTravelHistory()
        {
            try
            {
                con.Open();

                string query = @"
                SELECT b.BookingID,
                       ISNULL(p.Title, 'Custom Package') AS Title,
                       l.CityName, l.CountryName,
                       r.PreferredStartDate,
                       b.TotalCost
                FROM Booking b
                JOIN Request r ON b.RequestID = r.RequestID
                LEFT JOIN Package p ON r.PackageID = p.PackageID
                LEFT JOIN CustomTrip c ON r.CustomTripID = c.CustomTripID
                JOIN Destination d ON 
                    (r.TripSourceType = 'Package' AND p.DestinationID = d.DestinationID)
                    OR (r.TripSourceType = 'Custom' AND c.DestinationID = d.DestinationID)
                JOIN Location l ON d.LocationID = l.LocationID
                WHERE r.TravelerID = @TravelerID 
                      AND b.BookingStatus = 'Confirmed'
                      AND r.PreferredStartDate IS NOT NULL
                      AND r.PreferredStartDate < GETDATE()";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TravelerID", TravelerID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                flpTravelHistory.Controls.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    Panel panel = CreateInfoPanel(row, includeReviewButton: true);
                    flpTravelHistory.Controls.Add(panel);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading travel history: " + ex.Message);
                con.Close();
            }
        }

        private Panel CreateInfoPanel(DataRow row, bool includeReviewButton) //hekper function for confirmed and travel history i got lazy sorry
        {
            Panel panel = new Panel
            {
                Width = 500,
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(10)
            };

            string destination = $"{row["CityName"]}, {row["CountryName"]}";
            string startDate = row["PreferredStartDate"] == DBNull.Value ? "" :
                               $"Start Date: {Convert.ToDateTime(row["PreferredStartDate"]).ToShortDateString()}";
            string cost = $"Total Cost: ${Convert.ToDecimal(row["TotalCost"]):0.00}";

            Label lblTitle = new Label
            {
                Text = row["Title"].ToString(),
                Font = new Font("Sitka Banner", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 5)
            };

            Label lblDest = new Label
            {
                Text = destination,
                Font = new Font("Sitka Banner", 11, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(10, 35)
            };

            Label lblDate = new Label
            {
                Text = startDate,
                Font = new Font("Sitka Banner", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(10, 60)
            };

            Label lblCost = new Label
            {
                Text = cost,
                Font = new Font("Sitka Banner", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(200, 60)
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblDest);
            panel.Controls.Add(lblDate);
            panel.Controls.Add(lblCost);

            if (includeReviewButton)
            {
                Button btnReview = new Button
                {
                    Text = "Review",
                    Size = new Size(90, 35),
                    BackColor = Color.FromArgb(43, 53, 53),
                    ForeColor = Color.White,
                    Location = new Point(400, 30),
                    Tag = row["BookingID"]
                };

                btnReview.Click += BtnReview_Click; //event handler for when the the button is clicked
                panel.Controls.Add(btnReview);
            }

            return panel;
        }

        private void BtnReview_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int bookingID = Convert.ToInt32(btn.Tag);

            //open the review form — pass TravelerID and BookingID
            this.Hide();
            travelerReview reviewForm = new travelerReview(TravelerID);
            reviewForm.Show();
        }



        private void btnViewWishlist_Click(object sender, EventArgs e) //this is the home button btw
        {
            this.Hide();
            travelerHome home = new travelerHome();
            home.TravelerID = this.TravelerID;
            home.Show();
        }

        private void flpPendingBookings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flpConfirmedBookings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flpTravelHistory_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerHome home = new travelerHome();
            home.TravelerID = this.TravelerID;
            home.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerHome home = new travelerHome();
            home.TravelerID = this.TravelerID;
            home.Show();
        }
    }
}
