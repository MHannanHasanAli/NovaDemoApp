﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class TodoList
    {
        [Key] public int _Id { get; set; }
        public DateTime  _DueDate { get; set; }
        public string _Description { get; set; }
        public string _File { get; set; }
        public bool _Ticked { get; set; }
        public bool IsDeleted { get; set; }
        public string Type { get; set; }
    }
}
