using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{

    public class DeliveryFormViewModel
    {
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public string SearchTerm { get; set; }
        public List<DeliveryForm> forms{ get; set; }
    }
}