using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Reminder
    {
        [Key] public int _Id { get; set; }
        public DateTime _CreatedAt { get; set; }
        public string _CreatedBy { get; set; }
        public string _Title { get; set; }
        public string _Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Type { get; set; }
    }
}
