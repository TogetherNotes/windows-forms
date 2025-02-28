using System.Windows;
using System.Windows.Input;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ShutdownWindow(object sender, RoutedEventArgs e)
        {
            ExitConfirmation exitPopup = new ExitConfirmation();
            exitPopup.Owner = this; // Assegura que la finestra modal es centri sobre la principal

            bool? result = exitPopup.ShowDialog(); // Mostra la finestra i espera resposta

            if (result == true)
            {
                Application.Current.Shutdown();
            }
        }
    }
}