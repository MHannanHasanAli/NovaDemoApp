﻿using System.ComponentModel.DataAnnotations;

namespace ImperialNova.Entities
{
    public class Locations
    {
        [Key]
        public int _Id { get; set; }
        public string _LocationName { get; set; }
    }
}