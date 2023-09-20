using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class InventoryInViewModel
    {

    }

    public class InventoryInListingViewModel
    {
        public List<Entities.InventoryIn> inventoryins { get; set; }
        

    }

    public class InventoryInActionViewModel
    {
        public List<Entities.Supplier> suppliers { get; set; }
        public List<Locations> locations { get; set; }
        public List<InventoryInProduct> products { get; set; }
        public int _Id { get; set; }
        public DateTime _Date { get; set; }
        public string _ShippingCompany { get; set; }
        public string _Tracking { get; set; }
        public string _Supplier { get; set; }
        public string _Status { get; set; }
        public string _Quantity { get; set; }
        public string _Amount { get; set; }
    }
}