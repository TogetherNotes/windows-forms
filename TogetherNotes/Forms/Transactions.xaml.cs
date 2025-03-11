using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TogetherNotes.Forms
{
    public partial class Transactions : UserControl
    {
        public Transactions()
        {
            InitializeComponent();
        }

        private void SetCatalan(object sender, RoutedEventArgs e) => UpdateLanguage("ca", BorderCatalan);
        private void SetSpanish(object sender, RoutedEventArgs e) => UpdateLanguage("es", BorderSpanish);
        private void SetEnglish(object sender, RoutedEventArgs e) => UpdateLanguage("en", BorderEnglish);

        private void UpdateLanguage(string languageCode, Border selectedBorder)
        {
            ((App)Application.Current).ChangeLanguage(languageCode);

            // Restablir el marc de totes les banderes
            BorderCatalan.BorderBrush = Brushes.Transparent;
            BorderSpanish.BorderBrush = Brushes.Transparent;
            BorderEnglish.BorderBrush = Brushes.Transparent;

            // Aplicar el cercle només a la bandera seleccionada
            selectedBorder.BorderBrush = Brushes.White;
        }

        private void LogoutClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}