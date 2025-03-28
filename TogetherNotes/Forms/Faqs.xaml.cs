using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TogetherNotes.Forms
{
    public partial class Faqs : UserControl
    {
        public Faqs()
        {
            InitializeComponent();
        }

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

        // Función genérica para encontrar todos los elementos visuales de un tipo dentro de un contenedor
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
