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
using Microsoft.Reporting.WinForms;


namespace dbfinalproject_interfaces
{
    public partial class TotalBookingAndRevenue : Form
    {
        public int TravelerID { get; set; } // So that traveler is retained when returning home
        const string reportPath = "dbfinalproject_interfaces.Reports.TripBookingRevenueReport.rdlc";
        const string connectionString = "Data Source=DESKTOP-J0FKTUC\\SQLEXPRESS;Initial Catalog=\"Travel ease2\";Integrated Security=True;Encrypt=False";
        public TotalBookingAndRevenue()
        {
            InitializeComponent();
        }

       

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            LoadReportData();
        }
        private void LoadReportData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // 1. Total Bookings
                string queryTotalBookings = @"
                   SELECT COUNT(*) AS TotalBooking,BookingStatus
                  FROM Booking
                  Group by BookingStatus";
                SqlDataAdapter daTotalBookings = new SqlDataAdapter(queryTotalBookings, con);
                DataTable dtTotalBookings = new DataTable();
                daTotalBookings.Fill(dtTotalBookings);

                if (dtTotalBookings.Rows.Count == 0)
                {
                    dtTotalBookings.Rows.Add(0); // Add a dummy row to avoid errors
                }


                // Create ReportDataSource objects
                ReportDataSource rdsTotalBookings = new ReportDataSource("TotalBookings", dtTotalBookings);


                // 2.Revenue By Category 
                /*
                 
	                SELECT 
		                TripType,
		                SUM(TotalCost) AS TotalCost
	                FROM (
				                -- For Package-based bookings
				                SELECT 
					                tc.TripTypeID,
					                tc.TripType,
					                SUM(b.TotalCost) AS TotalCost
				                FROM Booking b
				                INNER JOIN Request r ON b.RequestID = r.RequestID
				                INNER JOIN Package p ON r.PackageID = p.PackageID
				                INNER JOIN TripCategory tc ON p.TripType = tc.TripType
				                INNER JOIN Payment py on py.BookingID = b.BookingID
				                WHERE r.TripSourceType = 'Package' and  py.PaymentStatus = 'Success' and b.BookingStatus = 'Confirmed'
				                GROUP BY tc.TripTypeID, tc.TripType

				                UNION ALL

				                -- For CustomTrip-based bookings
				                SELECT 
					                tc.TripTypeID,
					                tc.TripType,
					                SUM(b.TotalCost) AS TotalCost
				                FROM Booking b
				                INNER JOIN Request r ON b.RequestID = r.RequestID
				                INNER JOIN CustomTrip ct ON r.CustomTripID = ct.CustomTripID
				                right outer JOIN TripCategory tc ON ct.TripType = tc.TripType
				                INNER JOIN Payment py on py.BookingID = b.BookingID
				                WHERE r.TripSourceType = 'Custom' and  py.PaymentStatus = 'Success' and b.BookingStatus = 'Confirmed'
				                GROUP BY tc.TripTypeID, tc.TripType
	                ) AS Combined
	                GROUP BY TripType;


                 
                 */
                //the ReportDataSource name is RevenueByTripCategory 

                // 2. Revenue by Category
                string queryRevenueByCategory = @"
                    SELECT 
                        TripType,
                        SUM(TotalCost) AS TotalCost
                    FROM (
                        -- For Package-based bookings
                        SELECT 
                            tc.TripTypeID,
                            tc.TripType,
                            SUM(b.TotalCost) AS TotalCost
                        FROM Booking b
                        INNER JOIN Request r ON b.RequestID = r.RequestID
                        INNER JOIN Package p ON r.PackageID = p.PackageID
                        INNER JOIN TripCategory tc ON p.TripType = tc.TripType
                        INNER JOIN Payment py ON py.BookingID = b.BookingID
                        WHERE r.TripSourceType = 'Package' AND py.PaymentStatus = 'Success' AND b.BookingStatus = 'Confirmed'
                        GROUP BY tc.TripTypeID, tc.TripType

                        UNION ALL

                        -- For CustomTrip-based bookings
                        SELECT 
                            tc.TripTypeID,
                            tc.TripType,
                            SUM(b.TotalCost) AS TotalCost
                        FROM Booking b
                        INNER JOIN Request r ON b.RequestID = r.RequestID
                        INNER JOIN CustomTrip ct ON r.CustomTripID = ct.CustomTripID
                        RIGHT OUTER JOIN TripCategory tc ON ct.TripType = tc.TripType
                        INNER JOIN Payment py ON py.BookingID = b.BookingID
                        WHERE r.TripSourceType = 'Custom' AND py.PaymentStatus = 'Success' AND b.BookingStatus = 'Confirmed'
                        GROUP BY tc.TripTypeID, tc.TripType
                    ) AS Combined
                    GROUP BY TripType";
                SqlDataAdapter daRevenueByCategory = new SqlDataAdapter(queryRevenueByCategory, con);
                DataTable dtRevenueByCategory = new DataTable();
                daRevenueByCategory.Fill(dtRevenueByCategory);

