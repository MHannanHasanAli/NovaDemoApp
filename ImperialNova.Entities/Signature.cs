using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Signature
    {
        [Key]
        public int _Id { get; set; }
        public int DeliveryFormID { get; set; }
        public string SignatureValue { get; set; }
    }
}
