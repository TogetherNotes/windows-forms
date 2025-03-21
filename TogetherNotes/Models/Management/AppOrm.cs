using System.Data.SqlClient;
using System;
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
            try
            {
                return Orm.db.app.Count();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex)); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }

            return 0; 
        }

        public static int SelectTotalOfAppWithRole(string role)
        {
            try
            {
                return Orm.db.app
                    .Where(app => app.role == role)
                    .Count();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex)); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }

            return 0; 
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