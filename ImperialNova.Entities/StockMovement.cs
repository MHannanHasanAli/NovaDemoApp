using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class StockMovement
    {
        [Key] public int _Id { get; set; }
        public int _Fdays { get; set; }
        public int _Ffrom{ get; set; }
        public int _Fto { get; set; }
        public int _Sdays { get; set; }
        public int _Sfrom{ get; set; }
        public int _Sto { get; set; }
    }
}
