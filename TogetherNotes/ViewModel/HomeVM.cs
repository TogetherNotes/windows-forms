using System.Collections.Generic;
using System.ComponentModel;
using LiveCharts;
using TogetherNotes.Models.Management;

namespace TogetherNotes.ViewModel
{
    class HomeVM : INotifyPropertyChanged
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

        // Propietats de la gràfica
        private ChartValues<int> _userRegistrations;
        public ChartValues<int> UserRegistrations
        {
            get { return _userRegistrations; }
            set { _userRegistrations = value; OnPropertyChanged(nameof(UserRegistrations)); }
        }

        private List<string> _months;
        public List<string> Months
        {
            get { return _months; }
            set { _months = value; OnPropertyChanged(nameof(Months)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HomeVM()
        {
            LoadChartData();

            TotalSuperAdmins = AdminOrm.SelectTotalOfAdmin(1);
            TotalAdmins = AdminOrm.SelectTotalOfAdmin(2);
            TotalMaintenanceUsers = AdminOrm.SelectTotalOfAdmin(3);
            TotalAppUsers = AppOrm.SelectTotalOfApp();
            TotalArtists = AppOrm.SelectTotalOfAppWithRole("Artist");
            TotalSpaces = AppOrm.SelectTotalOfAppWithRole("Space");
        }

        private void LoadChartData()
        {
            Months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            UserRegistrations = new ChartValues<int> { 10, 25, 30, 50, 40, 60, 90, 70, 85, 100, 120, 150 };
        }
    }
}