using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Notification
    {
        [Key] public int _Id { get; set; }
        public string _Description { get; set; }
        public string _UserName { get; set; }

    }
}
