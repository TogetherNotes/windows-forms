using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    public class ManageAdminVM : INotifyPropertyChanged
    {
        private UsersVM _parentVM;
        public ICommand BackCommand { get; }

        public ManageAdminVM(UsersVM parentVM)
        {
            _parentVM = parentVM;
            BackCommand = new RelayCommand(Back);
        }

        private void Back(object obj)
        {
            _parentVM.CurrentUserView = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
