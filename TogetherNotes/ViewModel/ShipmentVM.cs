using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using TogetherNotes.Models;
using TogetherNotes.Utils;

namespace TogetherNotes.ViewModel
{
    class ShipmentVM : ViewModelBase
    {
        private readonly PageModel _pageModel;
        private readonly Timer _timer;

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

            // Inicialitza un thread que consulta el servidor cada 10 segons
            _timer = new Timer(UpdateShipmentInfo, null, 0, 10000);
        }

        private async void UpdateShipmentInfo(object state)
        {
            try
            {
                // Simulació de consulta a la API del servidor
                await Task.Delay(500); // Simula una resposta del servidor

                var updatedData = _pageModel.GetShipmentFAQs(); // Suposant que `PageModel` té una funció per obtenir FAQ

                if (updatedData != null)
                {
                    // Actualitza la llista a la UI des del thread principal
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        Questions.Clear();
                        foreach (var item in updatedData)
                        {
                            Questions.Add(new ShipmentFAQ { Question = item.Question, Answer = item.Answer });
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating shipment info: " + ex.Message);
            }
        }
    }

    public class ShipmentFAQ
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}