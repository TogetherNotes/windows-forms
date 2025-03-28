using System.Windows.Input;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    /// <summary>
    /// ViewModel que gestiona la navegación entre vistas y la autenticación de usuarios.
    /// </summary>
    class NavigationVM : ViewModelBase
    {
        private object _currentView;

        /// <summary>
        /// Vista actual que se muestra en la interfaz de usuario.
        /// </summary>
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        private bool _isAuthenticated;

        /// <summary>
        /// Indica si el usuario ha iniciado sesión correctamente.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Comando para navegar a la vista de inicio (Home).
        /// </summary>
        public ICommand HomeCommand { get; set; }

        /// <summary>
        /// Comando para navegar a la vista de usuarios.
        /// </summary>
        public ICommand UsersCommand { get; set; }

        /// <summary>
        /// Comando para navegar a la vista de calendario.
        /// </summary>
        public ICommand CalendarCommand { get; set; }

        /// <summary>
        /// Comando para navegar a la vista de mapa.
        /// </summary>
        public ICommand MapCommand { get; set; }

        /// <summary>
        /// Comando para navegar a la vista de preguntas frecuentes.
        /// </summary>
        public ICommand FaqCommand { get; set; }

        /// <summary>
        /// Comando para navegar a la vista de configuración.
        /// </summary>
        public ICommand SettingsCommand { get; set; }

        /// <summary>
        /// Navega a la vista de inicio (Home) si el usuario está autenticado.
        /// </summary>
        private void Home(object obj)
        {
            if (IsAuthenticated)
                CurrentView = new HomeVM();
        }

        /// <summary>
        /// Navega a la vista de gestión de usuarios según el rol del usuario (root, admin, mant) si está autenticado.
        /// </summary>
        private void Users(object obj)
        {
            if (!IsAuthenticated || string.IsNullOrEmpty(App.role))
            {
                return;
            }

            if (App.role.Equals("root"))
            {
                CurrentView = new ManageAdminVM();
            }
            else if (App.role.Equals("admin") || App.role.Equals("mant"))
            {
                CurrentView = new ManageAppVM();
            }
        }

        /// <summary>
        /// Navega a la vista del calendario si el usuario está autenticado.
        /// </summary>
        private void Calendar(object obj)
        {
            if (IsAuthenticated)
                CurrentView = new CalendarVM();
        }

        /// <summary>
        /// Navega a la vista del mapa si el usuario está autenticado.
        /// </summary>
        private void Map(object obj)
        {
            if (IsAuthenticated)
                CurrentView = new MapVM();
        }

        /// <summary>
        /// Navega a la vista de preguntas frecuentes si el usuario está autenticado.
        /// </summary>
        private void Faq(object obj)
        {
            if (IsAuthenticated)
                CurrentView = new FaqsVM();
        }

        /// <summary>
        /// Navega a la vista de configuración si el usuario está autenticado.
        /// </summary>
        private void Setting(object obj)
        {
            if (IsAuthenticated)
                CurrentView = new SettingVM();
        }

        /// <summary>
        /// Constructor del ViewModel de navegación. Inicializa los comandos y la vista de login.
        /// </summary>
        public NavigationVM()
        {
            // Inicializa los comandos de navegación
            HomeCommand = new RelayCommand(Home);
            UsersCommand = new RelayCommand(Users);
            CalendarCommand = new RelayCommand(Calendar);
            MapCommand = new RelayCommand(Map);
            FaqCommand = new RelayCommand(Faq);
            SettingsCommand = new RelayCommand(Setting);

            // Inicializa la vista con la pantalla de login
            IsAuthenticated = false;
            var loginVM = new LoginVM();
            loginVM.OnLoginSuccess = () =>
            {
                IsAuthenticated = true;
                CurrentView = new HomeVM(); // Carga el Dashboard después del login
            };

            // Establece la vista inicial como Login
            CurrentView = loginVM;
        }
    }
}
