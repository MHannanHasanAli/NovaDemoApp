using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class CustomerViewModel
    {
        public class CustomerListingViewModel
        {
            public List<Customer> customers { get; set; }
        }
        public class CustomerActionViewModel
        {
            public int _Id { get; set; }
            public string _Name { get; set; }
            public string _Email { get; set; }
            public string _Phone { get; set; }
            public string _Address { get; set; }
            public string _Zip { get; set; }
            public string _City { get; set; }
            public string _Country { get; set; }

        }
    }
}