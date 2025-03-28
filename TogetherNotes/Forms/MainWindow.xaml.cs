using System.Windows;
using System.Windows.Input;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Clase que representa la ventana principal de la aplicación.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor de la ventana principal.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Minimiza la ventana actual.
        /// </summary>
        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Permite arrastrar la ventana cuando se presiona el botón izquierdo del mouse.
        /// </summary>
        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Muestra una ventana de confirmación antes de cerrar la aplicación.
        /// </summary>
        private void ShutdownWindow(object sender, RoutedEventArgs e)
        {
            ExitConfirmation exitPopup = new ExitConfirmation();
            exitPopup.Owner = this; // Asegura que la ventana modal se centre sobre la principal

            bool? result = exitPopup.ShowDialog(); // Muestra la ventana y espera respuesta

            if (result == true)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
