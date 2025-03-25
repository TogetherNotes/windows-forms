using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace TogetherNotes.Models.Management
{
    public static class SpacesOrm
    {
        public static List<spaces> SelectAllSpaces()
        {
            try
            {
                return Orm.db.spaces.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return new List<spaces>();
        }
    }
}