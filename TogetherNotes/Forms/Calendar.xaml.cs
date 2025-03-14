using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Lógica de interacción para Products.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        public List<Event> Events { get; set; }

        public Calendar()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents()
        {
            // Simulación de eventos con timestamps
            Events = new List<Event>
            {
                new Event { Timestamp = 1668594900, Title = "Post en redes sociales" },
                new Event { Timestamp = 1668599400, Title = "Revisar diseño de la app" },
                new Event { Timestamp = 1668607200, Title = "Construir demo de la app" },
                new Event { Timestamp = 1668629700, Title = "Cena con John Doe" },
                new Event { Timestamp = 1668639600, Title = "Enviar correo a Mohammad" }
            };
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainCalendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = MainCalendar.SelectedDate.Value;

                // Filtrar eventos según la fecha seleccionada
                var filteredEvents = Events
                    .Where(ev => ConvertTimestampToDate(ev.Timestamp).Date == selectedDate.Date)
                    .Select(ev => new { Time = ConvertTimestampToDate(ev.Timestamp).ToString("HH:mm"), ev.Title })
                    .ToList();

                EventsList.ItemsSource = filteredEvents;
            }
        }

        private DateTime ConvertTimestampToDate(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
        }
    }
    public class Event
    {
        public long Timestamp { get; set; }
        public string Title { get; set; }
    }
}
