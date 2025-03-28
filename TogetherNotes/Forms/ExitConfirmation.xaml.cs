using System.Windows;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Ventana de confirmación para salir de la aplicación.
    /// </summary>
    public partial class ExitConfirmation : Window
    {
        /// <summary>
        /// Indica si el usuario ha confirmado la salida de la aplicación.
        /// </summary>
        public bool UserConfirmedExit { get; private set; }

        /// <summary>
        /// Constructor de la ventana ExitConfirmation.
        /// Inicializa los componentes y establece el valor predeterminado de confirmación en falso.
        /// </summary>
        public ExitConfirmation()
        {
            InitializeComponent();
            UserConfirmedExit = false; // Por defecto, la ventana no cierra la aplicación.
        }

        /// <summary>
        /// Maneja el evento de confirmación de salida.
        /// Establece UserConfirmedExit en true y cierra la ventana con resultado positivo.
        /// </summary>
        private void ConfirmExit(object sender, RoutedEventArgs e)
        {
            UserConfirmedExit = true;
            this.DialogResult = true; // Devuelve true al diálogo padre.
            this.Close();
        }

        /// <summary>
        /// Maneja el evento de cancelación de salida.
        /// Cierra la ventana sin confirmar la salida.
        /// </summary>
        private void CancelExit(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Devuelve false al diálogo padre.
            this.Close();
        }
    }
}
