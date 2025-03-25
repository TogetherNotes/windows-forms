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
            // Propiedades opcionales para artistas y espacios
            public int? Rating { get; set; }
            public string Genre { get; set; }
            public int? Capacity { get; set; }
        }

        public ManageAdmin()
        {
            InitializeComponent();
            LoadUserData();
            LoadGenres();
            SetupUserFilter();
        }

        private void LoadGenres()
        {
            List<string> genresFromDb = GenresOrm.SelectAllGenres();
            if (genresFromDb != null)
            {
                GenreBox.ItemsSource = genresFromDb;
            }
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
            List<admin> adminsFromDb = AdminOrm.SelectAllAdmins();
            List<artists> artistsFromDb = ArtistsOrm.SelectAllArtist();
            List<spaces> spacesFromDB = SpacesOrm.SelectAllSpaces();

            // Convertir los administradores a User
            var adminUsers = adminsFromDb.Select(a => new User
            {
                Id = a.id,
                Fullname = a.name,
                Mail = a.mail,
                Password = a.password,
                Role = a.roles.name
            });

            // Convertir los artistas a User (incluye Rating y Genre)
            var artistUsers = artistsFromDb.Select(a => new User
            {
                Id = a.app_user_id,
                Fullname = a.app.name,
                Mail = a.app.mail,
                Password = a.app.password,
                Role = a.app.role,
                Rating = (int?)a.app.rating,
                Genre = a.genres.name
            });

            // Convertir los espacios a User (incluye Rating y Capacity)
            var spaceUsers = spacesFromDB.Select(a => new User
            {
                Id = a.app_user_id,
                Fullname = a.app.name,
                Mail = a.app.mail,
                Password = a.app.password,
                Role = a.app.role,
                Rating = (int?)a.app.rating,
                Capacity = (int?)a.capacity
            });

            var allUsers = adminUsers
                .Concat(artistUsers)
                .Concat(spaceUsers)
                .ToList();

            users = new ObservableCollection<User>(allUsers);
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

                if (selectedUser.Role.Equals("root", StringComparison.OrdinalIgnoreCase))
                    roleComboBox.SelectedIndex = 0;
                else if (selectedUser.Role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    roleComboBox.SelectedIndex = 1;
                else if (selectedUser.Role.Equals("mant", StringComparison.OrdinalIgnoreCase))
                    roleComboBox.SelectedIndex = 2;
                else if (selectedUser.Role.Equals("Artist", StringComparison.OrdinalIgnoreCase))
                    roleComboBox.SelectedIndex = 3;
                else if (selectedUser.Role.Equals("Space", StringComparison.OrdinalIgnoreCase))
                    roleComboBox.SelectedIndex = 4;
                else
                    roleComboBox.SelectedIndex = -1;

                RatingBlock.Visibility = Visibility.Collapsed;
                RatingBox.Visibility = Visibility.Collapsed;
                CapacityBlock.Visibility = Visibility.Collapsed;
                CapacityBox.Visibility = Visibility.Collapsed;
                genreBlock.Visibility = Visibility.Collapsed;
                GenreBox.Visibility = Visibility.Collapsed;

 
                if (selectedUser.Role.Equals("Artist", StringComparison.OrdinalIgnoreCase))
                {
                    RatingBlock.Visibility = Visibility.Visible;
                    RatingBox.Visibility = Visibility.Visible;
                    genreBlock.Visibility = Visibility.Visible;
                    GenreBox.Visibility = Visibility.Visible;

                    RatingBox.Text = selectedUser.Rating?.ToString() ?? string.Empty;

                    if (!string.IsNullOrEmpty(selectedUser.Genre) && GenreBox.Items.Contains(selectedUser.Genre))
                    {
                        GenreBox.SelectedItem = selectedUser.Genre;
                    }
                }
                else if (selectedUser.Role.Equals("Space", StringComparison.OrdinalIgnoreCase))
                {
                    RatingBlock.Visibility = Visibility.Visible;
                    RatingBox.Visibility = Visibility.Visible;
                    CapacityBlock.Visibility = Visibility.Visible;
                    CapacityBox.Visibility = Visibility.Visible;

                    RatingBox.Text = selectedUser.Rating?.ToString() ?? string.Empty;
                    CapacityBox.Text = selectedUser.Capacity?.ToString() ?? string.Empty;
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
                // Si hay un usuario seleccionado, actualizamos
                selectedUser.Fullname = nameUser.Text;
                selectedUser.Mail = Mail.Text;
                selectedUser.Password = PasswordTextBox.Text;
                selectedUser.Role = (roleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                int roleId = 0;
                switch (selectedUser.Role.ToLower())
                {
                    case "root": roleId = 1; break;
                    case "admin": roleId = 2; break;
                    case "mant": roleId = 3; break;
                }

                bool updated = AdminOrm.UpdateAdmin(selectedUser.Id, selectedUser.Fullname, selectedUser.Mail, selectedUser.Password, roleId);
                if (updated)
                {
                    MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUserData();
                }
                else
                {
                    MessageBox.Show("Error al actualizar usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Si NO hay usuario seleccionado, creamos uno nuevo
                string name = nameUser.Text;
                string mail = Mail.Text;
                string password = PasswordTextBox.Text;
                string role = (roleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int roleId = 0;
                switch (role.ToLower())
                {
                    case "root": roleId = 1; break;
                    case "admin": roleId = 2; break;
                    case "mant": roleId = 3; break;
                }

                bool inserted = AdminOrm.InsertAdmin(name, mail, password, roleId);
                if (inserted)
                {
                    MessageBox.Show("Usuario creado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUserData(); // Recargar la lista de usuarios
                }
                else
                {
                    MessageBox.Show("Error al crear usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            ClearForm();
        }




        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                MessageBoxResult result = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este usuario?",
                    "Confirmación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    bool deleted = AdminOrm.DeleteAdmin(selectedUser.Id);
                    if (deleted)
                    {
                        MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        users.Remove(selectedUser);
                        usersView.Refresh();
                        ClearForm();
                        LoadUserData();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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
