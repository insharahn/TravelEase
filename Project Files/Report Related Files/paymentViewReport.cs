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
    public partial class paymentViewReport : Form
    {
        public int TravelerID { get; set; } //so that traveler is retained when returning home, can be changed to whoever's id's interface this report is in and you can set it using the object created
        /*for example:
         * destinationPopularity dest = new destinationPopulatiry();
         * dest.TravelerID = this.TravelerID; //assuming we r in traveler
         * dest.Show(); //will load for that traveler
         */
        const string reportPath = "dbfinalproject_interfaces.PaymentTransactionFraudReport.rdlc";
        public paymentViewReport()
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

                //oayment distribution
                string queryPaymentSuccessFailure = @"
                    SELECT 
                        PaymentStatus,
                        COUNT(*) AS Count
                    FROM Payment
                    GROUP BY PaymentStatus;";
                SqlDataAdapter daPaymentSuccessFailure = new SqlDataAdapter(queryPaymentSuccessFailure, con);
                DataTable dtPaymentSuccessFailure = new DataTable();
                daPaymentSuccessFailure.Fill(dtPaymentSuccessFailure);
                if (dtPaymentSuccessFailure.Rows.Count == 0) dtPaymentSuccessFailure.Rows.Add("No Status", 0, 0.00);
                ReportDataSource rdsPaymentSuccessFailure = new ReportDataSource("PaymentSuccessFailureDataSet", dtPaymentSuccessFailure);

                //chargeback rate
                string queryChargebackRate = @"
                    SELECT 
                        COUNT(CASE WHEN PaymentStatus = 'Chargeback' THEN 1 END) * 1.0 / COUNT(*) AS ChargebackRatePercent
                    FROM Payment;";
                SqlDataAdapter daChargebackRate = new SqlDataAdapter(queryChargebackRate, con);
                DataTable dtChargebackRate = new DataTable();
                daChargebackRate.Fill(dtChargebackRate);
                if (dtChargebackRate.Rows.Count == 0) dtChargebackRate.Rows.Add(0.00);
                ReportDataSource rdsChargebackRate = new ReportDataSource("ChargebackRateDataSet", dtChargebackRate);


                //success rate
                string querySuccessRate = @"
                    SELECT 
                        COUNT(CASE WHEN PaymentStatus = 'Success' THEN 1 END) * 1.0 / COUNT(*) AS CompletedPercent
                    FROM Payment;
                    ";
                SqlDataAdapter daSuccessRate = new SqlDataAdapter(querySuccessRate, con);
                DataTable dtSuccessRate = new DataTable();
                daSuccessRate.Fill(dtSuccessRate);
                if (dtSuccessRate.Rows.Count == 0) dtSuccessRate.Rows.Add(0.00);
                ReportDataSource rdsSuccessRate = new ReportDataSource("SuccessfulRateDataSet", dtSuccessRate);

                try
                {
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rdsPaymentSuccessFailure);
                    reportViewer1.LocalReport.DataSources.Add(rdsChargebackRate);
                    reportViewer1.LocalReport.DataSources.Add(rdsSuccessRate);
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading report: " + ex.Message + "\nInner Exception: " + (ex.InnerException?.Message ?? "None"));
                }
            }
        }
    }
}
