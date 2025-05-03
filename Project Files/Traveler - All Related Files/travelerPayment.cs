using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Data.SqlClient;

namespace dbfinalproject_interfaces
{
    public partial class travelerPayment : Form
    {
        int TravelerID { get; set; }
        string Title  { get; set; }
        string City { get; set; }
        string Country { get; set; }
        string PreferredStartDate { get; set; }
        double TotalCost { get; set; }
        int BookingID { get; set; }
        public travelerPayment()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False";
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");

        public travelerPayment(int travelerId, string title, string city, string country, string preferredStartDate, double totalCost, int bid)
        {
            InitializeComponent();
            TravelerID = travelerId;
            Title = title;
            City = city;
            Country = country;
            PreferredStartDate = preferredStartDate;
            TotalCost = totalCost;
            BookingID = bid;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerHome home = new travelerHome(TravelerID);
            home.Show();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerBookings book = new travelerBookings(TravelerID);
            book.Show();
        }

        private void flpDetails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void travelerPayment_Load(object sender, EventArgs e)
        {
            //make the same kinda panel in bookings
            Panel panel = new Panel
            {
                Width = 500,
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(10)
            };

            Label lblTitle = new Label
            {
                Text = Title,
                Font = new Font("Sitka Banner", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 5)
            };

            Label lblDest = new Label
            {
                Text = $"{City}, {Country}",
                Font = new Font("Sitka Banner", 11),
                AutoSize = true,
                Location = new Point(10, 35)
            };

            Label lblDate = new Label
            {
                Text = $"Start Date: {PreferredStartDate}",
                Font = new Font("Sitka Banner", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(10, 60)
            };

            Label lblCost = new Label
            {
                Text = $"Total Cost: ${TotalCost:0.00}",
                Font = new Font("Sitka Banner", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(200, 60)
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblDest);
            panel.Controls.Add(lblDate);
            panel.Controls.Add(lblCost);

            flpDetails.Controls.Add(panel);
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            //update payments, booking, request, digital passes

            string paymentMethod = cmbMethod.SelectedItem?.ToString(); //get payment method
            if (string.IsNullOrEmpty(paymentMethod))
            {
                MessageBox.Show("Please select a payment method.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    //1) INSERT INTO PAYMENT W/ TODAY'S DATE AND SUCCESSFULL TRANSACTION STATUS
                    string insertPayment = @"
                    INSERT INTO Payment (BookingID, PaymentDate, PaymentMethod, PaymentStatus)
                    VALUES (@BookingID, GETDATE(), @Method, 'Success');";

                    using (SqlCommand cmd = new SqlCommand(insertPayment, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", BookingID);
                        cmd.Parameters.AddWithValue("@Method", paymentMethod);
                        cmd.ExecuteNonQuery();
                    }

                    //2) UPDATE BOOKINGDATE IN BOOKING, STATUS REMAINS PENDING UNTIL THE ADMIN / OPERATOR CHANGES IT
                    string updateBooking = @"
                    UPDATE Booking
                    SET BookingDate = GETDATE()
                    WHERE BookingID = @BookingID;";

                    using (SqlCommand cmd = new SqlCommand(updateBooking, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", BookingID);
                        cmd.ExecuteNonQuery();
                    }

                    //3) UPDATE REQUEST KA TABLE SO ALL PAID STATUSES ARE MARKED AS PAID
                    string updateRequest = @"
                    UPDATE Request
                    SET AccomodationPaidStatus = 'Paid',
                        RidePaidStatus = 'Paid',
                        ActivityPaidStatus = 'Paid'
                    WHERE RequestID = (
                        SELECT RequestID FROM Booking WHERE BookingID = @BookingID
                    );";

                    using (SqlCommand cmd = new SqlCommand(updateRequest, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", BookingID);
                        cmd.ExecuteNonQuery();
                    }


                    //4) UPDATE DIGITAL PASSES BASED ON WHAT FLAGS WERE SET IN REQUESTS
                    string checkFlags = @"
                    SELECT ISNULL(c.AccommodationStatusFlag, p.AccommodationStatusFlag) AS Accommodation,
                           ISNULL(c.RideStatusFlag, p.RideStatusFlag) AS Ride,
                           ISNULL(c.ActivitiesStatusFlag, p.ActivityStatusFlag) AS Activities
                    FROM Booking b
                    INNER JOIN Request r ON r.RequestID = b.RequestID
                    LEFT JOIN CustomTrip c ON c.CustomTripID = r.CustomTripID
                    LEFT JOIN Package p ON p.PackageID = r.PackageID
                    WHERE b.BookingID = @BookingID;";

                    int accFlag = 0, rideFlag = 0, actFlag = 0;

                    using (SqlCommand cmd = new SqlCommand(checkFlags, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", BookingID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                accFlag = Convert.ToInt32(reader["Accommodation"]);
                                rideFlag = Convert.ToInt32(reader["Ride"]);
                                actFlag = Convert.ToInt32(reader["Activities"]);
                            }
                        }
                    }

                    //insert!!!!!!!!!
                    string insertPass = @"
                    INSERT INTO DigitalPasses (BookingID, PassType, IssueDate, ExpiryDate, PassStatus)
                    VALUES (@BookingID, @PassType, @IssueDate, @ExpiryDate, 'Active');";

                    //   calculate expiry date as preferred start + 20 days

                    //   DateTime preferredStartDate = DateTime.Parse(PreferredStartDate);

                    DateTime preferredStartDate;
                    string dateFormat = "yyyy-MM-dd";  //specify the exact format expected
                    if (!DateTime.TryParseExact(PreferredStartDate, dateFormat, null, System.Globalization.DateTimeStyles.None, out preferredStartDate))
                    {
                        MessageBox.Show($"Preferred Start Date: {PreferredStartDate}");
                        MessageBox.Show("Invalid date format for Preferred Start Date.");
                        return;  //exit if the date format is incorrect
                    }
                    DateTime expiryDate = preferredStartDate.AddDays(20);
                    DateTime now = DateTime.Now;  //get date and time


                    using (SqlCommand cmd = new SqlCommand(insertPass, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", BookingID);
                        cmd.Parameters.AddWithValue("@IssueDate", now);
                        cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                        cmd.Parameters.Add("@PassType", SqlDbType.VarChar); //empty placeholder to fill later

                        //add passes based on the flags
                        if (accFlag == 1)
                        {
                            cmd.Parameters["@PassType"].Value = "Hotel Voucher";
                            cmd.ExecuteNonQuery(); //execute query for each pass type so u have multiple passes per booking
                        }

                        if (rideFlag == 1)
                        {
                            cmd.Parameters["@PassType"].Value = "E-Ticket";
                            cmd.ExecuteNonQuery();
                        }

                        if (actFlag == 1)
                        {
                            cmd.Parameters["@PassType"].Value = "Activity Pass";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    //commit everything
                    tran.Commit();
                    MessageBox.Show("Payment successful and passes issued.");
                    conn.Close();

                    this.Hide();
                    travelerBookings booking = new travelerBookings(TravelerID);
                    booking.Show();
            }
                catch (Exception ex)
                {
                tran.Rollback();
                MessageBox.Show("Error during payment: " + ex.Message);
            }
        }
        }

    }
}
