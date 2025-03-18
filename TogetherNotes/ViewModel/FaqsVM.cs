using System.Collections.ObjectModel;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class FaqsVM : ViewModelBase
    {
        public ObservableCollection<ShipmentFAQ> Questions { get; set; }

        public FaqsVM()
        {
            Questions = new ObservableCollection<ShipmentFAQ>
            {
                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq1Question"],
                                  Answer = (string)App.Current.Resources["Faq1Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq2Question"],
                                  Answer = (string)App.Current.Resources["Faq2Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq3Question"],
                                  Answer = (string)App.Current.Resources["Faq3Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq4Question"],
                                  Answer = (string)App.Current.Resources["Faq4Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq5Question"],
                                  Answer = (string)App.Current.Resources["Faq5Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq6Question"],
                                  Answer = (string)App.Current.Resources["Faq6Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq7Question"],
                                  Answer = (string)App.Current.Resources["Faq7Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq8Question"],
                                  Answer = (string)App.Current.Resources["Faq8Answer"] }
            };
        }
    }

    public class ShipmentFAQ
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}