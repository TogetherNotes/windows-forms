using System.Collections.Generic;
using System.Linq;

namespace TogetherNotes.Models.Management
{
    public static class AppOrm
    {
        public class Location
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public static int SelectTotalOfApp()
        {
            int totalApp = Orm.db.app
                    .Count();

            return totalApp;
        }

        public static int SelectTotalOfAppWithRole(string role)
        {
            int totalApp = Orm.db.app
                    .Where(app => app.role == role)
                    .Count();

            return totalApp;
        }

        public static List<Location> SelectAllLocations()
        {
            return Orm.db.app
                .Where(app => app.latitude != null && app.longitude != null)
                .Select(app => new Location
                {
                    Latitude = (double)app.latitude,
                    Longitude = (double)app.longitude
                })
                .ToList();
        }
    }
}