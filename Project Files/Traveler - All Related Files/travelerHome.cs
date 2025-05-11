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
        int? currentSusScore = null;
        List<string> selectedAccessibilities = new List<string>();
        List<string> selectedActivities = new List<string>();
        string currentOperator = null;

        public travelerHome()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");
        public travelerHome(int tid)
        {
            TravelerID = tid;
            InitializeComponent();
        }

        //load packages w/ or w/out filters every time homepage loads
        private void LoadPackages(string keyword = "", int minBudget = 0, int maxBudget = 100000,
                                    int? duration = null, int? groupSize = null, int? sustainabiltyScore = 0, 
                                    string tripType = null, List<string> accessibilities = null, string cOperator = null,
                                    List<string> selectedActivities = null)
        {
            flowLayoutPackages.Controls.Clear();

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
            INNER JOIN TransportService ts ON r.TransportID = ts.TransportID
            INNER JOIN ServiceProvider sp_transport ON ts.ProviderID = sp_transport.ProviderId
            INNER JOIN Destination d ON p.DestinationID = d.DestinationID
            INNER JOIN Location l ON d.LocationID = l.LocationID
            INNER JOIN Operator o ON p.OperatorID = o.OperatorID
            WHERE 
                (@Keyword = '' OR p.Title LIKE '%' + @Keyword + '%' OR l.CityName LIKE '%' + @Keyword + '%' OR l.CountryName LIKE '%' + @Keyword + '%')
                AND p.BasePrice BETWEEN @MinBudget AND @MaxBudget
                AND (@Duration IS NULL OR (p.Duration = @Duration OR (@Duration = 10 AND p.Duration >= 10)))
                AND (@GroupSize IS NULL OR (p.GroupSize = @GroupSize OR (@GroupSize = 20 AND p.GroupSize >= 20)))
                AND (@TripType IS NULL OR p.TripType = @TripType)
                AND (@SustainabilityScore IS NULL OR p.SustainabilityScore = @SustainabilityScore)
                AND (@Operator IS NULL OR o.CompanyName = @Operator)
                AND (
                    @ActivityCount = 0 OR p.PackageID IN (
                        SELECT pa.PackageID
                        FROM PackageActivities pa
                        INNER JOIN Activity a ON a.ActivityID = pa.ActivityID
                        WHERE a.ActivityName IN (/* dynamically inserted activity list */)
                        GROUP BY pa.PackageID
                        HAVING COUNT(DISTINCT a.ActivityName) = @ActivityCount
                    )
                )
                AND (
                    @AccessibilityCount = 0 OR p.PackageID IN (
                        SELECT acc.PackageID
                        FROM (
                            --hotel
                            SELECT p.PackageID, a.AccessibilityName
                            FROM Package p
                            INNER JOIN Accommodation ac ON p.AccommodationID = ac.AccomodationID
                            INNER JOIN Hotel h ON ac.HotelID = h.HotelID
                            INNER JOIN ServiceProvider sp ON sp.ProviderId = h.ProviderID
                            INNER JOIN Accessibility a ON a.ProviderID = sp.ProviderId

                            UNION

                            --ride
                            SELECT p.PackageID, a.AccessibilityName
                            FROM Package p
                            INNER JOIN Ride r ON r.RideID = p.RideID
                            INNER JOIN TransportService ts ON r.TransportID = ts.TransportID
                            INNER JOIN ServiceProvider sp ON sp.ProviderId = ts.ProviderID
                            INNER JOIN Accessibility a ON a.ProviderID = sp.ProviderId

                            UNION

                            --guide
                            SELECT p.PackageID, 'Sign Language' AS AccessibilityName
                            FROM Package p
                            INNER JOIN PackageActivities pa ON pa.PackageID = p.PackageID
                            INNER JOIN Activity act ON act.ActivityID = pa.ActivityID
                            INNER JOIN Guide g ON g.GuideID = act.GuideID
                            WHERE g.SignLanguageFlag = 1
                        ) acc
                        WHERE acc.AccessibilityName IN (/* dynamically inserted param list */)
                        GROUP BY acc.PackageID
                        HAVING COUNT(DISTINCT acc.AccessibilityName) = @AccessibilityCount
                    )
                )
            ORDER BY p.PackageID
            OFFSET (@PageNumber * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;
            ";

            // the parameters are automatically filled in by c# thank u c#


            Dictionary<int, List<string>> packageActivities = new Dictionary<int, List<string>>();

            //get all activities for all packages we're loading via join
            string activitiesQuery = @"
            SELECT p.PackageID, a.ActivityName
            FROM Package p
            INNER JOIN PackageActivities pa ON pa.PackageID = p.PackageID
            INNER JOIN Activity a ON a.ActivityID = pa.ActivityID
            ORDER BY p.PackageID";

            //build the parameter list (@accessibilty1, @accessibilty2, so on)
            if (accessibilities == null || accessibilities.Count == 0)
            {
                packageQuery = packageQuery.Replace("/* dynamically inserted param list */", "NULL");
            }
            else
            {
                string placeholder = string.Join(", ", accessibilities.Select((_, index) => $"@Accessibility{index}"));
                packageQuery = packageQuery.Replace("/* dynamically inserted param list */", placeholder);
            }

            //get the activities the same way
            if (selectedActivities == null || selectedActivities.Count == 0)
            {
                packageQuery = packageQuery.Replace("/* dynamically inserted activity list */", "NULL");
            }
            else
            {
                string activityParams = string.Join(", ", selectedActivities.Select((_, index) => $"@Activity{index}"));
                packageQuery = packageQuery.Replace("/* dynamically inserted activity list */", activityParams);
            }


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
                cmd.Parameters.AddWithValue("@SustainabilityScore", (object)sustainabiltyScore ?? DBNull.Value);
                if (accessibilities == null || accessibilities.Count == 0) //none
                {
                    cmd.Parameters.AddWithValue("@AccessibilityCount", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccessibilityCount", accessibilities.Count);
                    for (int i = 0; i < accessibilities.Count; i++) //add in
                        cmd.Parameters.AddWithValue($"@Accessibility{i}", accessibilities[i]);
                }
                cmd.Parameters.AddWithValue("@Operator", (object)cOperator ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ActivityCount", selectedActivities?.Count ?? 0);
                if (selectedActivities != null)
                {
                    for (int i = 0; i < selectedActivities.Count; i++)
                        cmd.Parameters.AddWithValue($"@Activity{i}", selectedActivities[i]);
                }


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
            LoadPackages(currentKeyword, currentMinBudget, currentMaxBudget, currentDuration, currentGroupSize, currentSusScore, currentTripType, selectedAccessibilities, currentOperator, selectedActivities);

            PopulateOperatorComboBox(); //fill operator based on existing values
            PopulateTripTypeComboBox(); //do the same for trip tupe


            //default = any
            cmbDuration.SelectedIndex = 0;
            cmbGroupSize.SelectedIndex = 0;
            cmbTripType.SelectedIndex = 0;
            cmbSusScore.SelectedIndex = 0;

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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            //validate price fields
            if (!string.IsNullOrWhiteSpace(txtMinPrice.Text) && !int.TryParse(txtMinPrice.Text, out _))
            {
                MessageBox.Show("Please enter a valid numeric value for Min Price.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtMaxPrice.Text) && !int.TryParse(txtMaxPrice.Text, out _))
            {
                MessageBox.Show("Please enter a valid numeric value for Max Price.");
                return;
            }

            if (int.TryParse(txtMinPrice.Text.Trim(), out int minBudget))
                currentMinBudget = minBudget;
            else
                currentMinBudget = 0;

            if (int.TryParse(txtMaxPrice.Text.Trim(), out int maxBudget))
                currentMaxBudget = maxBudget;
            else
                currentMaxBudget = 100000;

            if (currentMinBudget > currentMaxBudget)
            {
                MessageBox.Show("Minimum price cannot be greater than maximum price.");
                return;
            }

            //clean it keyword
            currentKeyword = txtKeyword.Text.Trim();

            //validate dropdowns

            //duration
            currentDuration = null;
            if (cmbDuration.SelectedItem != null && cmbDuration.SelectedItem.ToString() != "Any")
            {
                if (!int.TryParse(cmbDuration.SelectedItem.ToString(), out int dur))
                {
                    MessageBox.Show("Invalid duration selected.");
                    return;
                }
                currentDuration = dur;
            }

            //group size
            currentGroupSize = null;
            if (cmbGroupSize.SelectedItem != null && cmbGroupSize.SelectedItem.ToString() != "Any")
            {
                if (!int.TryParse(cmbGroupSize.SelectedItem.ToString(), out int grp))
                {
                    MessageBox.Show("Invalid group size selected.");
                    return;
                }
                currentGroupSize = grp;
            }

            //trip type
            currentTripType = null;
            if (cmbTripType.SelectedItem != null && cmbTripType.SelectedItem.ToString() != "Any")
                currentTripType = cmbTripType.SelectedItem.ToString();

            //sustainabilty score
            currentSusScore = null;
            if (cmbSusScore.SelectedItem != null && cmbSusScore.SelectedItem.ToString() != "Any")
            {
                if (!int.TryParse(cmbSusScore.SelectedItem.ToString(), out int sus))
                {
                    MessageBox.Show("Invalid sustainability score selected.");
                    return;
                }
                currentSusScore = sus;
            }

            //collect accessibiltiites
            selectedAccessibilities.Clear();
            foreach (object item in clbAccessibility.CheckedItems)
            {
                selectedAccessibilities.Add(item.ToString());
            }

            //operator
            currentOperator = null;
            if (cmbOperator.SelectedItem != null && cmbOperator.SelectedItem.ToString() != "Any")
                currentOperator = cmbOperator.SelectedItem.ToString();

            //activities
            selectedActivities.Clear();
            foreach (object item in clbActivities.CheckedItems)
            {
                selectedActivities.Add(item.ToString());
            }


            //reload with valid filters
            currentPage = 0;
            LoadPackages(currentKeyword, currentMinBudget, currentMaxBudget, currentDuration, currentGroupSize, currentSusScore, currentTripType, selectedAccessibilities, currentOperator, selectedActivities);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadPackages(currentKeyword, currentMinBudget, currentMaxBudget, currentDuration, currentGroupSize, currentSusScore, currentTripType, selectedAccessibilities, currentOperator, selectedActivities);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
                currentPage--;
            LoadPackages(currentKeyword, currentMinBudget, currentMaxBudget, currentDuration, currentGroupSize, currentSusScore, currentTripType, selectedAccessibilities, currentOperator, selectedActivities);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            travelerLogin login = new travelerLogin();
            login.Show();
            this.Hide();
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCustomTrip_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerCustomTrip cs = new travelerCustomTrip(this.TravelerID);
            cs.Show();  
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerBookings booking = new travelerBookings(TravelerID);
            booking.OpenTab(0); //open at 0th (booking) tab
            booking.Show();
        }

        private void btnTravelHistory_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerBookings booking = new travelerBookings(TravelerID);
            booking.OpenTab(2); //third tab is travel history
            booking.Show();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerUpdateProfile profile = new travelerUpdateProfile(TravelerID);
            profile.Show();
        }

        private void PopulateTripTypeComboBox()
        {
            cmbTripType.Items.Clear();
            cmbTripType.Items.Add("Any"); //default option

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT TripType FROM Package WHERE TripType IS NOT NULL", con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbTripType.Items.Add(reader.GetString(0).Trim());
                    }
                }

                cmbTripType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip types: " + ex.Message);
            }
        }


        void PopulateOperatorComboBox() //populate operator with actual values
        {
            cmbOperator.Items.Clear();
            cmbOperator.Items.Add("Any"); // default

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CompanyName FROM Operator", con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbOperator.Items.Add(reader.GetString(0));
                        }
                    }
                }

                cmbOperator.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading operators: " + ex.Message);
            }
        }

        private void cmbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnReviews_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerReview review = new travelerReview(TravelerID);
            review.OpenTab(4); //open reviews directly
            review.Show();
        }

        private void cmbTripType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            this.Hide();
            travelerViewReports reports = new travelerViewReports();
            reports.TravelerID = this.TravelerID;
            reports.Show();
        }
    }
}
