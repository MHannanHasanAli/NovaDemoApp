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
        public int _Id { get; set; }
        public DateTime _Date { get; set; }
        public string _Record { get; set; }
        public DateTime _ShipByDate { get; set; }
        public string _Tracking { get; set; }
        public string _Status { get; set; }
        public int _CustomerId { get; set; }
        public string _Priority { get; set; }
        public int _ProductId { get; set; }
        public string _Photo { get; set; }
        public string _SKU { get; set; }
        public string _Title { get; set; }
        public int _Qty { get; set; }
        public decimal _Price { get; set; }
        public decimal _Amount { get; set; }
        public string _IsPacked { get; set; }
    }
}