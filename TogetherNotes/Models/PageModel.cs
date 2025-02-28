using System;
using System.Collections.Generic;

namespace TogetherNotes.Models
{
    public class PageModel
    {
        public int CustomerCount { get; set; }
        public string ProductStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TransactionValue { get; set; }
        public TimeSpan ShipmentDelivery { get; set; }
        public bool LocationStatus { get; set; }

        // 📌 Afegim un mètode per obtenir preguntes sobre l'enviament
        public List<ShipmentFAQ> GetShipmentFAQs()
        {
            return new List<ShipmentFAQ>
            {
                new ShipmentFAQ { Question = "How can I track my shipment?", Answer = "Use your tracking ID in the shipment portal." },
                new ShipmentFAQ { Question = "What should I do if my package is delayed?", Answer = "Check the tracking details or contact support." },
                new ShipmentFAQ { Question = "Can I change my delivery address?", Answer = "Yes, within 24 hours of placing the order." }
            };
        }
    }

    // Definició de la classe ShipmentFAQ
    public class ShipmentFAQ
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}