using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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