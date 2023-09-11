using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Payment
    {
        [Key] public int _Id { get; set; }
        public int _Record { get; set; }
        public DateTime _Date { get; set; }
        public string _Individual { get; set; }
        public decimal _Amount { get; set; }
        public string _Remarks { get; set; }
    }
}
