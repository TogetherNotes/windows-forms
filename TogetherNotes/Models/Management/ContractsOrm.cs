using System.Collections.Generic;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace TogetherNotes.Models.Management
{
    public static class ContractsOrm
    {
        public static List<object> GetEventsByDate(DateTime selectedDate)
        {
            try
            {
                return Orm.db.contracts
                    .Where(e => e.init_hour.Date == selectedDate.Date)
                    .Select(e => new
                    {
                        Time = e.init_hour.ToString("HH:mm"),
                        Title = e.meet_type
                    })
                    .ToList<object>(); 
            }
            catch (SqlException ex)
            {
                Console.WriteLine(Orm.ErrorMessage(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
            return new List<object>();
        }
    }
}