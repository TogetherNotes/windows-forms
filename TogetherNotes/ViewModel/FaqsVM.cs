using System.Collections.ObjectModel;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    /// <summary>
    /// ViewModel que gestiona las preguntas y respuestas frecuentes (FAQ) de la vista.
    /// Esta clase inicializa una colección de preguntas y respuestas que serán mostradas en la vista.
    /// </summary>
    class FaqsVM : ViewModelBase
    {
        /// <summary>
        /// Colección observable de preguntas frecuentes (FAQ).
        /// </summary>
        public ObservableCollection<FAQ> Questions { get; set; }

        /// <summary>
        /// Constructor de la clase FaqsVM.
        /// Inicializa la colección de preguntas y respuestas utilizando recursos definidos en la aplicación.
        /// </summary>
        public FaqsVM()
        {
            Questions = new ObservableCollection<FAQ>
            {
                // Carga las preguntas y respuestas desde los recursos de la aplicación.
                // Cada FAQ es una instancia de la clase FAQ que contiene una pregunta y su respectiva respuesta.
                
                new FAQ
                {
                    Question = (string)App.Current.Resources["Faq1Question"],
                    Answer = (string)App.Current.Resources["Faq1Answer"]
                },

                new FAQ
                {
                    Question = (string)App.Current.Resources["Faq2Question"],
                    Answer = (string)App.Current.Resources["Faq2Answer"]
                },

                new FAQ
                {
                    Question = (string)App.Current.Resources["Faq3Question"],
                    Answer = (string)App.Current.Resources["Faq3Answer"]
                },

                new FAQ
                {
                    Question = (string)App.Current.Resources["Faq4Question"],
                    Answer = (string)App.Current.Resources["Faq4Answer"]
                },

                new FAQ
                {
                    Question = (string)App.Current.Resources["Faq5Question"],
                    Answer = (string)App.Current.Resources["Faq5Answer"]
                },

                new FAQ
                {
                    Question = (string)App.Current.Resources["Faq6Question"],
                    Answer = (string)App.Current.Resources["Faq6Answer"]
                },

                new FAQ
                {
                    Question = (string)App.Current.Resources["Faq7Question"],
                    Answer = (string)App.Current.Resources["Faq7Answer"]
                },

                new FAQ
                {
                    Question = (string)App.Current.Resources["Faq8Question"],
                    Answer = (string)App.Current.Resources["Faq8Answer"]
                }
            };
        }
    }
}
