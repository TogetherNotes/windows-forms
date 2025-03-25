using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace TogetherNotes.Models.Management
{
    public static class ArtistsOrm
    {
        public static List<artists> SelectAllArtist()
        {
            try
            {
                return Orm.db.artists.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return new List<artists>();
        }
    }
}