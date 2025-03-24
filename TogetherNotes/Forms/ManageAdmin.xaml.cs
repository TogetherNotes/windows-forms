using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Data.SqlClient;
using TogetherNotes.Models;
using TogetherNotes.Models.Management;

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
            LoadUserData();  
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

        private void LoadUserData()
        {
            // Obtener los administradores y usuarios comunes
            List<admin> adminsFromDb = AdminOrm.SelectAllAdmins();
            List<app> usersFromDb = AppOrm.SelectAllUsers();

            // Convertir los administradores a la clase User
            var adminUsers = adminsFromDb.Select(a => new User
            {
                Id = a.id,
                Fullname = a.name,
                Mail = a.mail,
                Password = a.password,
                Role = a.roles.name,
            });

            // Convertir los usuarios comunes a la clase User
            var appUsers = usersFromDb.Select(u => new User
            {
                Id = u.id,
                Fullname = u.name,
                Mail = u.mail,
                Password = u.password,
                Role = u.role,
            });

            // Combinar las dos listas
            var allUsers = adminUsers.Concat(appUsers).ToList();

            // Cargar la lista combinada en la vista
            users = new ObservableCollection<User>(allUsers);

            // Configurar la vista para filtrar usuarios
            usersView = CollectionViewSource.GetDefaultView(users);
            usersDataGrid.ItemsSource = usersView;
        }

        private void searchedUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            usersView.Refresh();

            if (!usersView.Cast<User>().Any())
            {
                searchedUser.Text = string.Empty;
                MessageBox.Show("No se encontraron usuarios.", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Information);
                usersView.Refresh();
            }
        }

        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                nameUser.Text = selectedUser.Fullname;
                Mail.Text = selectedUser.Mail;
                PasswordBox.Password = selectedUser.Password;
                PasswordTextBox.Text = selectedUser.Password;
                if (selectedUser.Role.Equals("root"))
                {
                    roleComboBox.SelectedIndex = 0;
                }
                else if (selectedUser.Role.Equals("admin"))
                {
                    roleComboBox.SelectedIndex = 1;
                }
                else if (selectedUser.Role.Equals("mant"))
                {
                    roleComboBox.SelectedIndex = 2;
                }
                else if (selectedUser.Role.Equals("Artist"))
                {
                    roleComboBox.SelectedIndex = 3;
                }
                else
                {
                    roleComboBox.SelectedIndex = 4;
                }
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
            CapacityBox.Text = string.Empty;
            RatingBox.Text = string.Empty;
            GenreBox.Text = string.Empty;
            roleComboBox.SelectedIndex = -1;
            usersDataGrid.SelectedItem = null;

            RatingBlock.Visibility = Visibility.Collapsed;
            RatingBox.Visibility = Visibility.Collapsed;
            CapacityBlock.Visibility = Visibility.Collapsed;
            CapacityBox.Visibility = Visibility.Collapsed;
            genreBlock.Visibility = Visibility.Collapsed;
            GenreBox.Visibility = Visibility.Collapsed;
        }


        private void roleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roleComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedRole = selectedItem.Content.ToString();

                // Ocultar todos por defecto
                RatingBlock.Visibility = Visibility.Collapsed;
                RatingBox.Visibility = Visibility.Collapsed;
                CapacityBlock.Visibility = Visibility.Collapsed;
                CapacityBox.Visibility = Visibility.Collapsed;
                genreBlock.Visibility = Visibility.Collapsed;
                GenreBox.Visibility = Visibility.Collapsed;

                // Mostrar según el rol seleccionado
                if (selectedRole == "Art")
                {
                    RatingBlock.Visibility = Visibility.Visible;
                    RatingBox.Visibility = Visibility.Visible;
                    genreBlock.Visibility = Visibility.Visible;
                    GenreBox.Visibility = Visibility.Visible;
                }
                else if (selectedRole == "Space")
                {
                    RatingBlock.Visibility = Visibility.Visible;
                    RatingBox.Visibility = Visibility.Visible;
                    CapacityBlock.Visibility = Visibility.Visible;
                    CapacityBox.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }
    }
}
