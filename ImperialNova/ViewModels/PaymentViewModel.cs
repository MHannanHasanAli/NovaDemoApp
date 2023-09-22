using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class PaymentViewModel
    {
        public class PaymentListingViewModel
        {
            public List<Payment> Payments { get; set; }
            public string SearchTerm { get; set; }
        }
        public class PaymentActionViewModel
        {
            public List<Customer> customers { get; set; }
            public int _Id { get; set; }
            public int _Record { get; set; }
            public DateTime _Date { get; set; }
            public string _Individual { get; set; }
            public decimal _Amount { get; set; }
            public string _Remarks { get; set; }

        }
    }
}