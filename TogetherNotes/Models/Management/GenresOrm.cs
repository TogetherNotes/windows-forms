using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace TogetherNotes.Models.Management
{
    public static class GenresOrm
    {
        public static List<string> SelectAllGenres()
        {
            try
            {
                return Orm.db.genres.Select(g => g.name).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return new List<string>();
        }
    }

}