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
    public partial class travelerHome : Form
    {
        public int TravelerID { get; set; }

        public travelerHome()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = LAPTOP9158\\SQLEXPRESS;Initial Catalog = 'Project Data'; Integrated Security = True; Encrypt=False");

        //private void LoadPackages()
        //{
        //    flowLayoutPackages.Controls.Clear();

        //    string query = @"
        //    SELECT 
        //        p.*,
        //        sp_accom.ProviderName AS AccommodationProvider,
        //        sp_transport.ProviderName AS TransportProvider,
        //        l.CityName,
        //        l.CountryName,
        //        STUFF((
        //            SELECT ', ' + a.ActivityName
        //            FROM PackageActivities pa
        //            INNER JOIN Activity a ON pa.ActivityID = a.ActivityID
        //            WHERE pa.PackageID = p.PackageID
        //            FOR XML PATH('')
        //        ), 1, 2, '') AS Activities
        //    FROM Package p
        //    INNER JOIN Accommodation ac ON p.AccommodationID = ac.AccomodationID
        //    INNER JOIN Hotel h ON ac.HotelID = h.HotelID
        //    INNER JOIN ServiceProvider sp_accom ON h.ProviderID = sp_accom.ProviderId
        //    INNER JOIN Ride r on r.RideID = p.RideID
        //    INNER JOIN TransportService ts ON r.RideID = ts.TransportID
        //    INNER JOIN ServiceProvider sp_transport ON ts.ProviderID = sp_transport.ProviderId
        //    INNER JOIN Destination d ON p.DestinationID = d.DestinationID
        //    INNER JOIN Location l ON d.LocationID = l.LocationID
        //    ORDER BY p.PackageID
        //    OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        PackageCard tripCard = new PackageCard();
        //        tripCard.SetData(
        //            row["Title"].ToString(),
        //            row["Description"].ToString(),
        //            row["CityName"].ToString(),
        //            row["CountryName"].ToString(),
        //            row["AccommodationProvider"].ToString(),
        //            row["TransportProvider"].ToString(),
        //            row["Activities"].ToString(),
        //            row["GroupSize"].ToString(),
        //            row["TripType"].ToString(),
        //            row["SustainabilityScore"].ToString(),
        //            row["Duration"].ToString(),
        //            row["BasePrice"].ToString()
        //        );

        //        flowLayoutPackages.Controls.Add(tripCard);
        //    }
        //}

        private void LoadPackages()
        {
            flowLayoutPackages.Controls.Clear();

            // Main query to get package information
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
            OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

            Dictionary<int, List<string>> packageActivities = new Dictionary<int, List<string>>();

            // First get all activities for all packages we're loading
            string activitiesQuery = @"
        SELECT p.PackageID, a.ActivityName
        FROM Package p
        INNER JOIN PackageActivities pa ON pa.PackageID = p.PackageID
        INNER JOIN Activity a ON a.ActivityID = pa.ActivityID
        WHERE p.PackageID IN (
            SELECT TOP 10 PackageID FROM Package ORDER BY PackageID
        )";

            using (SqlCommand cmd = new SqlCommand(activitiesQuery, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int packageId = reader.GetInt32(0);
                        string activityName = reader.GetString(1);

                        if (!packageActivities.ContainsKey(packageId))
                        {
                            packageActivities[packageId] = new List<string>();
                        }
                        packageActivities[packageId].Add(activityName);
                    }
                }
            }

            // Now get the packages
            using (SqlCommand cmd = new SqlCommand(packageQuery, con))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    int packageId = Convert.ToInt32(row["PackageID"]); //get package id!
                    string activities = packageActivities.ContainsKey(packageId)
                        ? string.Join(", ", packageActivities[packageId])
                        : "No activities";

                    PackageCard tripCard = new PackageCard(this.TravelerID, packageId);

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
            LoadPackages();
        }


    }
}
