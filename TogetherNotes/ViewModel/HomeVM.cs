using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LiveCharts;
using TogetherNotes.Utils;
using TogetherNotes.Models.Management;

namespace TogetherNotes.ViewModel
{
    /// <summary>
    /// ViewModel para la vista de inicio (Home) de la aplicación.
    /// Gestiona la carga de datos relacionados con estadísticas de usuarios y eventos del día, además de proporcionar datos para la gráfica de registros de usuarios.
    /// </summary>
    class HomeVM : INotifyPropertyChanged
    {
        // Campos privados para las estadísticas de usuarios
        private int _totalSuperAdmins;
        private int _totalAdmins;
        private int _totalMaintenanceUsers;
        private int _totalAppUsers;
        private int _totalArtists;
        private int _totalSpaces;

        /// <summary>
        /// Propiedad que representa el total de Super Administradores.
        /// </summary>
        public int TotalSuperAdmins
        {
            get { return _totalSuperAdmins; }
            set { _totalSuperAdmins = value; OnPropertyChanged(nameof(TotalSuperAdmins)); }
        }

        /// <summary>
        /// Propiedad que representa el total de Administradores.
        /// </summary>
        public int TotalAdmins
        {
            get { return _totalAdmins; }
            set { _totalAdmins = value; OnPropertyChanged(nameof(TotalAdmins)); }
        }

        /// <summary>
        /// Propiedad que representa el total de usuarios de mantenimiento.
        /// </summary>
        public int TotalMaintenanceUsers
        {
            get { return _totalMaintenanceUsers; }
            set { _totalMaintenanceUsers = value; OnPropertyChanged(nameof(TotalMaintenanceUsers)); }
        }

        /// <summary>
        /// Propiedad que representa el total de usuarios de la aplicación.
        /// </summary>
        public int TotalAppUsers
        {
            get { return _totalAppUsers; }
            set { _totalAppUsers = value; OnPropertyChanged(nameof(TotalAppUsers)); }
        }

        /// <summary>
        /// Propiedad que representa el total de artistas registrados.
        /// </summary>
        public int TotalArtists
        {
            get { return _totalArtists; }
            set { _totalArtists = value; OnPropertyChanged(nameof(TotalArtists)); }
        }

        /// <summary>
        /// Propiedad que representa el total de espacios registrados.
        /// </summary>
        public int TotalSpaces
        {
            get { return _totalSpaces; }
            set { _totalSpaces = value; OnPropertyChanged(nameof(TotalSpaces)); }
        }

        // Propiedades para la gráfica de registros de usuarios
        private ChartValues<int> _userRegistrations;

        /// <summary>
        /// Propiedad que almacena los valores de registros de usuarios para la gráfica.
        /// </summary>
        public ChartValues<int> UserRegistrations
        {
            get { return _userRegistrations; }
            set { _userRegistrations = value; OnPropertyChanged(nameof(UserRegistrations)); }
        }

        private List<string> _months;

        /// <summary>
        /// Propiedad que almacena los nombres de los meses para la gráfica.
        /// </summary>
        public List<string> Months
        {
            get { return _months; }
            set { _months = value; OnPropertyChanged(nameof(Months)); }
        }

        /// <summary>
        /// Evento que notifica cuando una propiedad ha cambiado.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Método para notificar el cambio de una propiedad.
        /// </summary>
        /// <param name="propertyName">El nombre de la propiedad que ha cambiado.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Colección observable que contiene los eventos que ocurrirán hoy.
        /// </summary>
        public ObservableCollection<Event> EventsToday { get; set; }

        /// <summary>
        /// Constructor de la clase HomeVM.
        /// Inicializa las propiedades de estadísticas de usuarios y carga la información de los eventos de hoy.
        /// </summary>
        public HomeVM()
        {
            LoadChartData();
            LoadEventsForToday();

            // Carga los totales de usuarios desde la base de datos utilizando los métodos de los ORM correspondientes.
            TotalSuperAdmins = AdminOrm.SelectTotalOfAdmin(1);
            TotalAdmins = AdminOrm.SelectTotalOfAdmin(2);
            TotalMaintenanceUsers = AdminOrm.SelectTotalOfAdmin(3);
            TotalAppUsers = AppOrm.SelectTotalOfApp();
            TotalArtists = AppOrm.SelectTotalOfAppWithRole("Artist");
            TotalSpaces = AppOrm.SelectTotalOfAppWithRole("Space");
        }

        /// <summary>
        /// Carga los datos para la gráfica de registros de usuarios (por mes).
        /// </summary>
        private void LoadChartData()
        {
            Months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            // Los datos de la gráfica de registros de usuarios para cada mes.
            UserRegistrations = new ChartValues<int> { 10, 25, 30, 50, 40, 60, 90, 70, 85, 100, 120, 150 };
        }

        /// <summary>
        /// Carga los eventos que ocurrirán hoy desde la base de datos.
        /// </summary>
        private void LoadEventsForToday()
        {
            // Obtiene los eventos del día actual desde el ORM de contratos.
            var events = ContractsOrm.GetEventsForToday();
            // Asigna los eventos a la colección observable EventsToday.
            EventsToday = new ObservableCollection<Event>(events);
        }
    }
}
