using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace TogetherNotes.Forms
{
    public partial class ManageAdmin : UserControl
    {
        private bool _isPasswordVisible = false;
        private ObservableCollection<User> users;
        private ICollectionView usersView;

        public class User
        {
            public int Id { get; set; }
            public string Fullname { get; set; }
            public string Mail { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public ManageAdmin()
        {
            InitializeComponent();
            LoadTestData();
            SetupUserFilter();
        }

        private void SetupUserFilter()
        {
            if (usersView != null)
            {
                usersView.Filter = item =>
                {
                    if (item is User user)
                    {
                        return string.IsNullOrEmpty(searchedUser.Text) ||
                               user.Fullname.ToLower().Contains(searchedUser.Text.ToLower());
                    }
                    return false;
                };
            }
        }

        private void LoadTestData()
        {
            users = new ObservableCollection<User>
            {
                new User { Id = 1, Fullname = "Juan Pérez", Mail = "juan.perez@email.com", Password = "12345", Role = "Admin" },
                new User { Id = 2, Fullname = "Ana González", Mail = "ana.gonzalez@email.com", Password = "password123", Role = "Gestor" },
                new User { Id = 3, Fullname = "Carlos López", Mail = "carlos.lopez@email.com", Password = "qwerty", Role = "Mantenimiento" },
                new User { Id = 4, Fullname = "María Fernández", Mail = "maria.fernandez@email.com", Password = "admin123", Role = "Admin" },
                new User { Id = 5, Fullname = "Pedro García", Mail = "pedro.garcia@email.com", Password = "adminpass", Role = "Gestor" },
                new User { Id = 6, Fullname = "Juan Pérez", Mail = "juan.perez@email.com", Password = "12345", Role = "Admin" },
                new User { Id = 7, Fullname = "Ana González", Mail = "ana.gonzalez@email.com", Password = "password123", Role = "Gestor" },
                new User { Id = 8, Fullname = "Carlos López", Mail = "carlos.lopez@email.com", Password = "qwerty", Role = "Mantenimiento" },
                new User { Id = 9, Fullname = "María Fernández", Mail = "maria.fernandez@email.com", Password = "admin123", Role = "Admin" },
                new User { Id = 10, Fullname = "Pedro García", Mail = "pedro.garcia@email.com", Password = "adminpass", Role = "Gestor" }
            };

            usersView = CollectionViewSource.GetDefaultView(users);
            usersDataGrid.ItemsSource = usersView;
        }

        private void searchedUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            usersView.Refresh();
        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                selectedUser.Fullname = nameUser.Text;
                selectedUser.Mail = Mail.Text;
                selectedUser.Password = PasswordTextBox.Text;
                selectedUser.Role = (roleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            }
            else
            {
                User newUser = new User
                {
                    Id = users.Count + 1, 
                    Fullname = nameUser.Text,
                    Mail = Mail.Text,
                    Password = PasswordBox.Password,
                    Role = (roleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                };
                users.Add(newUser);
            }
            usersView.Refresh();
            ClearForm();
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                users.Remove(selectedUser);
                usersView.Refresh();
                ClearForm();
            }
        }

        private void ClearForm()
        {
            nameUser.Text = string.Empty;
            Mail.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            PasswordTextBox.Text = string.Empty;
            roleComboBox.SelectedIndex = -1;
            usersDataGrid.SelectedItem = null;
        }

    }
}
