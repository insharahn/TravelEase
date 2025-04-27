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
    public partial class travelerWishlist : Form
    {
        private int travelerId;
        private SqlConnection con = new SqlConnection("Data Source=LAPTOP9158\\SQLEXPRESS;Initial Catalog='Project Data';Integrated Security=True;Encrypt=False");
        public travelerWishlist()
        {
            InitializeComponent();
        }
        public travelerWishlist(int TravelerID)
        {
            InitializeComponent();
            this.travelerId = TravelerID;
        }

        private void travelerWishlist_Load(object sender, EventArgs e)
        {
            LoadWishlist();
        }

        private void LoadWishlist()
        {
            try
            {
                con.Open();

                //get the wishlist info (title, price, date)
                SqlCommand cmd = new SqlCommand(
                    "SELECT P.PackageID, P.Title, P.BasePrice, W.AddedDate " +
                    "FROM Wishlist W " +
                    "JOIN Package P ON W.PackageID = P.PackageID " +
                    "WHERE W.TravelerID = @travelerID", con);

                cmd.Parameters.AddWithValue("@travelerID", travelerId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                flowLayoutPanelWishlist.Controls.Clear(); // clear old stuff if any

                //make a panel with every item being a wishlist entry
                foreach (DataRow row in dt.Rows)
                {
                    //panel for each entry
                    Panel itemPanel = new Panel();
                    itemPanel.Width = 420;  
                    itemPanel.Height = 80; 
                    itemPanel.BorderStyle = BorderStyle.FixedSingle;
                    itemPanel.Padding = new Padding(10);
                    itemPanel.Margin = new Padding(8);

                    //title label like title - $price
                    Label lblTitle = new Label();
                    lblTitle.Text = $"{row["Title"]} - ${Convert.ToDecimal(row["BasePrice"]):0.00}";
                    lblTitle.Font = new Font("Sitka Banner", 14, FontStyle.Bold);
                    lblTitle.Location = new Point(10, 5);
                    lblTitle.AutoSize = true;

                    //date label underneat
                    Label lblDate = new Label();
                    lblDate.Text = $"Added on {Convert.ToDateTime(row["AddedDate"]).ToShortDateString()}";
                    lblDate.Font = new Font("Sitka Banner", 10, FontStyle.Italic);
                    lblDate.Location = new Point(10, 40);
                    lblDate.AutoSize = true;

                    //remove button on the side
                    Button btnRemove = new Button();
                    btnRemove.Text = "Remove";
                    btnRemove.Tag = row["PackageID"];
                    btnRemove.Size = new Size(90, 35);
                    btnRemove.Font = new Font("Mongolian Baiti", 10, FontStyle.Bold);
                    btnRemove.ForeColor = Color.GhostWhite;
                    btnRemove.BackColor = Color.FromArgb(1, 63, 63);
                    btnRemove.FlatStyle = FlatStyle.Flat;
                    btnRemove.FlatAppearance.BorderSize = 0;
                    btnRemove.Location = new Point(itemPanel.Width - btnRemove.Width - 10, 25);
                    btnRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    btnRemove.Click += BtnRemove_Click;

                    //add the controls to the panel
                    itemPanel.Controls.Add(lblTitle);
                    itemPanel.Controls.Add(lblDate);
                    itemPanel.Controls.Add(btnRemove);

                    flowLayoutPanelWishlist.Controls.Add(itemPanel); //add the panel to the flow layout
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading wishlist: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e) //handles deletion of wishlist item
        {
            Button btn = sender as Button;
            int packageId = (int)btn.Tag;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Wishlist WHERE TravelerID = @travelerID AND PackageID = @packageID", con);

                cmd.Parameters.AddWithValue("@travelerID", travelerId);
                cmd.Parameters.AddWithValue("@packageID", packageId);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Package removed from wishlist.");
                con.Close();
                LoadWishlist(); // refresh

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing package: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerHome home = new travelerHome();
            home.TravelerID = travelerId; //set traveler id so wishlist is viewable even upon return
            home.Show();
        }
    }

}
