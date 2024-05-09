using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class BranchItem
    {
        [Key]
        public int BranchItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? UserId { get; set; }
        public Product? Product { get; set; } // Navigation property
    }
}
