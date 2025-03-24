using System.Windows;

namespace TogetherNotes.Forms
{
    public partial class ExitConfirmation : Window
    {
        public bool UserConfirmedExit { get; private set; }

        public ExitConfirmation()
        {
            InitializeComponent();
            UserConfirmedExit = false; // Per defecte no tanca
        }

        private void ConfirmExit(object sender, RoutedEventArgs e)
        {
            UserConfirmedExit = true;
            this.DialogResult = true;
            this.Close();
        }

        private void CancelExit(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}