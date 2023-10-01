using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class AdjustmentProduct 
    {
        [Key] public int _Id { get; set; }
        public int _AdjustmentId { get; set; }
        public string _Photo { get; set; }
        public string _SKU { get; set; }
        public string _Title { get; set; }
        public int _Qty { get; set; }
    }
}
