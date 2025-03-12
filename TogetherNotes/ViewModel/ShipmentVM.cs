using System.Collections.ObjectModel;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class ShipmentVM : ViewModelBase
    {
        public ObservableCollection<ShipmentFAQ> Questions { get; set; }

        public ShipmentVM()
        {
            Questions = new ObservableCollection<ShipmentFAQ>
            {
                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq1Question"],
                                  Answer = (string)App.Current.Resources["Faq1Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq2Question"],
                                  Answer = (string)App.Current.Resources["Faq2Answer"] },

                new ShipmentFAQ { Question = (string)App.Current.Resources["Faq3Question"],
                                  Answer = (string)App.Current.Resources["Faq3Answer"] }
            };
        }
    }

    public class ShipmentFAQ
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}