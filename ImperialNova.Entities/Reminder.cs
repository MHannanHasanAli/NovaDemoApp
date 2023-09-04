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
        public string _Title { get; set; }
        public string _Text { get; set; }
    }
}
