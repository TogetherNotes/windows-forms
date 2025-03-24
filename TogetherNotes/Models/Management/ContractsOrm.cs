using System;
using System.Collections.Generic;
using System.Linq;

namespace TogetherNotes.Models.Management
{
    public static class ContractOrm
    {
        public static List<Utils.Event> GetEventsForToday()
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            // Primer: carrega els valors necessaris a memòria
            var contracts = Orm.db.contracts
                .Where(c => c.init_hour >= today && c.init_hour < tomorrow)
                .Select(c => new
                {
                    InitHour = c.init_hour
                })
                .ToList(); // Això fa que les dades es carreguin a memòria

            // Després: transforma a la classe Utils.Event
            var events = contracts
                .Select(c => new Utils.Event(
                    c.InitHour.ToUnixTimeSeconds(),
                    "Event at " + c.InitHour.LocalDateTime.ToShortTimeString()
                ))
                .ToList();

            return events;
        }
    }
}