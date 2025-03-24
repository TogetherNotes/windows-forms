using System.Collections.ObjectModel;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class FaqsVM : ViewModelBase
    {
        public ObservableCollection<FAQ> Questions { get; set; }

        public FaqsVM()
        {
            Questions = new ObservableCollection<FAQ>
            {
                new FAQ { Question = (string)App.Current.Resources["Faq1Question"],
                                  Answer = (string)App.Current.Resources["Faq1Answer"] },

                new FAQ { Question = (string)App.Current.Resources["Faq2Question"],
                                  Answer = (string)App.Current.Resources["Faq2Answer"] },

                new FAQ { Question = (string)App.Current.Resources["Faq3Question"],
                                  Answer = (string)App.Current.Resources["Faq3Answer"] },

                new FAQ { Question = (string)App.Current.Resources["Faq4Question"],
                                  Answer = (string)App.Current.Resources["Faq4Answer"] },

                new FAQ { Question = (string)App.Current.Resources["Faq5Question"],
                                  Answer = (string)App.Current.Resources["Faq5Answer"] },

                new FAQ { Question = (string)App.Current.Resources["Faq6Question"],
                                  Answer = (string)App.Current.Resources["Faq6Answer"] },

                new FAQ { Question = (string)App.Current.Resources["Faq7Question"],
                                  Answer = (string)App.Current.Resources["Faq7Answer"] },

                new FAQ { Question = (string)App.Current.Resources["Faq8Question"],
                                  Answer = (string)App.Current.Resources["Faq8Answer"] }
            };
        }
    }

    public class FAQ
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}