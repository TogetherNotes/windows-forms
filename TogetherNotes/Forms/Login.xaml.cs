using System.Windows;
using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Clase que representa la interfaz de usuario para el inicio de sesión.
    /// </summary>
    public partial class Login : UserControl
    {
        /// <summary>
        /// Constructor de la clase Login.
        /// Inicializa los componentes de la interfaz.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento cuando la contraseña cambia en el PasswordBox.
        /// Sincroniza el valor de la contraseña con la vista modelo.
        /// </summary>
        /// <param name="sender">PasswordBox que activó el evento.</param>
        /// <param name="e">Datos del evento.</param>
        private void txtPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginVM viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
