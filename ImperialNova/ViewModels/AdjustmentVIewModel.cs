using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class AdjustmentViewModel
    {

    }
    public class AdjustmentListingViewModel
    {
        public List<Adjustment> Adjustments { get; set; }
        public string SearchTerm { get; set; }
    }
    public class AdjustmentActionViewModel
    {
       public List<Locations> Locations { get; set; }
        public int _Id { get; set; }
        public DateTime _Date { get; set; }
        public string _Remarks { get; set; }
        public int _Quantity { get; set; }

    }
}