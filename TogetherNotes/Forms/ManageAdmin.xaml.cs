using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using TogetherNotes.Models.Management;
using TogetherNotes.Utils;

namespace TogetherNotes.Forms
{
    public partial class ManageAdmin : UserControl
    {
        private bool _isPasswordVisible = false;
        private ObservableCollection<User> users;
        private ICollectionView usersView;
        /// <summary>
        /// Lista de géneros para los artistas
        /// </summary>
        public ObservableCollection<GenreModel> Genres { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ManageAdmin()
        {
            InitializeComponent();
            LoadUserData();
            Genres = new ObservableCollection<GenreModel>();
            DataContext = this;
            LoadGenres();
            SetupUserFilter();
        }

        /// <summary>
        /// Cargar los géneros de la base de datos
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
        /// Configurar el filtro de usuarios
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
        /// Cargar los datos de los usuarios
        /// </summary>
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

        /// <summary>
        /// Filtra los usuarios en función del texto ingresado en el cuadro de búsqueda.
        /// </summary>
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
                InsertUser();
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
            else
            {
                int roleId = GetRoleId(role);
                bool updated = AdminOrm.UpdateAdmin(selectedUser.Id, nameUser.Text, Mail.Text, PasswordTextBox.Text, roleId);
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
            else
            {
                int roleId = GetRoleId(role);
                bool inserted = AdminOrm.InsertAdmin(name, mail, password, roleId);
                ShowMessage(inserted, "created");
            }

            LoadUserData();
        }


        /// <summary>
        /// Obtiene el ID de un rol en función de su nombre.
        /// </summary>
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

        /// <summary>
        /// Muestra un mensaje de éxito o error al realizar una acción.
        /// </summary>
        private void ShowMessage(bool success, string action)
        {
            if (success)
            {
                MessageBox.Show($"User {action} successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Error ro {action} user.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Elimina un usuario seleccionado de la base de datos.
        /// </summary>
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (usersDataGrid.SelectedItem is User selectedUser)
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
                        ShowMessage(deleted, "deleted");
                        users.Remove(selectedUser);
                        usersView.Refresh();
                        ClearForm();
                        LoadUserData();
                    }
                    else
                    {
                        ShowMessage(deleted, "deleteing");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a user to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            RatingBlock.Visibility = Visibility.Collapsed;
            RatingBox.Visibility = Visibility.Collapsed;
            CapacityBlock.Visibility = Visibility.Collapsed;
            CapacityBox.Visibility = Visibility.Collapsed;
            genreBlock.Visibility = Visibility.Collapsed;
            GenreBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Maneja el evento de cambio de selección en el cuadro de roles.
        /// </summary>
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

        /// <summary>
        /// Maneja el evento de clic en el botón de limpiar.
        /// </summary>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }
    }
}
