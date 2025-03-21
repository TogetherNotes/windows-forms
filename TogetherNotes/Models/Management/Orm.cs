using System;
using System.Data.SqlClient;

namespace TogetherNotes.Models
{
    class Orm
    {
        public static tgtnotesEntities db = new tgtnotesEntities();

        internal static bool ErrorMessage(SqlException ex)
        {
            throw new NotImplementedException();
        }
    }
}