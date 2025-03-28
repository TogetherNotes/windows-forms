using System.Windows.Controls;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Control de usuario que representa un calendario en la aplicación.
    /// </summary>
    public partial class Calendar : UserControl
    {
        /// <summary>
        /// Constructor de la clase Calendar.
        /// Inicializa los componentes y asigna el ViewModel correspondiente.
        /// </summary>
        public Calendar()
        {
            InitializeComponent();
            DataContext = new CalendarVM(); // Asigna el ViewModel al contexto de datos.
        }
    }
}
