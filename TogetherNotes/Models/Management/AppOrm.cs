using System.Data.SqlClient;
using System;
using System.Linq;

namespace TogetherNotes.Models.Management
{
    public static class AppOrm
    {
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

    }
}