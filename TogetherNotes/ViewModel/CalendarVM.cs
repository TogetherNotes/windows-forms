using System;
using System.Collections.ObjectModel;
using TogetherNotes.Models.Management;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    /// <summary>
    /// ViewModel que gestiona la lógica de la vista de calendario.
    /// Maneja la fecha seleccionada y los eventos filtrados por fecha.
    /// </summary>
    public class CalendarVM : ViewModelBase
    {
        private DateTime? _selectedDate;

        /// <summary>
        /// Propiedad que almacena la fecha seleccionada en el calendario.
        /// Al cambiar la fecha, se recargan los eventos relacionados con dicha fecha.
        /// </summary>
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(); // Notifica a la vista sobre el cambio en la propiedad.
                LoadEventsFromDatabase(); // Carga los eventos de la base de datos basados en la nueva fecha seleccionada.
            }
        }

        /// <summary>
        /// Colección observable de eventos filtrados por la fecha seleccionada.
        /// </summary>
        public ObservableCollection<object> FilteredEvents { get; set; }

        /// <summary>
        /// Constructor de la clase CalendarVM.
        /// Inicializa la colección de eventos y asigna la fecha actual como fecha seleccionada.
        /// </summary>
        public CalendarVM()
        {
            FilteredEvents = new ObservableCollection<object>(); // Inicializa la colección de eventos.
            SelectedDate = DateTime.Now; // Establece la fecha actual como la fecha seleccionada por defecto.
        }

        /// <summary>
        /// Carga los eventos desde la base de datos para la fecha seleccionada.
        /// </summary>
        private void LoadEventsFromDatabase()
        {
            // Si no hay una fecha seleccionada, no se cargan eventos.
            if (!SelectedDate.HasValue) return;

            // Obtiene los eventos de la base de datos para la fecha seleccionada.
            var events = ContractsOrm.GetEventsByDate(SelectedDate.Value);

            FilteredEvents.Clear(); // Limpiar la colección actual de eventos.

            // Agrega los eventos obtenidos a la colección observable.
            foreach (var item in events)
            {
                FilteredEvents.Add(item);
            }
        }
    }
}
