using System.Windows;
using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            var viewModel = new LoginVM();
            DataContext = viewModel;
        }

        private void txtPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginVM viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}