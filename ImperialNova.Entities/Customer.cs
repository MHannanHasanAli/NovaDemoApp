using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Customer : DeleteUpdate
    {
        [Key] public int _Id { get; set; }
        public string _Name { get; set; }
        public string _Email { get; set; }
        public string _Phone { get; set; }
        public string _Address { get; set; }
        public string _Zip { get; set; }
        public string _City { get; set; }
        public string _Country { get; set; }

    }
}
