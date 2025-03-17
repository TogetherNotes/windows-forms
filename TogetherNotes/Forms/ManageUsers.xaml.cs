using System;
using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Lógica de interacción para ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : UserControl
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        public ManageUsers(UsersVM parentVM) : this()
        {
            this.DataContext = new ManageUsersVM(parentVM);
        }

    }
}
