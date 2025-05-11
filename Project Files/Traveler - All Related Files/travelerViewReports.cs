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
    public partial class travelerViewReports : Form
    {
        public int TravelerID { get; set; } //so that traveler is retained when returning home
        const string reportPath = "dbfinalproject_interfaces.TravelerDemPrefReport.rdlc"; //so that this can be changed easily later

        public travelerViewReports()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");
        const string connectionString = "Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False";


        private void travelerViewReports_Load(object sender, EventArgs e)
        {
            cmbReportChoice.Items.Clear();  
            cmbReportChoice.Items.AddRange(new string[]
               {
                    "Age Distribution",
                    "Nationality Distribution",
                    "Preferred Trip Types",
                    "Preferred Destinations",
                    "Spending Habits"
               });
            cmbReportChoice.SelectedIndex = 0; //load on first index
            LoadAgeChart(); //which is the age chart

            cmbReportChoice.SelectedIndexChanged += comboBox1_SelectedIndexChanged; //event handler for changing the dropdown item
        }

        private void LoadTripTypeChart() //trip type query
        {
            string query = @"
             SELECT COALESCE(ct.TripType, p.TripType) AS TripType, 
               COALESCE(SUM(ct.NumBooked), 0) + COALESCE(SUM(p.NumBooked), 0) AS NumBooked
                FROM (
                    SELECT c.TripType, COUNT(*) AS NumBooked
                    FROM Request r
                    INNER JOIN CustomTrip c ON c.CustomTripID = r.CustomTripID
                    INNER JOIN Booking b ON b.RequestID = r.RequestID AND b.BookingStatus = 'Confirmed'
                    WHERE r.TripSourceType = 'Custom'
                    GROUP BY c.TripType
                ) ct
                FULL OUTER JOIN (
                    SELECT c.TripType, COUNT(*) AS NumBooked
                    FROM Request r
                    INNER JOIN Package c ON c.PackageID = r.PackageID
                    INNER JOIN Booking b ON b.RequestID = r.RequestID AND b.BookingStatus = 'Confirmed'
                    WHERE r.TripSourceType = 'Package'
                    GROUP BY c.TripType
                ) p ON ct.TripType = p.TripType
                GROUP BY COALESCE(ct.TripType, p.TripType);";


            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data returned from the query. The chart will be empty.");
                    return;
                }

                ReportDataSource rds = new ReportDataSource("triptypebookingPLEASE", dt);

                //dummy data sources for the rest so they load during runtime
                DataTable dDummy = new DataTable();
                dDummy.Columns.Add("Dest", typeof(string));
                dDummy.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsBook = new ReportDataSource("bookingsPerDestTT", dDummy);

                DataTable dtNationality = new DataTable();
                dtNationality.Columns.Add("CountryName", typeof(string));
                dtNationality.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsDestination = new ReportDataSource("nationalityTraveler", dtNationality);

                DataTable dtAge = new DataTable();
                dtAge.Columns.Add("AgeGroup", typeof(string));
                dtAge.Columns.Add("NumTravelers", typeof(int));
                ReportDataSource rdsAge = new ReportDataSource("ageTraveler", dtAge);

                DataTable dtSpendingTimeSeries = new DataTable();
                dtSpendingTimeSeries.Columns.Add("BookingDate", typeof(DateTime));
                dtSpendingTimeSeries.Columns.Add("TotalCost", typeof(decimal));
                ReportDataSource rdsSpendingTimeSeries = new ReportDataSource("spending", dtSpendingTimeSeries);
                try
                {
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.LocalReport.DataSources.Add(rdsBook); //bookings
                    reportViewer1.LocalReport.DataSources.Add(rdsDestination); //destination
                    reportViewer1.LocalReport.DataSources.Add(rdsAge); //age
                    reportViewer1.LocalReport.DataSources.Add(rdsSpendingTimeSeries);
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("ReportType", "TripType")); //select trip type chart
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("TravelerID", TravelerID.ToString()));

                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading report: " + ex.Message);
                }

            }

        }

        private void LoadDestinationChart()
        {
            //only load top 10 bc otherwise the graph is hard to read
            string query = @"
                SELECT TOP 10 COALESCE(ct.Destination, p.Destination) AS Destination, 
                       COALESCE(SUM(ct.NumBooked), 0) + COALESCE(SUM(p.NumBooked), 0) AS NumBooked
                FROM (
                    SELECT l.CityName + ', ' + l.CountryName AS Destination, COUNT(*) AS NumBooked
                    FROM Request r
                    INNER JOIN CustomTrip c ON c.CustomTripID = r.CustomTripID
                    INNER JOIN Booking b ON b.RequestID = r.RequestID AND b.BookingStatus = 'Confirmed'
                    INNER JOIN Destination d ON d.DestinationID = c.DestinationID
                    INNER JOIN Location l ON l.LocationID = d.LocationID
                    WHERE r.TripSourceType = 'Custom'
                    GROUP BY l.CityName, l.CountryName
                ) ct
                FULL OUTER JOIN (
                    SELECT l.CityName + ', ' + l.CountryName AS Destination, COUNT(*) AS NumBooked
                    FROM Request r
                    INNER JOIN Package c ON c.PackageID = r.PackageID
                    INNER JOIN Booking b ON b.RequestID = r.RequestID AND b.BookingStatus = 'Confirmed'
                    INNER JOIN Destination d ON d.DestinationID = c.DestinationID
                    INNER JOIN Location l ON l.LocationID = d.LocationID
                    WHERE r.TripSourceType = 'Package'
                    GROUP BY l.CityName, l.CountryName
                ) p ON ct.Destination = p.Destination
                GROUP BY COALESCE(ct.Destination, p.Destination)
                ORDER BY NumBooked DESC;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
              

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data returned for Destinations. The chart will be empty.");
                    return;
                }

                ReportDataSource rdsBook = new ReportDataSource("bookingsPerDestTT", dt);

                //dummies
                DataTable dtTripType = new DataTable();
                dtTripType.Columns.Add("TripType", typeof(string));
                dtTripType.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsTripType = new ReportDataSource("triptypebookingPLEASE", dtTripType);

                DataTable dtNationality = new DataTable();
                dtNationality.Columns.Add("CountryName", typeof(string));
                dtNationality.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsDestination = new ReportDataSource("nationalityTraveler", dtNationality);

                DataTable dtAge = new DataTable();
                dtAge.Columns.Add("AgeGroup", typeof(string));
                dtAge.Columns.Add("NumTravelers", typeof(int));
                ReportDataSource rdsAge = new ReportDataSource("ageTraveler", dtAge);

                DataTable dtSpendingTimeSeries = new DataTable();
                dtSpendingTimeSeries.Columns.Add("BookingDate", typeof(DateTime));
                dtSpendingTimeSeries.Columns.Add("TotalCost", typeof(decimal));
                ReportDataSource rdsSpendingTimeSeries = new ReportDataSource("spending", dtSpendingTimeSeries);

                try
                {
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rdsBook); //actual
                    reportViewer1.LocalReport.DataSources.Add(rdsTripType); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsDestination); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsAge); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsSpendingTimeSeries);
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("ReportType", "Destination")); //show destination chart
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("TravelerID", TravelerID.ToString()));

                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Destination report: " + ex.Message);
                }
            }
        }

        private void LoadNationalityChart()
        {
            string query = @"
            SELECT TOP 10 CountryName, COUNT(*) AS NumTravelers
            FROM Traveler t
            INNER JOIN Location l ON l.LocationID = t.Nationality
            GROUP BY CountryName
            ORDER BY NumTravelers DESC;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data returned for Nationality Distribution. The chart will be empty.");
                    return;
                }

                ReportDataSource rds = new ReportDataSource("nationalityTraveler", dt);

                //dummy data sources for other datasets
                DataTable dtTripType = new DataTable();
                dtTripType.Columns.Add("TripType", typeof(string));
                dtTripType.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsTripType = new ReportDataSource("triptypebookingPLEASE", dtTripType);

                DataTable dtDestination = new DataTable();
                dtDestination.Columns.Add("Destination", typeof(string));
                dtDestination.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsDestination = new ReportDataSource("bookingsPerDestTT", dtDestination);

                DataTable dtAge = new DataTable();
                dtAge.Columns.Add("AgeGroup", typeof(string));
                dtAge.Columns.Add("NumTravelers", typeof(int));
                ReportDataSource rdsAge = new ReportDataSource("ageTraveler", dtAge);

                DataTable dtSpendingTimeSeries = new DataTable();
                dtSpendingTimeSeries.Columns.Add("BookingDate", typeof(DateTime));
                dtSpendingTimeSeries.Columns.Add("TotalCost", typeof(decimal));
                ReportDataSource rdsSpendingTimeSeries = new ReportDataSource("spending", dtSpendingTimeSeries);

                try
                {
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds); //actual data source
                    reportViewer1.LocalReport.DataSources.Add(rdsTripType); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsDestination); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsAge); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsSpendingTimeSeries);
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("ReportType", "Nationality"));
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("TravelerID", TravelerID.ToString()));

                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Nationality report: " + ex.Message + "\nInner Exception: " + (ex.InnerException?.Message ?? "None"));
                }
            }
        }

        private void LoadAgeChart()
        {
            string query = @"
            SELECT 
              CASE 
                WHEN Age BETWEEN 0 AND 18 THEN '0-18'
                WHEN Age BETWEEN 18 AND 25 THEN '18-25'
                WHEN Age BETWEEN 26 AND 35 THEN '26-35'
                WHEN Age BETWEEN 36 AND 50 THEN '36-50'
                WHEN Age > 50 THEN '50+'
                ELSE 'Other'
              END AS AgeGroup,
              COUNT(*) AS NumTravelers
            FROM (
              SELECT DATEDIFF(YEAR, DateOfBirth, GETDATE()) AS Age
              FROM Traveler
            ) AS Derived
            GROUP BY 
              CASE 
                WHEN Age BETWEEN 0 AND 18 THEN '0-18'
                WHEN Age BETWEEN 18 AND 25 THEN '18-25'
                WHEN Age BETWEEN 26 AND 35 THEN '26-35'
                WHEN Age BETWEEN 36 AND 50 THEN '36-50'
                WHEN Age > 50 THEN '50+'
                ELSE 'Other'
              END;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data returned for Age Distribution. The chart will be empty.");
                    return;
                }

                ReportDataSource rds = new ReportDataSource("ageTraveler", dt);

                //dummy
                DataTable dtTripType = new DataTable();
                dtTripType.Columns.Add("TripType", typeof(string));
                dtTripType.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsTripType = new ReportDataSource("triptypebookingPLEASE", dtTripType);

                DataTable dtDestination = new DataTable();
                dtDestination.Columns.Add("Destination", typeof(string));
                dtDestination.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsDestination = new ReportDataSource("bookingsPerDestTT", dtDestination);

                DataTable dtNationality = new DataTable();
                dtNationality.Columns.Add("CountryName", typeof(string));
                dtNationality.Columns.Add("NumTravelers", typeof(int));
                ReportDataSource rdsNationality = new ReportDataSource("nationalityTraveler", dtNationality);

                DataTable dtSpendingTimeSeries = new DataTable();
                dtSpendingTimeSeries.Columns.Add("BookingDate", typeof(DateTime));
                dtSpendingTimeSeries.Columns.Add("TotalCost", typeof(decimal));
                ReportDataSource rdsSpendingTimeSeries = new ReportDataSource("spending", dtSpendingTimeSeries);

                try
                {
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds); //actual data source
                    reportViewer1.LocalReport.DataSources.Add(rdsTripType); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsDestination); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsNationality); //dummy
                    reportViewer1.LocalReport.DataSources.Add(rdsSpendingTimeSeries);
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("TravelerID", TravelerID.ToString()));
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("ReportType", "Age"));
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Age Distribution report: " + ex.Message + "\nInner Exception: " + (ex.InnerException?.Message ?? "None"));
                }
            }
        }

        private void LoadSpendingChart()
        {
            //average budget
            string queryAvg = @"
            SELECT AVG(b.TotalCost) as AverageBudget
            FROM Traveler t
            INNER JOIN Request r ON r.TravelerID = t.TravelerID
            INNER JOIN Booking b ON b.RequestID = r.RequestID
            WHERE t.TravelerID = @TravelerID;";

            //spending over time
            string queryTimeSeries = @"
            SELECT CAST(r.DateRequested AS DATE) as BookingDate, b.TotalCost
            FROM Traveler t
            INNER JOIN Request r ON r.TravelerID = t.TravelerID
            INNER JOIN Booking b ON b.RequestID = r.RequestID
            WHERE t.TravelerID = @TravelerID
            ORDER BY BookingDate;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //fetch time series data
                SqlCommand cmdTimeSeries = new SqlCommand(queryTimeSeries, con);
                cmdTimeSeries.Parameters.AddWithValue("@TravelerID", TravelerID);
                SqlDataAdapter daTimeSeries = new SqlDataAdapter(cmdTimeSeries);
                DataTable dtTimeSeries = new DataTable();
                daTimeSeries.Fill(dtTimeSeries);

                if (dtTimeSeries.Rows.Count == 0)
                {
                    MessageBox.Show("No spending data available for this traveler (Spending Over Time). The chart will be empty.");
                }

                //fetch avg value
                SqlCommand cmdAvg = new SqlCommand(queryAvg, con);
                cmdAvg.Parameters.AddWithValue("@TravelerID", TravelerID);
                SqlDataAdapter daAvg = new SqlDataAdapter(cmdAvg);
                DataTable dtAvg = new DataTable();
                daAvg.Fill(dtAvg);

                decimal avgBudget = 0m;
                if (dtAvg.Rows.Count > 0 && dtAvg.Rows[0]["AverageBudget"] != DBNull.Value)
                {
                    avgBudget = Convert.ToDecimal(dtAvg.Rows[0]["AverageBudget"]);
                }
               

                dtTimeSeries.Columns.Add("AverageBudget", typeof(decimal)); //add avg budget as a straight line
                foreach (DataRow row in dtTimeSeries.Rows)
                {
                    row["AverageBudget"] = avgBudget;
                }

                ReportDataSource rdsTimeSeries = new ReportDataSource("spending", dtTimeSeries);

                //dummy data sources for other datasets
                DataTable dtTripType = new DataTable();
                dtTripType.Columns.Add("TripType", typeof(string));
                dtTripType.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsTripType = new ReportDataSource("triptypebookingPLEASE", dtTripType);

                DataTable dtDestination = new DataTable();
                dtDestination.Columns.Add("Destination", typeof(string));
                dtDestination.Columns.Add("NumBooked", typeof(int));
                ReportDataSource rdsDestination = new ReportDataSource("bookingsPerDestTT", dtDestination);

                DataTable dtNationality = new DataTable();
                dtNationality.Columns.Add("CountryName", typeof(string));
                dtNationality.Columns.Add("NumTravelers", typeof(int));
                ReportDataSource rdsNationality = new ReportDataSource("nationalityTraveler", dtNationality);

                DataTable dtAge = new DataTable();
                dtAge.Columns.Add("AgeGroup", typeof(string));
                dtAge.Columns.Add("NumTravelers", typeof(int));
                ReportDataSource rdsAge = new ReportDataSource("ageTraveler", dtAge);

                try
                {
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rdsTimeSeries);
                    reportViewer1.LocalReport.DataSources.Add(rdsTripType);
                    reportViewer1.LocalReport.DataSources.Add(rdsDestination);
                    reportViewer1.LocalReport.DataSources.Add(rdsNationality);
                    reportViewer1.LocalReport.DataSources.Add(rdsAge);
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("ReportType", "Spending"));
                    reportViewer1.LocalReport.SetParameters(new ReportParameter("TravelerID", TravelerID.ToString()));
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Spending Habits report: " + ex.Message + "\nInner Exception: " + (ex.InnerException?.Message ?? "None"));
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           //load based on selection
            string selectedReport = cmbReportChoice.SelectedItem.ToString();
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;

            switch (selectedReport)
            {
                case "Age Distribution":
                    LoadAgeChart();
                    break;
                case "Preferred Trip Types":
                    LoadTripTypeChart();
                    break;
                case "Preferred Destinations":
                    LoadDestinationChart();
                    break;
                case "Nationality Distribution":
                    LoadNationalityChart();
                    break;
                case "Spending Habits":
                    LoadSpendingChart();
                    break;
                default:
                    MessageBox.Show("Selected report type does not exist.");
                    break;
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReports_Click(object sender, EventArgs e) //actually the home button
        {
            this.Hide();
            travelerHome home = new travelerHome();
            home.TravelerID = this.TravelerID;
            home.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerHome home = new travelerHome();
            home.TravelerID = this.TravelerID;
            home.Show();
        }

        private void cmbReportChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
