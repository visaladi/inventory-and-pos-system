using IMS.CoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public int QuantityToSell { get; set; } = 0;
        public double UnitPrice { get; set; }
        public Product? Product { get; set; }


        public int Quantity { get; set; }
    }
}
