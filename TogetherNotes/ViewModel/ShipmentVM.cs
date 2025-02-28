using System.Collections.ObjectModel;
using TogetherNotes.Models;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class ShipmentVM : ViewModelBase
    {
        private readonly PageModel _pageModel;

        private ObservableCollection<ShipmentFAQ> _questions;
        public ObservableCollection<ShipmentFAQ> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }

        public ShipmentVM()
        {
            _pageModel = new PageModel();
            Questions = new ObservableCollection<ShipmentFAQ>
                {
                    new ShipmentFAQ { Question = "How can I track my shipment?", Answer = "Use your tracking ID in the shipment portal." },
                    new ShipmentFAQ { Question = "What should I do if my package is delayed?", Answer = "Check the tracking details or contact support." },
                    new ShipmentFAQ { Question = "Can I change my delivery address?", Answer = "Yes, within 24 hours of placing the order." }
                };
        }
    }

    public class ShipmentFAQ
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}