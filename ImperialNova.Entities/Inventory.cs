using System.ComponentModel.DataAnnotations;

namespace ImperialNova.Entities
{
    public class Inventory
    {
        [Key]
        public int _Id { get; set; }
        public int _ProductId { get; set; }
        public string _ProductName { get; set; }
        public string _Store { get; set; }
        public int _ToBeChangedQuantity { get; set; }
    }
}
