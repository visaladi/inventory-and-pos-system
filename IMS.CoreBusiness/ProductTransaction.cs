using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class ProductTransaction
    {
        public int ProductTransactionId { get; set; }
        public string SONumber { get; set; } = string.Empty;
        public string ProductionNumber { get; set; } = string.Empty;
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int QauntityBefore { get; set; }
        [Required]
        public ProductTransactionType ActivityType { get; set; }
        [Required]
        public int QauntityAfter { get; set; }
        public double? UnitPrice { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public Product? Product { get; set; }


        // To add Sold Quantity & Total Price to the Report
        public int SoldQuantity { get; set; }
        public double TotalPrice { get; set; }


        // To display the branch
        public string? UserId { get; set; }
        public string? UserEmail { get; set; } = string.Empty;
    }
}
