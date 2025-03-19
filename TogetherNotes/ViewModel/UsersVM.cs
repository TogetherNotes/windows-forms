using System.Windows.Input;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    public class UsersVM : ViewModelBase
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

        private void ShowAdmin(object obj) { CurrentUserView = new ManageAdminVM(this); }
        private void ShowMusician(object obj) { CurrentUserView = new ManageAdminVM(this); }
        private void ShowRestaurant(object obj) { CurrentUserView = new ManageAdminVM(this); }

        public UsersVM()
        {
            ShowAdminCommand = new RelayCommand(ShowAdmin);
            ShowMusicianCommand = new RelayCommand(ShowMusician);
            ShowRestaurantCommand = new RelayCommand(ShowRestaurant);

            CurrentUserView = null; // Inicialmente muestra el menú principal
        }
    }
}
