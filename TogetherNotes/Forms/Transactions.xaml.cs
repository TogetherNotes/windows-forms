using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TogetherNotes.ViewModel;

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

            // Mantenir el marc només a la bandera seleccionada
            BorderCatalan.BorderBrush = Brushes.Transparent;
            BorderSpanish.BorderBrush = Brushes.Transparent;
            BorderEnglish.BorderBrush = Brushes.Transparent;
            selectedBorder.BorderBrush = Brushes.White;

            // 🔄 Reinicialitzar la vista perquè els bindings es mantinguin
            var navVM = (NavigationVM)Application.Current.MainWindow.DataContext;
            var currentView = navVM.CurrentView;
            navVM.CurrentView = null;
            navVM.CurrentView = currentView;
        }

        private void LogoutClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}