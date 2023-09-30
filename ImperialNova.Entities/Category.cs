using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImperialNova.Entities
{
    public class Category: DeleteUpdate
    {
        [Key]
        public int _Id { get; set; }
        public string _CName { get; set; }
        public string _Description { get; set; }
       
    }
}
