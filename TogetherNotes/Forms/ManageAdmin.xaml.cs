using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using TogetherNotes.Models;
using TogetherNotes.Models.Management;
using TogetherNotes.Utils;

namespace TogetherNotes.Forms
{
    public partial class ManageAdmin : UserControl
    {
        private bool _isPasswordVisible = false;
        private ObservableCollection<User> users;
        private ICollectionView usersView;
        public ObservableCollection<GenreModel> Genres { get; set; }

        public ManageAdmin()
        {
            InitializeComponent();
            LoadUserData();
            Genres = new ObservableCollection<GenreModel>();
            DataContext = this;
            LoadGenres();
            SetupUserFilter();
        }

        private void LoadGenres()
        {
            List<string> genresFromDb = GenresOrm.SelectAllGenres();
            if (genresFromDb != null)
            {
                foreach (var genre in genresFromDb)
                {
                    Genres.Add(new GenreModel { Name = genre });
                }
            }
        }
        private void GenreBox_DropDownClosed(object sender, EventArgs e)
        {
            var selectedGenres = Genres.Where(g => g.IsSelected).Select(g => g.Name);
            GenreBox.Text = string.Join(", ", selectedGenres);
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
            List<User> adminsFromDb = AdminOrm.SelectAllAdmins();
            List<User> artistsFromDb = ArtistsOrm.SelectAllArtists();
            List<User> spacesFromDB = SpacesOrm.SelectAllSpaces();


            var allUsers = adminsFromDb
                .Concat(artistsFromDb)
                .Concat(spacesFromDB)
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


                    foreach (var genre in Genres)
                    {
                        genre.IsSelected = false;
                    }


                    foreach (var genre in selectedUser.Genre)
                    {
                        var genreToMark = Genres.FirstOrDefault(g => g.Name == genre);
                        if (genreToMark != null)
                        {
                            genreToMark.IsSelected = true;
                        }
                        var selectedGenres = Genres.Where(g => g.IsSelected).Select(g => g.Name);
                        GenreBox.Text = string.Join(", ", selectedGenres);
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
        /*
        private void SaveUser(object sender, RoutedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                UpdateUser(selectedUser);
            }
            else
            {
                InsertUser();
            }

            usersView.Refresh();
            ClearForm();
        }
        
        private void UpdateUser(User selectedUser)
        {
            string role = (roleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (role == "Artist")
            {
                int rating = int.TryParse(RatingBox.Text, out int r) ? r : 1;
                rating = Math.Max(1, Math.Min(rating, 5)); // Asegura que esté entre 1 y 5
                int genreId = GenreBox.SelectedIndex + 1;

                bool updated = ArtistsOrm.UpdateArtist(selectedUser.Id, nameUser.Text, Mail.Text, PasswordTextBox.Text, genreId, rating);
                ShowMessage(updated, "actualizado");
            }
            else if (role == "Space")
            {
                int capacity = int.TryParse(CapacityBox.Text, out int c) ? c : 1;
                if (capacity < 1)
                {
                    MessageBox.Show("La capacidad debe ser mayor a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool updated = SpacesOrm.UpdateSpace(selectedUser.Id, nameUser.Text, Mail.Text, PasswordTextBox.Text, capacity);
                ShowMessage(updated, "actualizado");
            }
            else
            {
                int roleId = GetRoleId(role);
                bool updated = AdminOrm.UpdateAdmin(selectedUser.Id, nameUser.Text, Mail.Text, PasswordTextBox.Text, roleId);
                ShowMessage(updated, "actualizado");
            }

            LoadUserData();
        }
        

        private void InsertUser()
        {
            string name = nameUser.Text;
            string mail = Mail.Text;
            string password = PasswordTextBox.Text;
            string role = (roleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (role == "Artist")
            {
                int rating = int.TryParse(RatingBox.Text, out int r) ? r : 1;
                rating = Math.Max(1, Math.Min(rating, 5)); // Asegura que esté entre 1 y 5
                int genreId = GenreBox.SelectedIndex + 1;

                bool inserted = ArtistsOrm.InsertArtist(name, mail, password, genreId, rating);
                ShowMessage(inserted, "creado");
            }
            else if (role == "Space")
            {
                int capacity = int.TryParse(CapacityBox.Text, out int c) ? c : 1;
                if (capacity < 1)
                {
                    MessageBox.Show("La capacidad debe ser mayor a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool inserted = SpacesOrm.InsertSpace(name, mail, password, capacity);
                ShowMessage(inserted, "creado");
            }
            else
            {
                int roleId = GetRoleId(role);
                bool inserted = AdminOrm.InsertAdmin(name, mail, password, roleId);
                ShowMessage(inserted, "creado");
            }

            LoadUserData();
        }
        */

        private int GetRoleId(string role)
        {
            switch (role.ToLower())
            {
                case "root": return 1;
                case "admin": return 2;
                case "mant": return 3;
                default: return 0;
            }
        }


        private void ShowMessage(bool success, string action)
        {
            if (success)
            {
                MessageBox.Show($"Usuario {action} correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Error al {action} usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }




        /*
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
                    bool deleted = false;

                    if (selectedUser.Role == "Artist")
                    {
                        deleted = ArtistsOrm.DeleteArtist(selectedUser.Id);
                    }
                    else if (selectedUser.Role == "Space")
                    {
                        deleted = SpacesOrm.DeleteSpace(selectedUser.Id);
                    }
                    else
                    {
                        deleted = AdminOrm.DeleteAdmin(selectedUser.Id);
                    }

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
        */
    



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

                RatingBlock.Visibility = Visibility.Collapsed;
                RatingBox.Visibility = Visibility.Collapsed;
                CapacityBlock.Visibility = Visibility.Collapsed;
                CapacityBox.Visibility = Visibility.Collapsed;
                genreBlock.Visibility = Visibility.Collapsed;
                GenreBox.Visibility = Visibility.Collapsed;

                if (selectedRole == "Artist")
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
