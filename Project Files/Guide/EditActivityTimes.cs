using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Hotel_and_Transport
{
    public partial class EditActivityTimes : Form
    {
        private int _ActivityId;

        public EditActivityTimes(int activityId, DateTime? startTime, DateTime? endTime)
        {
            InitializeComponent();
            _ActivityId = activityId;
            dtpStartTime.Value = startTime ?? DateTime.Now;
            dtpEndTime.Value = endTime ?? DateTime.Now.AddHours(2);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startTime = dtpStartTime.Value;
                DateTime endTime = dtpEndTime.Value;

                if (endTime <= startTime)
                {
                    MessageBox.Show("End time must be after start time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection("Data Source=AABIA\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Activity
                        SET StartTime = @StartTime, EndTime = @EndTime
                        WHERE ActivityID = @ActivityID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.Parameters.AddWithValue("@ActivityID", _ActivityId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Failed to update times. Activity not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Times updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating times: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblRoomDescription_Click(object sender, EventArgs e)
        {

        }

    }
}