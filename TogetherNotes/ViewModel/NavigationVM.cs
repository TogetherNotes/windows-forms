using System;
using System.Windows.Input;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand TransactionsCommand { get; set; }
        public ICommand ShipmentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private void Home(object obj) { if (IsAuthenticated) CurrentView = new HomeVM(); }
        private void Customer(object obj) { if (IsAuthenticated) CurrentView = new CustomerVM(); }
        private void Product(object obj) { if (IsAuthenticated) CurrentView = new ProductVM(); }
        private void Order(object obj) { if (IsAuthenticated) CurrentView = new OrderVM(); }
        private void Transaction(object obj) { if (IsAuthenticated) CurrentView = new TransactionVM(); }
        private void Shipment(object obj) { if (IsAuthenticated) CurrentView = new ShipmentVM(); }
        private void Setting(object obj) { if (IsAuthenticated) CurrentView = new SettingVM(); }

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Customer);
            ProductsCommand = new RelayCommand(Product);
            OrdersCommand = new RelayCommand(Order);
            TransactionsCommand = new RelayCommand(Transaction);
            ShipmentsCommand = new RelayCommand(Shipment);
            SettingsCommand = new RelayCommand(Setting);

            // Inicializar vista de login
            IsAuthenticated = false;
            var loginVM = new LoginVM();
            loginVM.OnLoginSuccess = () =>
            {
                IsAuthenticated = true;
                CurrentView = new HomeVM();
            };

            CurrentView = loginVM;
        }
    }
}
