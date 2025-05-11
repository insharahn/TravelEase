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
    public partial class bookingReport : Form
    {
        public int TravelerID { get; set; } //so that traveler is retained when returning home
        const string reportPath = "dbfinalproject_interfaces.AbandonedBookingsReport.rdlc";
        public bookingReport()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");
        const string connectionString = "Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False";

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // 1. Abandonment Rate
                string queryAbandonment = @"
                    SELECT 
                        (CAST(SUM(CASE WHEN BookingStatus <> 'Confirmed' THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*)) * 100 AS AbandonmentRate
                    FROM Booking;";
                SqlDataAdapter daAbandonment = new SqlDataAdapter(queryAbandonment, con);
                DataTable dtAbandonment = new DataTable();
                daAbandonment.Fill(dtAbandonment);

                if (dtAbandonment.Rows.Count == 0)
                {
                    dtAbandonment.Rows.Add(0); //add a dummy row to avoid errors
                }

                // 2. Common Reasons
                string queryReasons = @"
                    SELECT ISNULL(CancellationReason, 'Unknown') AS CancellationReason, COUNT(*) AS CancellationCount
                    FROM Booking
                    WHERE BookingStatus = 'Cancelled'
                    GROUP BY CancellationReason;";
                SqlDataAdapter daReasons = new SqlDataAdapter(queryReasons, con);
                DataTable dtReasons = new DataTable();
                daReasons.Fill(dtReasons);

                if (dtReasons.Rows.Count == 0)
                {
                    dtReasons.Rows.Add("No Data", 0); 
                }

                // 3. Potential Revenue Loss
                string queryRevenueLoss = @"
                    SELECT SUM(TotalCost) AS PotentialRevenueLoss
                    FROM Booking
                    WHERE BookingStatus = 'Cancelled';";
                SqlDataAdapter daRevenueLoss = new SqlDataAdapter(queryRevenueLoss, con);
                DataTable dtRevenueLoss = new DataTable();
                daRevenueLoss.Fill(dtRevenueLoss);

                if (dtRevenueLoss.Rows.Count == 0 || dtRevenueLoss.Rows[0]["PotentialRevenueLoss"] == DBNull.Value)
                {
                    dtRevenueLoss.Rows.Add(0); //add a dummy row or handle null
                }

                ReportDataSource rdsAbandonment = new ReportDataSource("AbandonmentRateDataSet", dtAbandonment);
                ReportDataSource rdsReasons = new ReportDataSource("CommonReasonsDataSet", dtReasons);
                ReportDataSource rdsRevenueLoss = new ReportDataSource("RevenueLossDataSet", dtRevenueLoss);

                try
                {
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rdsAbandonment);
                    reportViewer1.LocalReport.DataSources.Add(rdsReasons);
                    reportViewer1.LocalReport.DataSources.Add(rdsRevenueLoss);
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
    }
}
   