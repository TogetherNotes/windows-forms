using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TogetherNotes.Models.Management;
using TogetherNotes.Utils;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Lógica de interacción para ManageApp.xaml
    /// </summary>
    public partial class ManageApp : UserControl
    {
        private bool _isPasswordVisible = false;
        private ObservableCollection<User> users;
        private ICollectionView usersView;

        /// <summary>
        /// Colección de géneros disponibles.
        /// </summary>
        public ObservableCollection<GenreModel> Genres { get; set; }

        /// <summary>
        /// Constructor de la clase ManageApp.
        /// Inicializa la interfaz y carga los datos de usuarios y géneros.
        /// </summary>
        public ManageApp()
        {
            InitializeComponent();
            LoadUserData();
            Genres = new ObservableCollection<GenreModel>();
            DataContext = this;
            LoadGenres();
            SetupUserFilter();
        }

        /// <summary>
        /// Carga la lista de géneros desde la base de datos.
        /// </summary>
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

        /// <summary>
        /// Evento que actualiza el cuadro de texto de género cuando se cierra el desplegable.
        /// </summary>
        private void GenreBox_DropDownClosed(object sender, EventArgs e)
        {
            var selectedGenres = Genres.Where(g => g.IsSelected).Select(g => g.Name);
            GenreBox.Text = string.Join(", ", selectedGenres);
        }

        /// <summary>
        /// Carga los datos de los usuarios desde la base de datos.
        /// </summary>
        private void LoadUserData()
        {
            List<User> artistsFromDb = ArtistsOrm.SelectAllArtists();
            List<User> spacesFromDB = SpacesOrm.SelectAllSpaces();


            var allUsers = artistsFromDb
                .Concat(spacesFromDB)
                .ToList();

            users = new ObservableCollection<User>(allUsers);
            usersView = CollectionViewSource.GetDefaultView(users);
            usersDataGrid.ItemsSource = usersView;
        }

        /// <summary>
        /// Configura el filtro de búsqueda de usuarios.
        /// </summary>
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

        /// <summary>
        /// Filtra los usuarios en función del texto ingresado en el cuadro de búsqueda.
        /// </summary>
        private void searchedUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (usersView.DeferRefresh())
            {
                if (!usersView.Cast<User>().Any())
                {
                    searchedUser.Text = string.Empty;
                    MessageBox.Show("No users were found.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// Maneja el evento de cambio de selección en la tabla de usuarios.
        /// </summary>
        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                nameUser.Text = selectedUser.Fullname;
                Mail.Text = selectedUser.Mail;
                PasswordBox.Password = selectedUser.Password;
                PasswordTextBox.Text = selectedUser.Password;

                if (selectedUser.Role.Equals("Artist", StringComparison.OrdinalIgnoreCase))
                    roleComboBox.SelectedIndex = 0;
                else if (selectedUser.Role.Equals("Space", StringComparison.OrdinalIgnoreCase))
                    roleComboBox.SelectedIndex = 1;
                else
                    roleComboBox.SelectedIndex = -1;

                CapacityBlock.Visibility = Visibility.Collapsed;
                CapacityBox.Visibility = Visibility.Collapsed;
                genreBlock.Visibility = Visibility.Collapsed;
                GenreBox.Visibility = Visibility.Collapsed;

                if (selectedUser.Role.Equals("Artist", StringComparison.OrdinalIgnoreCase))
                {
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
                    CapacityBlock.Visibility = Visibility.Visible;
                    CapacityBox.Visibility = Visibility.Visible;

                    RatingBox.Text = selectedUser.Rating?.ToString() ?? string.Empty;
                    CapacityBox.Text = selectedUser.Capacity?.ToString() ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Alterna la visibilidad de la contraseña entre texto plano y oculto.
        /// </summary>
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

        /// <summary>
        /// Actualiza el cuadro de texto de contraseña cuando se modifica la contraseña.
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!_isPasswordVisible)
                PasswordTextBox.Text = PasswordBox.Password;
        }

        /// <summary>
        /// Guarda o actualiza la información del usuario seleccionado.
        /// </summary>
        private void SaveUser(object sender, RoutedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                UpdateUser(selectedUser);
            }
            else
            {
                if(App.role.Equals("mant"))
                {
                    MessageBox.Show("You do not have permissions to create users.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    InsertUser();
                }
            }
            usersView.Refresh();
            ClearForm();
        }

        /// <summary>
        /// Actualiza un usuario seleccionado en la base de datos.
        /// </summary>
        private void UpdateUser(User selectedUser)
        {
            string role = (roleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (role == "Artist")
            {
                int rating = int.TryParse(RatingBox.Text, out int r) ? r : 1;
                rating = Math.Max(1, Math.Min(rating, 5));

                List<string> selectedGenres = Genres.Where(g => g.IsSelected).Select(g => g.Name).ToList();

                bool updated = ArtistsOrm.UpdateArtist(selectedUser.Id, nameUser.Text, Mail.Text, PasswordTextBox.Text, selectedGenres, rating);
                ShowMessage(updated, "updated");
            }
            else if (role == "Space")
            {
                int capacity = int.TryParse(CapacityBox.Text, out int c) ? c : 1;
                if (capacity < 1)
                {
                    MessageBox.Show("The capacity must be greater than 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool updated = SpacesOrm.UpdateSpace(selectedUser.Id, nameUser.Text, Mail.Text, PasswordTextBox.Text, capacity);
                ShowMessage(updated, "updated");
            }
            LoadUserData();
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        private void InsertUser()
        {
            string name = nameUser.Text;
            string mail = Mail.Text;
            string password = PasswordTextBox.Text;
            string role = (roleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("All fields are required.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (role == "Artist")
            {
                int rating = int.TryParse(RatingBox.Text, out int r) ? r : 1;
                rating = Math.Max(1, Math.Min(rating, 5));

                List<string> selectedGenres = Genres.Where(g => g.IsSelected).Select(g => g.Name).ToList();

                bool inserted = ArtistsOrm.InsertArtist(name, mail, password, selectedGenres, rating);
                ShowMessage(inserted, "created");
            }
            else if (role == "Space")
            {
                int rating = int.TryParse(RatingBox.Text, out int r) ? r : 1;
                rating = Math.Max(1, Math.Min(rating, 5));
                int capacity = int.TryParse(CapacityBox.Text, out int c) ? c : 1;
                if (capacity < 1)
                {
                    MessageBox.Show("The capacity must be greater than 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool inserted = SpacesOrm.InsertSpace(name, mail, password, capacity, rating);
                ShowMessage(inserted, "created");
            }
            LoadUserData();
        }

        /// <summary>
        /// Elimina un usuario seleccionado de la base de datos.
        /// </summary>
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
            {
                if (App.role.Equals("admin"))
                {
                    MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete this user?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        bool deleted = false;

                        if (selectedUser.Role.Equals("Artist", StringComparison.OrdinalIgnoreCase))
                        {
                            deleted = ArtistsOrm.DeleteArtist(selectedUser.Id);
                        }
                        else if (selectedUser.Role.Equals("Space", StringComparison.OrdinalIgnoreCase))
                        {
                            deleted = SpacesOrm.DeleteSpace(selectedUser.Id);
                        }
                        else
                        {
                            deleted = AdminOrm.DeleteAdmin(selectedUser.Id);
                        }

                        if (deleted)
                        {
                            MessageBox.Show("User deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            users.Remove(selectedUser);
                            usersView.Refresh();
                            ClearForm();
                            LoadUserData();
                        }
                        else
                        {
                            MessageBox.Show("Error deleting user.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You do not have permissions to delete users.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Select a user to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Muestra un mensaje de éxito o error al realizar una acción.
        /// </summary>
        private void ShowMessage(bool success, string action)
        {
            if (success)
            {
                MessageBox.Show($"User {action} successfulyy.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Error to {action} user.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Limpia el formulario de usuario.
        /// </summary>
        private void ClearForm()
        {
            nameUser.Clear();
            Mail.Clear();
            PasswordBox.Clear();
            PasswordTextBox.Clear();
            CapacityBox.Clear();
            RatingBox.Clear();
            GenreBox.Text = string.Empty;
            roleComboBox.SelectedIndex = -1;
            usersDataGrid.SelectedItem = null;

            CapacityBlock.Visibility = Visibility.Collapsed;
            CapacityBox.Visibility = Visibility.Collapsed;
            genreBlock.Visibility = Visibility.Collapsed;
            GenreBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Maneja el evento de cambio de selección en el ComboBox de roles.
        /// </summary>
        private void roleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roleComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedRole = selectedItem.Content.ToString();

                // Ocultar todos por defecto
                CapacityBlock.Visibility = Visibility.Collapsed;
                CapacityBox.Visibility = Visibility.Collapsed;
                genreBlock.Visibility = Visibility.Collapsed;
                GenreBox.Visibility = Visibility.Collapsed;

                // Mostrar según el rol seleccionado
                if (selectedRole == "Artist")
                {
                    genreBlock.Visibility = Visibility.Visible;
                    GenreBox.Visibility = Visibility.Visible;
                }
                else if (selectedRole == "Space")
                {
                    CapacityBlock.Visibility = Visibility.Visible;
                    CapacityBox.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Maneja el evento de limpiar formulario.
        /// </summary>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        
    }
}
