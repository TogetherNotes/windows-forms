using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TogetherNotes.Forms
{
    /// <summary>
    /// Clase que representa la interfaz de preguntas frecuentes (FAQs).
    /// </summary>
    public partial class Faqs : UserControl
    {
        /// <summary>
        /// Constructor de la clase Faqs.
        /// Inicializa los componentes de la interfaz.
        /// </summary>
        public Faqs()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento cuando un Expander es expandido.
        /// Se asegura de que solo un Expander esté abierto a la vez.
        /// </summary>
        /// <param name="sender">El Expander que ha sido expandido.</param>
        /// <param name="e">Datos del evento.</param>
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expandedExpander)
            {
                // Cerrar todos los Expanders excepto el seleccionado
                foreach (var expander in FindVisualChildren<Expander>(this))
                {
                    if (expander != expandedExpander)
                    {
                        expander.IsExpanded = false;
                    }
                }
            }
        }

        /// <summary>
        /// Método genérico para encontrar todos los elementos visuales de un tipo dentro de un contenedor.
        /// </summary>
        /// <typeparam name="T">El tipo de elemento a buscar.</typeparam>
        /// <param name="parent">El contenedor donde se buscarán los elementos.</param>
        /// <returns>Enumeración de elementos encontrados del tipo especificado.</returns>
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                {
                    yield return typedChild;
                }

                // Recursión para encontrar los elementos visuales hijos
                foreach (var childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}