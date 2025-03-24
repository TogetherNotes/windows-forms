using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    public class CalendarVM : ViewModelBase
    {
        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
                FilterEvents();
            }
        }

        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<object> FilteredEvents { get; set; }

        public CalendarVM()
        {
            Events = new ObservableCollection<Event>
            {

            };
            FilteredEvents = new ObservableCollection<object>();
        }

        private void FilterEvents()
        {
            if (SelectedDate.HasValue)
            {
                var filtered = Events
                    .Where(ev => ev.Timestamp.Date == SelectedDate.Value.Date)  
                    .Select(ev => new
                    {
                        Time = ev.Timestamp.LocalDateTime.ToString("HH:mm"), 
                        ev.Title
                    })
                    .ToList();

                FilteredEvents.Clear();
                foreach (var item in filtered)
                {
                    FilteredEvents.Add(item);
                }
            }
        }

        private DateTime ConvertTimestampToDate(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
        }
    }
}