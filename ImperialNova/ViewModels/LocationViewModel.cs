using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class LocationListingViewModel
    {
        public List<Locations> Locations { get; set; }
        public string SearchTerm { get; set; }
    }
    public class LocationActionViewModel
    {
        public int _Id { get; set; }
        public string _LocationName { get; set; }

    }
}