﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Document : DeleteUpdate
    {
        [Key] public int _Id { get; set; }
        public string _Name { get; set; }
        public string _File { get; set; }
    }
}
