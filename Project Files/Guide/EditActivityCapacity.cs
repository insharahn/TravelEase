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
    public partial class EditActivityCapacity : Form
    {
        private int _ActivityId;

        public EditActivityCapacity(int activityId, int capacity)
        {
            InitializeComponent();
            _ActivityId = activityId;
            ActivityCapacity.Text = capacity.ToString();
        }

        SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!decimal.TryParse(ActivityCapacity.Text, out decimal capacity) || capacity <= 0)
                {
                    MessageBox.Show("Capacity must be a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check current participants
                int currentParticipants;
                using (SqlConnection connCheck = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    connCheck.Open();
                    string checkQuery = "SELECT CurrentParticipants FROM Activity WHERE ActivityID = @ActivityID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, connCheck);
                    checkCmd.Parameters.AddWithValue("@ActivityID", _ActivityId);
                    object result = checkCmd.ExecuteScalar();
                    currentParticipants = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }

                if (capacity <= currentParticipants)
                {
                    MessageBox.Show($"New capacity ({capacity}) must be greater than current participants ({currentParticipants}).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update Activity table
                conn.Open();
                string query = @"
                    UPDATE Activity
                    SET CapacityLimit = @capacity
                    WHERE ActivityID = @ActivityID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@capacity", capacity);
                cmd.Parameters.AddWithValue("@ActivityID", _ActivityId);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Failed to update capacity. Activity not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Capacity updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Capacity: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
