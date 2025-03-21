using System;
using System.Data.SqlClient;
using System.Linq;

namespace TogetherNotes.Models.Management
{
    public static class AdminOrm
    {
        public static int SelectTotalOfAdmin(int rol)
        {
            try
            {
                return Orm.db.admin
                    .Where(admin => admin.role_id == rol)
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