using System.ComponentModel;
using TogetherNotes.Models;
using TogetherNotes.Models.Management;

namespace TogetherNotes.ViewModel
{
    class HomeVM
    {
        private int _totalSuperAdmins;
        private int _totalAdmins;
        private int _totalMaintenanceUsers;
        private int _totalAppUsers;
        private int _totalArtists;
        private int _totalSpaces;

        public int TotalSuperAdmins
        {
            get { return _totalSuperAdmins; }
            set { _totalSuperAdmins = value; OnPropertyChanged(nameof(TotalSuperAdmins)); }
        }

        public int TotalAdmins
        {
            get { return _totalAdmins; }
            set { _totalAdmins = value; OnPropertyChanged(nameof(TotalAdmins)); }
        }

        public int TotalMaintenanceUsers
        {
            get { return _totalMaintenanceUsers; }
            set { _totalMaintenanceUsers = value; OnPropertyChanged(nameof(TotalMaintenanceUsers)); }
        }

        public int TotalAppUsers
        {
            get { return _totalAppUsers; }
            set { _totalAppUsers = value; OnPropertyChanged(nameof(TotalAppUsers)); }
        }

        public int TotalArtists
        {
            get { return _totalArtists; }
            set { _totalArtists = value; OnPropertyChanged(nameof(TotalArtists)); }
        }

        public int TotalSpaces
        {
            get { return _totalSpaces; }
            set { _totalSpaces = value; OnPropertyChanged(nameof(TotalSpaces)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HomeVM()
        {
            // Simulación de datos (en una aplicación real, esto vendría de una base de datos o servicio)
            TotalSuperAdmins = AdminOrm.SelectTotalOfAdmin(1);
            TotalAdmins = AdminOrm.SelectTotalOfAdmin(2);
            TotalMaintenanceUsers = AdminOrm.SelectTotalOfAdmin(3);
            TotalAppUsers = 150;
            TotalArtists = 42;
            TotalSpaces = 29;
        }
    }
}