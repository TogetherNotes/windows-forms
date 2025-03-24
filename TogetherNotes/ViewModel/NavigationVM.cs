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
        public ICommand UsersCommand { get; set; }
        public ICommand CalendarCommand { get; set; }
        public ICommand MapCommand { get; set; }
        public ICommand FaqCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        private void Home(object obj) { if (IsAuthenticated) CurrentView = new HomeVM(); }
        private void Users(object obj) 
        {
            if (IsAuthenticated)
            {
                CurrentView = new ManageAdminVM();
            }

        }
        private void Calendar(object obj) { if (IsAuthenticated) CurrentView = new CalendarVM(); }
        private void Map(object obj) { if (IsAuthenticated) CurrentView = new MapVM(); }
        private void Faq(object obj) { if (IsAuthenticated) CurrentView = new FaqsVM(); }
        private void Setting(object obj) { if (IsAuthenticated) CurrentView = new SettingVM(); }
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            UsersCommand = new RelayCommand(Users);
            CalendarCommand = new RelayCommand(Calendar);
            MapCommand = new RelayCommand(Map);
            FaqCommand = new RelayCommand(Faq);
            SettingsCommand = new RelayCommand(Setting);

            // Inicialitzar la vista amb Login
            IsAuthenticated = false;
            var loginVM = new LoginVM();
            loginVM.OnLoginSuccess = () =>
            {
                IsAuthenticated = true;
                CurrentView = new HomeVM(); // Dashboard després del login
            };

            CurrentView = loginVM;
        }
    }
}