using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ImperialNova.Entities
{
    public class DeliveryForm : DeleteUpdate
    {
        [Key]
        public int _id { get; set; }
        public string _SlaesPerson { get; set; }
        public DateTime _Date { get; set; }
        public string _CustomerName { get; set; }
        public string _Address { get; set; }
        public string _Country { get; set; }
        public string _ContactNo { get; set; }
        public string _Email { get; set; }
        public string _Note { get; set; }

        public decimal _CashPaid { get; set; }
        public decimal _CardPaid { get; set; }
        public decimal _FinancePaid { get; set; }
        public string _FinanceCompany { get; set; }
        public string _SignatureData { get; set; }
        //public List<DeliveryFormProductsDB> _Products { get; set; }
        public string _RequestedDate { get; set; }

        public string ProductsData { get; set; }
        public decimal _TotalAmount { get; set; }
        public string _PayMethod { get; set; }
        public string _OrderNumber { get; set; }
        public decimal _AmountPaid { get; set; }
        public decimal _AmountInBalance { get; set; }

    }
    public class DeliveryFormProductsDB
    {
        public string _ProductName { get; set; }
        public int _ProductQuantity { get; set; }
        public decimal _CostPrice { get; set; }
        public decimal _SalePrice { get; set; }
    }
}
