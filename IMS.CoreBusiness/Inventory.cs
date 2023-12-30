using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        [Required]
        [StringLength(150)]
        public string InventoryName { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public int Quantity { get; set; }
        [Range(10, int.MaxValue, ErrorMessage = "Must be greater than or equal to 10")]
        public double Price { get; set; }

        public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
    }
}
