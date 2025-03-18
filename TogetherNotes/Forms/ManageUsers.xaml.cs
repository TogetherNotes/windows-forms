using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Lógica de interacción para ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : UserControl
    {
        private bool _isPasswordVisible = false;

        // Clase User para los datos de prueba
        public class User
        {
            public int Id { get; set; }
            public string Fullname { get; set; }
            public string Mail { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public ManageUsers()
        {
            InitializeComponent();
            // Llamamos al método para cargar los usuarios de prueba en el DataGrid
            LoadTestData();
        }

        private void LoadTestData()
        {
            // Crear algunos datos de prueba
            List<User> users = new List<User>
            {
                new User { Id = 1, Fullname = "Juan Pérez", Mail = "juan.perez@email.com", Password = "12345", Role = "Administrador" },
                new User { Id = 2, Fullname = "Ana González", Mail = "ana.gonzalez@email.com", Password = "password123", Role = "Administrador" },
                new User { Id = 3, Fullname = "Carlos López", Mail = "carlos.lopez@email.com", Password = "qwerty", Role = "Administrador" },
                new User { Id = 4, Fullname = "María Fernández", Mail = "maria.fernandez@email.com", Password = "admin123", Role = "Administrador" },
                new User { Id = 5, Fullname = "Pedro García", Mail = "pedro.garcia@email.com", Password = "adminpass", Role = "Administrador" }
            };

            // Asignamos la lista de usuarios al DataGrid
            usersDataGrid.ItemsSource = users;
        }

        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;

            if (_isPasswordVisible)
            {
                PasswordTextBox.Text = PasswordBox.Password; // Sincroniza el texto
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordBox.Password = PasswordTextBox.Text; // Devuelve el texto al PasswordBox
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!_isPasswordVisible) // Solo actualiza si está oculto
                PasswordTextBox.Text = PasswordBox.Password;
        }
    }
}
