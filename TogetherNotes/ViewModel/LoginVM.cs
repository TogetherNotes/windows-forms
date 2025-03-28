using System;
using System.Windows;
using System.Windows.Input;
using TogetherNotes.Utils;
using TogetherNotes.Models.Management;

namespace TogetherNotes.ViewModel
{
    class LoginVM : Utils.ViewModelBase
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
            // Validem amb la DB
            int? adminRole = AdminOrm.ValidateUser(Username, Password);
            bool isValid = false;
            if (adminRole == 1)
            {
                App.role = "root";
                isValid = true;
            }
            else if (adminRole == 2)
            {
                App.role = "admin";
                isValid = true;
            }
            else if (adminRole == 3)
            {
                App.role = "mant";
                isValid = true;
            }

            if (isValid)
            {

                OnLoginSuccess?.Invoke(); // Notificar que el login ha estat correcte
            }
            else
            {
                MessageBox.Show("Usuari o contrasenya incorrectes", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}