﻿using System;
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

            // Carrega contractes d'avui
            var contracts = Orm.db.contracts
                .Where(c => c.init_hour >= today && c.init_hour < tomorrow)
                .ToList();

            // Carrega tots els usuaris (artistes i espais)
            var apps = Orm.db.app.ToList();

            var events = contracts.Select(c =>
            {
                var artist = apps.FirstOrDefault(a => a.id == c.artist_id);
                var space = apps.FirstOrDefault(a => a.id == c.space_id);

                string artistName = artist != null ? artist.name : "Unknown Artist";
                string spaceName = space != null ? space.name : "Unknown Space";
                string type = c.meet_type;

                string title = $"Event at {c.init_hour.LocalDateTime.ToShortTimeString()} with {artistName} and {spaceName} - {type}";

                return new Utils.Event(c.init_hour, title);

            }).ToList();

            return events;
        }
    }
}