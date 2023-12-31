﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Adjustment
    {
        [Key] public int _Id { get; set; }
        public DateTime _Date { get; set; }
        public string _Type { get; set; }
        public string _Remarks { get; set; }
        public string _Product { get; set; }
        public int _Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public string Type { get; set; }
    }
}
