using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class StockMovementViewModel
    {

    }

    public class StockMovementListingViewModel
    {
        public List<Entities.StockMovement> stockdata{ get; set; }
        

    }

    public class StockMovementActionViewModel
    {
        public int _Id { get; set; }
        public int _Fdays { get; set; }
        public int _Ffrom { get; set; }
        public int _Fto { get; set; }
        public int _Sdays { get; set; }
        public int _Sfrom { get; set; }
        public int _Sto { get; set; }
    }
}