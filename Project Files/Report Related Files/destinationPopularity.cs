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
    public partial class destinationPopularity : Form
    {
        public int TravelerID { get; set; } //so that traveler is retained when returning home, can be changed to whoever's id's interface this report is in and you can set it using the object created
        /*for example:
         * destinationPopularity dest = new destinationPopulatiry();
         * dest.TravelerID = this.TravelerID; //assuming we r in traveler
         * dest.Show(); //will load for that traveler
         */
        const string reportPath = "dbfinalproject_interfaces.DestinationPopularityReport.rdlc";
        public destinationPopularity()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");
        const string connectionString = "Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False";

        private void LoadReportData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // 1. Emerging Destinations by request count
                string queryEmerging = @"
                 SELECT TOP 10 
                    Combined.CityName + ', ' + Combined.CountryName AS DestinationName, 
                    COUNT(*) AS RequestCount
                FROM (
                    SELECT r.DateRequested, d.DestinationID, l.CityName, l.CountryName
                    FROM Request r
                    INNER JOIN CustomTrip c ON c.CustomTripID = r.CustomTripID AND r.TripSourceType = 'Custom'
                    INNER JOIN Destination d ON d.DestinationID = c.DestinationID
                    INNER JOIN Location l ON l.LocationID = d.LocationID
                    WHERE DATEDIFF(MONTH, r.DateRequested, GETDATE()) <= 6

                    UNION ALL

                    SELECT r.DateRequested, d.DestinationID, l.CityName, l.CountryName
                    FROM Request r
                    INNER JOIN Package c ON c.PackageID = r.PackageID AND r.TripSourceType = 'Package'
                    INNER JOIN Destination d ON d.DestinationID = c.DestinationID
                    INNER JOIN Location l ON l.LocationID = d.LocationID
                    WHERE DATEDIFF(MONTH, r.DateRequested, GETDATE()) <= 6
                ) AS Combined
                GROUP BY Combined.CityName, Combined.CountryName
                ORDER BY RequestCount DESC;";

                SqlDataAdapter daEmerging = new SqlDataAdapter(queryEmerging, con);
                DataTable dtEmerging = new DataTable();
                daEmerging.Fill(dtEmerging);

                if (dtEmerging.Rows.Count == 0)
                {
                    dtEmerging.Rows.Add("No Data", 0);
                }

                // 2. Most-Booked Destinations by booking count
                string queryMostBooked = @"
                    SELECT TOP 10 
                        Combined.CityName + ', ' + Combined.CountryName AS DestinationName, 
                        COUNT(*) AS BookingCount
                    FROM (
                        SELECT d.DestinationID, l.CityName, l.CountryName
                        FROM Request r
                        INNER JOIN Booking b ON r.RequestID = b.RequestID AND b.BookingStatus = 'Confirmed'
                        INNER JOIN Package c ON c.PackageID = r.PackageID AND r.TripSourceType = 'Package'
                        INNER JOIN Destination d ON d.DestinationID = c.DestinationID
                        INNER JOIN Location l ON l.LocationID = d.LocationID

                        UNION ALL

                        SELECT d.DestinationID, l.CityName, l.CountryName
                        FROM Request r
                        INNER JOIN Booking b ON r.RequestID = b.RequestID AND b.BookingStatus = 'Confirmed'
                        INNER JOIN CustomTrip c ON c.CustomTripID = r.CustomTripID AND r.TripSourceType = 'Custom'
                        INNER JOIN Destination d ON d.DestinationID = c.DestinationID
                        INNER JOIN Location l ON l.LocationID = d.LocationID
                    ) AS Combined
                    GROUP BY Combined.CityName, Combined.CountryName
                    ORDER BY BookingCount DESC;";

                SqlDataAdapter daMostBooked = new SqlDataAdapter(queryMostBooked, con);
                DataTable dtMostBooked = new DataTable();
                daMostBooked.Fill(dtMostBooked);

                if (dtMostBooked.Rows.Count == 0)
                {
                    dtMostBooked.Rows.Add("No Data", 0);
                }

                // 3. Seasonal Trends by request count
                string querySeasonal = @"
                    WITH CombinedRequests AS (
                    SELECT 
                        DATEPART(MONTH, r.DateRequested) AS RequestMonth,
                        CASE 
                            WHEN DATEPART(MONTH, r.DateRequested) IN (12, 1, 2) THEN 'Winter'
                            WHEN DATEPART(MONTH, r.DateRequested) IN (3, 4, 5) THEN 'Spring'
                            WHEN DATEPART(MONTH, r.DateRequested) IN (6, 7, 8) THEN 'Summer'
                            WHEN DATEPART(MONTH, r.DateRequested) IN (9, 10, 11) THEN 'Fall'
                        END AS Season,
                        l.CityName + ', ' + l.CountryName AS DestinationName
                    FROM Request r
                    INNER JOIN Package c ON c.PackageID = r.PackageID AND r.TripSourceType = 'Package'
                    INNER JOIN Destination d ON d.DestinationID = c.DestinationID
                    INNER JOIN Location l ON l.LocationID = d.LocationID

                    UNION ALL

                    SELECT 
                        DATEPART(MONTH, r.DateRequested) AS RequestMonth,
                        CASE 
                            WHEN DATEPART(MONTH, r.DateRequested) IN (12, 1, 2) THEN 'Winter'
                            WHEN DATEPART(MONTH, r.DateRequested) IN (3, 4, 5) THEN 'Spring'
                            WHEN DATEPART(MONTH, r.DateRequested) IN (6, 7, 8) THEN 'Summer'
                            WHEN DATEPART(MONTH, r.DateRequested) IN (9, 10, 11) THEN 'Fall'
                        END AS Season,
                        l.CityName + ', ' + l.CountryName AS DestinationName
                    FROM Request r
                    INNER JOIN CustomTrip c ON c.CustomTripID = r.CustomTripID AND r.TripSourceType = 'Custom'
                    INNER JOIN Destination d ON d.DestinationID = c.DestinationID
                    INNER JOIN Location l ON l.LocationID = d.LocationID
                    ),
                    TopDestinations AS (
                        SELECT DestinationName
                        FROM CombinedRequests
                        GROUP BY DestinationName
                        ORDER BY COUNT(*) DESC
                        OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY
                    )
                    SELECT 
                        cr.Season,
                        cr.DestinationName,
                        COUNT(*) AS RequestCount
                    FROM CombinedRequests cr
                    WHERE cr.DestinationName IN (SELECT DestinationName FROM TopDestinations)
                    GROUP BY cr.Season, cr.DestinationName
                    ORDER BY cr.Season, RequestCount DESC;";

                SqlDataAdapter daSeasonal = new SqlDataAdapter(querySeasonal, con);
                DataTable dtSeasonal = new DataTable();
                daSeasonal.Fill(dtSeasonal);

                if (dtSeasonal.Rows.Count == 0)
                {
                    dtSeasonal.Rows.Add("Winter", "No Data", 0);
                }

                // 4. Traveler Satisfaction Score by rating
                string querySatisfaction = @"
                SELECT
                l.CityName + ', ' + l.CountryName AS DestinationName,
                AVG(CAST(r.Rating AS FLOAT)) AS AvgRating
                FROM Review r
                LEFT JOIN Accommodation a ON r.AccommodationID = a.AccomodationID
                LEFT JOIN Hotel h ON h.HotelID = a.HotelID
                LEFT JOIN ServiceProvider sp1 ON sp1.ProviderId = h.ProviderID
                LEFT JOIN Guide g ON r.GuideID = g.GuideID
                LEFT JOIN ServiceProvider sp2 ON sp2.ProviderId = g.GuideID
                LEFT JOIN Ride ri ON r.RideID = ri.RideID
                LEFT JOIN TransportService ts ON ts.TransportID = ri.TransportID
                LEFT JOIN ServiceProvider sp3 ON sp3.ProviderId = ts.ProviderID
                LEFT JOIN Destination d ON 
                    d.DestinationID = COALESCE(sp1.DestinationID, sp2.DestinationID, sp3.DestinationID)
                INNER JOIN Location l ON l.LocationID = d.LocationID
                WHERE r.ModerationStatus = 'Approved'
                    AND d.DestinationID IS NOT NULL
                GROUP BY l.CityName, l.CountryName
                HAVING AVG(CAST(r.Rating AS FLOAT)) IS NOT NULL
                ORDER BY AvgRating DESC;
                ";

                SqlDataAdapter daSatisfaction = new SqlDataAdapter(querySatisfaction, con);
                DataTable dtSatisfaction = new DataTable();
                daSatisfaction.Fill(dtSatisfaction);

                if (dtSatisfaction.Rows.Count == 0)
                {
                    dtSatisfaction.Rows.Add("No Data", 0);
                }

                ReportDataSource rdsEmerging = new ReportDataSource("EmergingDestinationsDataSet", dtEmerging);
                ReportDataSource rdsMostBooked = new ReportDataSource("MostBookedDestinationsDataSet", dtMostBooked);
                ReportDataSource rdsSeasonal = new ReportDataSource("SeasonalTrendsDataSet", dtSeasonal);
                ReportDataSource rdsSatisfaction = new ReportDataSource("SatisfactionScoreDataSet", dtSatisfaction);

                try
                {
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rdsEmerging);
                    reportViewer1.LocalReport.DataSources.Add(rdsMostBooked);
                    reportViewer1.LocalReport.DataSources.Add(rdsSeasonal);
                    reportViewer1.LocalReport.DataSources.Add(rdsSatisfaction);
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

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
