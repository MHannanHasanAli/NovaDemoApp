using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class OrderViewModel
    {
    }
    public class OrderListingViewModel
    {
        public List<Entities.Order> orders { get; set; }
    }

    public class OrderActionViewModel
    {
        public List<Customer> customers { get; set; }
        public List<Locations> locations { get; set; }
        public List<OrderProduct> products { get; set; }
        public int _Id { get; set; }
        public DateTime _Date { get; set; }
        public string _Record { get; set; }
        public DateTime _ShipByDate { get; set; }
        public string _Tracking { get; set; }
        public string _Status { get; set; }
        public string _Customer { get; set; }
        public string _Priority { get; set; }
        public string _Quantity { get; set; }
        public string _Amount { get; set; }
        public string _IsPacked { get; set; }
    }
}