using System;
using System.Windows;
using System.Windows.Input;
using TogetherNotes.Utils;
using TogetherNotes.Models.Management;

namespace TogetherNotes.ViewModel
{
    /// <summary>
    /// ViewModel para la vista de inicio de sesión (Login).
    /// Gestiona la validación de las credenciales del usuario y notifica el éxito del login.
    /// </summary>
    class LoginVM : Utils.ViewModelBase
    {
        // Propiedad para almacenar el nombre de usuario ingresado
        private string _username;

        /// <summary>
        /// Propiedad que representa el nombre de usuario ingresado.
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        // Propiedad para almacenar la contraseña ingresada
        private string _password;

        /// <summary>
        /// Propiedad que representa la contraseña ingresada.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        // Comando para ejecutar la acción de login
        public ICommand LoginCommand { get; set; }

        // Acción que se ejecuta cuando el login es exitoso
        public Action OnLoginSuccess { get; set; }

        /// <summary>
        /// Constructor del ViewModel. Inicializa el comando de login.
        /// </summary>
        public LoginVM()
        {
            // Inicializa el comando de login y lo vincula al método Login
            LoginCommand = new RelayCommand(Login);
        }

        /// <summary>
        /// Método que valida las credenciales del usuario.
        /// </summary>
        /// <param name="obj">Parametro que no se utiliza, pero es requerido para el comando.</param>
        private void Login(object obj)
        {
            // Llama al ORM de Admin para validar las credenciales
            int? adminRole = AdminOrm.ValidateUser(Username, Password);
            bool isValid = false;

            // Verifica el rol del usuario
            if (adminRole == 1)
            {
                App.role = "root";  // Rol de Super Administrador
                isValid = true;
            }
            else if (adminRole == 2)
            {
                App.role = "admin";  // Rol de Administrador
                isValid = true;
            }
            else if (adminRole == 3)
            {
                App.role = "mant";  // Rol de Mantenimiento
                isValid = true;
            }

            // Si las credenciales son válidas, invoca el evento de éxito
            if (isValid)
            {
                OnLoginSuccess?.Invoke(); // Notifica que el login fue exitoso
            }
            else
            {
                // Si las credenciales son incorrectas, muestra un mensaje de error
                MessageBox.Show("Usuari o contrasenya incorrectes", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
