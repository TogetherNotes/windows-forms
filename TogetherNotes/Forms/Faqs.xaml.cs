using System.Linq;
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
            if (sender is Expander selectedExpander)
            {
                // Trobar tots els Expanders dins de l'ItemsControl
                var expanders = FindVisualChildren<Expander>(this).Where(exp => exp != selectedExpander);

                // Tancar tots els altres Expanders
                foreach (var expander in expanders)
                {
                    expander.IsExpanded = false;
                }
            }
        }

        // Funció genèrica per trobar tots els elements visuals d'un tipus dins d'un contenidor
        private static System.Collections.Generic.IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}