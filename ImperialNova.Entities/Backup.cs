using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Backup
    {
        [Key]
        public int _Id { get; set; }
        public int ComponenetId { get; set; }
        public string Aspect { get; set; }
        public string Type { get; set; }
        public DateTime DeletionDate { get; set; }
    }
}
