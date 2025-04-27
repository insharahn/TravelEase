using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace dbfinalproject_interfaces
{
    public partial class travelerHome : Form
    {
        public int TravelerID { get; set; }
        //keep track of "pages" to load packages properly
        int currentPage = 0;
        int pageSize = 10;

        //filters
        // store filters
        string currentKeyword = "";
        int currentMinBudget = 0;
        int currentMaxBudget = 100000;
        int? currentDuration = null;
        int? currentGroupSize = null;
        string currentTripType = null;

        public travelerHome()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");

        //load packages w/ or w/out filters every time homepage loads
        private void LoadPackages(string keyword = "", int minBudget = 0, int maxBudget = 100000,
                                    int? duration = null, int? groupSize = null, string tripType = null)
        {
            flowLayoutPackages.Controls.Clear();

            /*
            //main query to get package information
            string packageQuery = @"
        SELECT 
            p.*,
            sp_accom.ProviderName AS AccommodationProvider,
            sp_transport.ProviderName AS TransportProvider,
            l.CityName,
            l.CountryName
            FROM Package p
            INNER JOIN Accommodation ac ON p.AccommodationID = ac.AccomodationID
            INNER JOIN Hotel h ON ac.HotelID = h.HotelID
            INNER JOIN ServiceProvider sp_accom ON h.ProviderID = sp_accom.ProviderId
            INNER JOIN Ride r on r.RideID = p.RideID
            INNER JOIN TransportService ts ON r.RideID = ts.TransportID
            INNER JOIN ServiceProvider sp_transport ON ts.ProviderID = sp_transport.ProviderId
            INNER JOIN Destination d ON p.DestinationID = d.DestinationID
            INNER JOIN Location l ON d.LocationID = l.LocationID
            ORDER BY p.PackageID
            OFFSET (@PageNumber * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY"; //10 at a time
            */

            //main query to get all package information + apply filters + load 10 packages per "page"
            string packageQuery = @"
            SELECT 
                p.*, 
                sp_accom.ProviderName AS AccommodationProvider,
                sp_transport.ProviderName AS TransportProvider,
                l.CityName,
                l.CountryName
            FROM Package p
            INNER JOIN Accommodation ac ON p.AccommodationID = ac.AccomodationID
            INNER JOIN Hotel h ON ac.HotelID = h.HotelID
            INNER JOIN ServiceProvider sp_accom ON h.ProviderID = sp_accom.ProviderId
            INNER JOIN Ride r on r.RideID = p.RideID
            INNER JOIN TransportService ts ON r.RideID = ts.TransportID
            INNER JOIN ServiceProvider sp_transport ON ts.ProviderID = sp_transport.ProviderId
            INNER JOIN Destination d ON p.DestinationID = d.DestinationID
            INNER JOIN Location l ON d.LocationID = l.LocationID
            WHERE 
                (@Keyword = '' OR p.Title LIKE '%' + @Keyword + '%' OR l.CityName LIKE '%' + @Keyword + '%')
                AND p.BasePrice BETWEEN @MinBudget AND @MaxBudget
                AND (@Duration IS NULL OR (p.Duration = @Duration OR (@Duration = 10 AND p.Duration >= 10)))
                AND (@GroupSize IS NULL OR (p.GroupSize = @GroupSize OR (@GroupSize = 20 AND p.GroupSize >= 20)))
                AND (@TripType IS NULL OR p.TripType = @TripType)
            ORDER BY p.PackageID
            OFFSET (@PageNumber * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;
            ";


            Dictionary<int, List<string>> packageActivities = new Dictionary<int, List<string>>();

            //get all activities for all packages we're loading via join
            string activitiesQuery = @"
            SELECT p.PackageID, a.ActivityName
            FROM Package p
            INNER JOIN PackageActivities pa ON pa.PackageID = p.PackageID
            INNER JOIN Activity a ON a.ActivityID = pa.ActivityID
            ORDER BY p.PackageID";

            //fill the packages
            using (SqlCommand cmd = new SqlCommand(activitiesQuery, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int packageId = reader.GetInt32(0); //convert package id to int to pass into activities
                        string activityName = reader.GetString(1);

                        if (!packageActivities.ContainsKey(packageId))
                        {
                            packageActivities[packageId] = new List<string>(); //index activities per id
                        }
                        packageActivities[packageId].Add(activityName); //add to list
                    }
                }
            }

            //get the packages
            using (SqlCommand cmd = new SqlCommand(packageQuery, con))
            {
                //pass pages to load more packages
                cmd.Parameters.AddWithValue("@PageNumber", currentPage);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                //pass the filter info
                cmd.Parameters.AddWithValue("@Keyword", keyword ?? "");
                cmd.Parameters.AddWithValue("@MinBudget", minBudget);
                cmd.Parameters.AddWithValue("@MaxBudget", maxBudget);
                cmd.Parameters.AddWithValue("@Duration", (object)duration ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GroupSize", (object)groupSize ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TripType", (object)tripType ?? DBNull.Value);


                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    int packageId = Convert.ToInt32(row["PackageID"]); //get package id!

                    //concatenate activities
                    string activities = packageActivities.ContainsKey(packageId)
                        ? string.Join(", ", packageActivities[packageId])
                        : "No activities";

                    //make a trip card with the right travalerid and package id (have to pass for wishlist functionality)
                    PackageCard tripCard = new PackageCard(this.TravelerID, packageId);

                    //fill the packages using setter
                    tripCard.SetData(
                        row["Title"].ToString(),
                        row["Description"].ToString(),
                        row["CityName"].ToString(),
                        row["CountryName"].ToString(),
                        row["AccommodationProvider"].ToString(),
                        row["TransportProvider"].ToString(),
                        activities,
                        row["GroupSize"].ToString(),
                        row["TripType"].ToString(),
                        row["SustainabilityScore"].ToString(),
                        row["Duration"].ToString(),
                        row["BasePrice"].ToString()
                    );

                    flowLayoutPackages.Controls.Add(tripCard);
                }
            }
        }


        private void travelerHome_Load(object sender, EventArgs e)
        {
            LoadPackages(); //upon loading, call generic load packages w/ no filter

            //default = any
            cmbDuration.SelectedIndex = 0;
            cmbGroupSize.SelectedIndex = 0;
            cmbTripType.SelectedIndex = 0;
        }

        private void flowLayoutPackages_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnViewWishlist_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerWishlist wishlist = new travelerWishlist(this.TravelerID);
            wishlist.Show();
        }

        private void btnFilter_Click(object sender, EventArgs e) //apply filters -> reload packages
        {
            //save filters globally so that when pages are reloaded, old filters remain
            currentKeyword = txtKeyword.Text.Trim();
            currentDuration = null;
            currentGroupSize = null;
            currentTripType = null;
            //parse min budget
            if (int.TryParse(txtMinPrice.Text.Trim(), out int minBudget))
            {
                currentMinBudget = minBudget;
            }
            else
            {
                currentMinBudget = 0; //default
            }
            //parse max budget
            if (int.TryParse(txtMaxPrice.Text.Trim(), out int maxBudget))
            {
                currentMaxBudget = maxBudget;
            }
            else
            {
                currentMaxBudget = 100000; //default
            }

            //get the rest
            if (cmbDuration.SelectedItem != null && cmbDuration.SelectedItem.ToString() != "Any")
            {
                if (int.TryParse(cmbDuration.SelectedItem.ToString(), out int dur))
                {
                    currentDuration = dur;
                }
            }
            else
            {
                currentDuration = null;
            }

            if (cmbGroupSize.SelectedItem != null && cmbGroupSize.SelectedItem.ToString() != "Any")
            {
                if (int.TryParse(cmbGroupSize.SelectedItem.ToString(), out int grp))
                {
                    currentGroupSize = grp;
                }
            }
            else
            {
                currentGroupSize = null;
            }

            if (cmbTripType.SelectedItem != null && cmbTripType.SelectedItem.ToString() != "Any")
            {
                currentTripType = cmbTripType.SelectedItem.ToString();
            }
            else
            {
                currentTripType = null;
            }


            currentPage = 0; // reset to page 0 when filtering
            LoadPackages(currentKeyword, currentMinBudget, currentMaxBudget, currentDuration, currentGroupSize, currentTripType);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadPackages(currentKeyword, currentMinBudget, currentMaxBudget, currentDuration, currentGroupSize, currentTripType);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
                currentPage--;
            LoadPackages(currentKeyword, currentMinBudget, currentMaxBudget, currentDuration, currentGroupSize, currentTripType);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            travelerLogin login = new travelerLogin();
            login.Show();
            this.Hide();
        }
    }
}
