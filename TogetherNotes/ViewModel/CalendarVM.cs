using System;
using System.Collections.ObjectModel;
using TogetherNotes.Models.Management;
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
                LoadEventsFromDatabase();
            }
        }

        public ObservableCollection<object> FilteredEvents { get; set; }

        public CalendarVM()
        {
            FilteredEvents = new ObservableCollection<object>();

            SelectedDate = DateTime.Now;
        }

        private void LoadEventsFromDatabase()
        {
            if (!SelectedDate.HasValue) return;

            var events = ContractsOrm.GetEventsByDate(SelectedDate.Value);

            FilteredEvents.Clear();
            foreach (var item in events)
            {
                FilteredEvents.Add(item);
            }
        }
    }
}