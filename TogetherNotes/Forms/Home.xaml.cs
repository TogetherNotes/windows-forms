using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Representa la interfaz de usuario para la pantalla de inicio.
    /// </summary>
    public partial class Home : UserControl
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HomeVM"/>.
        /// </summary>
        public Home()
        {
            InitializeComponent();
            this.DataContext = new HomeVM();
        }
    }
}