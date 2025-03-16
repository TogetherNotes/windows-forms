using System.Windows.Input;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class UsersVM : ViewModelBase
    {
        private object _currentUserView;
        public object CurrentUserView
        {
            get { return _currentUserView; }
            set { _currentUserView = value; OnPropertyChanged(); }
        }

        public ICommand ShowAdminCommand { get; set; }
        public ICommand ShowMusicianCommand { get; set; }
        public ICommand ShowRestaurantCommand { get; set; }
        public ICommand BackToUsersCommand { get; set; }

        private void ShowAdmin(object obj) { CurrentUserView = new ManageUsersVM(this); }
        private void ShowMusician(object obj) { CurrentUserView = new ManageUsersVM(this); }
        private void ShowRestaurant(object obj) { CurrentUserView = new ManageUsersVM(this); }
        private void BackToUsers(object obj) { CurrentUserView = null; } // Vuelve a la vista principal

        public UsersVM()
        {
            ShowAdminCommand = new RelayCommand(ShowAdmin);
            ShowMusicianCommand = new RelayCommand(ShowMusician);
            ShowRestaurantCommand = new RelayCommand(ShowRestaurant);
            BackToUsersCommand = new RelayCommand(BackToUsers);

            CurrentUserView = null; // Inicialmente muestra el menú principal
        }
    }
}
