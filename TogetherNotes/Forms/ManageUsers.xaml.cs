using System;
using System.Windows;
using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Lógica de interacción para ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : UserControl
    {
        private bool _isPasswordVisible = false;

        public ManageUsers()
        {
            InitializeComponent();
        }

        public ManageUsers(UsersVM parentVM) : this()
        {
            this.DataContext = new ManageUsersVM(parentVM);
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