                if (dtRevenueByCategory.Rows.Count == 0)
                {
                    dtRevenueByCategory.Rows.Add("No Data", 0); // Add a dummy row for TripType, TotalCost
                }

                // Create ReportDataSource objects
                ReportDataSource rdsRevenueByCategory = new ReportDataSource("RevenueByTripCategory", dtRevenueByCategory);

                // 3. Revenue by Capacity Type
                string queryRevenueByCapacityType = @"
                    SELECT CapacityType,
                        SUM(TotalCost) AS TotalCost
                    FROM (
                        -- For Package-based bookings
                        SELECT 
                            p.CapacityType,
                            SUM(b.TotalCost) AS TotalCost
                        FROM Booking b
                        INNER JOIN Request r ON b.RequestID = r.RequestID
                        INNER JOIN Package p ON r.PackageID = p.PackageID
                        INNER JOIN Payment py ON py.BookingID = b.BookingID
                        WHERE r.TripSourceType = 'Package' AND py.PaymentStatus = 'Success' AND b.BookingStatus = 'Confirmed'
                        GROUP BY p.CapacityType

                        UNION ALL

                        -- For CustomTrip-based bookings
                        SELECT 
                            ct.CapacityType,
                            SUM(b.TotalCost) AS TotalCost
                        FROM Booking b
                        INNER JOIN Request r ON b.RequestID = r.RequestID
                        INNER JOIN CustomTrip ct ON r.CustomTripID = ct.CustomTripID
                        INNER JOIN Payment py ON py.BookingID = b.BookingID
                        WHERE r.TripSourceType = 'Custom' AND py.PaymentStatus = 'Success' AND b.BookingStatus = 'Confirmed'
                        GROUP BY ct.CapacityType
                    ) AS Combined
                    GROUP BY CapacityType";
                SqlDataAdapter daRevenueByCapacityType = new SqlDataAdapter(queryRevenueByCapacityType, con);
                DataTable dtRevenueByCapacityType = new DataTable();
                daRevenueByCapacityType.Fill(dtRevenueByCapacityType);

                if (dtRevenueByCapacityType.Rows.Count == 0)
                {
                    dtRevenueByCapacityType.Rows.Add("No Data", 0); // Add a dummy row for CapacityType, TotalCost
                }

                // Create ReportDataSource objects
                ReportDataSource rdsRevenueByCapacityType = new ReportDataSource("RevenueByCategory", dtRevenueByCapacityType);

                // 4. Revenue by Duration
                string queryRevenueByDuration = @"
                    SELECT Duration,
                        SUM(TotalCost) AS TotalCost
                    FROM (
                        -- For Package-based bookings
                        SELECT 
                            p.Duration,
                            SUM(b.TotalCost) AS TotalCost
                        FROM Booking b
                        INNER JOIN Request r ON b.RequestID = r.RequestID
                        INNER JOIN Package p ON r.PackageID = p.PackageID
                        INNER JOIN Payment py ON py.BookingID = b.BookingID
                        WHERE r.TripSourceType = 'Package' AND py.PaymentStatus = 'Success' AND b.BookingStatus = 'Confirmed'
                        GROUP BY p.Duration

                        UNION ALL

                        -- For CustomTrip-based bookings
                        SELECT 
                            ct.Duration,
                            SUM(b.TotalCost) AS TotalCost
                        FROM Booking b
                        INNER JOIN Request r ON b.RequestID = r.RequestID
                        INNER JOIN CustomTrip ct ON r.CustomTripID = ct.CustomTripID
                        INNER JOIN Payment py ON py.BookingID = b.BookingID
                        WHERE r.TripSourceType = 'Custom' AND py.PaymentStatus = 'Success' AND b.BookingStatus = 'Confirmed'
                        GROUP BY ct.Duration
                    ) AS Combined
                    GROUP BY Duration";
                SqlDataAdapter daRevenueByDuration = new SqlDataAdapter(queryRevenueByDuration, con);
                DataTable dtRevenueByDuration = new DataTable();
                daRevenueByDuration.Fill(dtRevenueByDuration);

