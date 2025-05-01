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

        private void travelerBookings_Load(object sender, EventArgs e)
        {

        }
    }
}
