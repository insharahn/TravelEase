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
    public partial class EditRideArrival : Form
    {
        private readonly int _rideId;
        private readonly string _providerName;
        private DateTime _timeRequested;
        public EditRideArrival(int rideId, string providerName)
        {
            _rideId = rideId;
            _providerName = providerName;
            InitializeComponent();
            LoadRideData();
        }
        private void LoadRideData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                        SELECT ArrivalTime, TimeRequested
                        FROM Ride
                        WHERE RideID = @RideID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RideID", _rideId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            DateTimePicker dtpArrivalTime = (DateTimePicker)this.Controls["dtpArrivalTime"];
                            dtpArrivalTime.Value = reader.GetDateTime(0);
                        }
                        _timeRequested = reader.GetDateTime(1);
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Ride not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ride data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker dtpArrivalTime = (DateTimePicker)this.Controls["dtpArrivalTime"];
                DateTime newArrivalTime = dtpArrivalTime.Value;

                // Validate ArrivalTime is after TimeRequested
                if (newArrivalTime <= _timeRequested)
                {
                    MessageBox.Show("Arrival time must be after the requested departure time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Ride
                        SET ArrivalTime = @ArrivalTime
                        WHERE RideID = @RideID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RideID", _rideId);
                    cmd.Parameters.AddWithValue("@ArrivalTime", newArrivalTime);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Arrival time updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update arrival time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating arrival time: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
