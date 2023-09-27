using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class CSVViewModel
    {
        
    }
    public class CSVListingViewModel
    {
        public List<Entities.CSV> csvs { get; set; }
    }

    public class CSVActionViewModel
    {
        public int _Id { get; set; }
        public DateTime _Date { get; set; }
        public string _Description { get; set; }
        public string _File { get; set; }
    }
}