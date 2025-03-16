using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class ManageUsersVM
    {
        private UsersVM _parentVM;

        public ICommand BackCommand { get; }

        public ManageUsersVM(UsersVM parentVM)
        {
            _parentVM = parentVM;
            BackCommand = new RelayCommand(Back);
        }

        private void Back(object obj)
        {
            _parentVM.CurrentUserView = null;
        }
    }

}