                if (dtRevenueByDuration.Rows.Count == 0)
                {
                    dtRevenueByDuration.Rows.Add("No Data", 0); // Add a dummy row for Duration, TotalCost
                }

                // Create ReportDataSource objects
                ReportDataSource rdsRevenueByDuration = new ReportDataSource("RevenueByDuration", dtRevenueByDuration);

                // 5. Cancellation Rate
                string queryCancellationRate = @"
                    SELECT 
                        'Cancelled' AS BookingStatus, 
                        (COUNT(b.BookingID) * 1.0 / (SELECT COUNT(r.BookingID) * 1.0 FROM Booking r) * 100.0) AS Percentage
                    FROM Booking b
                    WHERE b.BookingStatus = 'Cancelled'

                    UNION ALL

                    SELECT 
                        'Non-Cancelled' AS BookingStatus, 
                        100.0 - (COUNT(b.BookingID) * 1.0 / (SELECT COUNT(r.BookingID) * 1.0 FROM Booking r) * 100.0) AS Percentage
                    FROM Booking b
                    WHERE b.BookingStatus = 'Cancelled'";
                SqlDataAdapter daCancellationRate = new SqlDataAdapter(queryCancellationRate, con);
                DataTable dtCancellationRate = new DataTable();
                daCancellationRate.Fill(dtCancellationRate);

                if (dtCancellationRate.Rows.Count == 0)
                {
                    dtCancellationRate.Rows.Add("No Data", 0); // Add a dummy row for BookingStatus, Percentage
                }

                ReportDataSource rdsCancellationRate = new ReportDataSource("CancellationRate", dtCancellationRate);

                // 6. Peak Booking Periods
                string queryPeakBookingPeriods = @"
                    SELECT 
                        DATENAME(MONTH, b.BookingDate) + ' ' + CAST(YEAR(b.BookingDate) AS VARCHAR) AS BookingPeriod,
                        COUNT(b.BookingID) AS NumberOfBookings
                    FROM Booking b
                    WHERE b.BookingStatus = 'Confirmed'
                    GROUP BY 
                        DATENAME(MONTH, b.BookingDate),
                        YEAR(b.BookingDate)
                    ORDER BY NumberOfBookings DESC";
                SqlDataAdapter daPeakBookingPeriods = new SqlDataAdapter(queryPeakBookingPeriods, con);
                DataTable dtPeakBookingPeriods = new DataTable();
                daPeakBookingPeriods.Fill(dtPeakBookingPeriods);

                if (dtPeakBookingPeriods.Rows.Count == 0)
                {
                    dtPeakBookingPeriods.Rows.Add("No Data", 0); // Add a dummy row for BookingPeriod, NumberOfBookings
                }

                ReportDataSource rdsPeakBookingPeriods = new ReportDataSource("PeakBookingPeriods", dtPeakBookingPeriods);

                // 7. Average Booking Value
                string queryAverageBookingValue = @"
                    SELECT 
                        AVG(CAST(b.TotalCost AS FLOAT)) AS AverageBookingValue
                    FROM Booking b
                    WHERE b.BookingStatus = 'Confirmed'";
                SqlDataAdapter daAverageBookingValue = new SqlDataAdapter(queryAverageBookingValue, con);
                DataTable dtAverageBookingValue = new DataTable();
                daAverageBookingValue.Fill(dtAverageBookingValue);

                if (dtAverageBookingValue.Rows.Count == 0)
                {
                    dtAverageBookingValue.Rows.Add(0.0); // Add a dummy row for AverageBookingValue
                }

                ReportDataSource rdsAverageBookingValue = new ReportDataSource("AverageBookingValue", dtAverageBookingValue);

                try
                {
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rdsTotalBookings);
                    reportViewer1.LocalReport.DataSources.Add(rdsRevenueByCategory);
                    reportViewer1.LocalReport.DataSources.Add(rdsRevenueByCapacityType);
                    reportViewer1.LocalReport.DataSources.Add(rdsRevenueByDuration);
                    reportViewer1.LocalReport.DataSources.Add(rdsCancellationRate);
                    reportViewer1.LocalReport.DataSources.Add(rdsPeakBookingPeriods);
                    reportViewer1.LocalReport.DataSources.Add(rdsAverageBookingValue);
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading report: " + ex.Message + "\nInner Exception: " + (ex.InnerException?.Message ?? "None"));
                }

            }
        }
        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
