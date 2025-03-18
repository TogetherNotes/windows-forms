using System;
using System.Windows.Input;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class LoginVM :Utils.ViewModelBase
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }
        public Action OnLoginSuccess { get; set; }

        public LoginVM()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object obj)
        {
            // Simulación de validación de credenciales
            if (Username == "admin" && Password == "1234")
            {
                OnLoginSuccess?.Invoke(); // Notificar que el login fue exitoso
            }
            else
            {
                System.Windows.MessageBox.Show("Usuari o credencials incorrectes", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
        }
    }
}