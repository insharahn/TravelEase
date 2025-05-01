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
    public partial class EditRidePrice : Form
    {
        private int _RideId;

        public EditRidePrice(int RideID, decimal price)
        {
            InitializeComponent();
            _RideId = RideID;
            RidePrice.Text = price.ToString("F2");
        }
        SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!decimal.TryParse(RidePrice.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Price per night must be a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update Ride table
                {
                    conn.Open();
                    string query = @"
                        UPDATE Ride
                        SET Price = @Price
                        WHERE RideID = @RideID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@RideID", _RideId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Failed to update price. Ride not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Price updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating price: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
