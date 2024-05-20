using IMS.CoreBusiness;

namespace IMS.WebApp.ViewModels
{
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int QuantityToSell { get; set; }
        public double UnitPrice { get; set; }
        public Product? Product { get; set; }


        public int Quantity { get; set; }
    }
}