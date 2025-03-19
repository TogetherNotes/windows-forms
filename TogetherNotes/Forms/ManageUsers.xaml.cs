using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace TogetherNotes.Forms
{
    public partial class ManageUsers : UserControl
    {
        private bool _isPasswordVisible = false;
        private ObservableCollection<User> users;

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
            LoadTestData();
        }

        private void LoadTestData()
        {
            users = new ObservableCollection<User>
            {
                new User { Id = 1, Fullname = "Juan Pérez", Mail = "juan.perez@email.com", Password = "12345", Role = "Admin" },
                new User { Id = 2, Fullname = "Ana González", Mail = "ana.gonzalez@email.com", Password = "password123", Role = "Gestor" },
                new User { Id = 3, Fullname = "Carlos López", Mail = "carlos.lopez@email.com", Password = "qwerty", Role = "Mantenimiento" },
                new User { Id = 4, Fullname = "María Fernández", Mail = "maria.fernandez@email.com", Password = "admin123", Role = "Admin" },
                new User { Id = 5, Fullname = "Pedro García", Mail = "pedro.garcia@email.com", Password = "adminpass", Role = "Gestor" }
            };

            usersDataGrid.ItemsSource = users;
        }

        private void searchedUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchedUser.Text.ToLower();
            usersDataGrid.ItemsSource = users.Where(u => u.Fullname.ToLower().Contains(searchText)).ToList();
        }

        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                nameUser.Text = selectedUser.Fullname;
                Mail.Text = selectedUser.Mail;
                PasswordBox.Password = selectedUser.Password;
                PasswordTextBox.Text = selectedUser.Password;
                roleComboBox.SelectedItem = roleComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == selectedUser.Role);
            }
        }

        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;

            if (_isPasswordVisible)
            {
                PasswordTextBox.Text = PasswordBox.Password;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordBox.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!_isPasswordVisible)
                PasswordTextBox.Text = PasswordBox.Password;
        }
    }
}
