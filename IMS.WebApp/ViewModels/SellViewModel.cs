using IMS.CoreBusiness;
using System.Collections.Generic;
using IMS.WebApp.ViewModelsValidations;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class SellViewModel
    {
        [Required]
        public string SalesOrderNumber { get; set; } = string.Empty;
        public List<SellItemViewModel> ItemsToSell { get; set; } = new List<SellItemViewModel>();
    }

    public class SellItemViewModel
    {
        public int CartItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity has to be greater than 1.")]
        //[Sell_EnsureEnoughProductQuantity]
        public int QuantityToSell { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "UnitPrice has to be greater than 0.")]
        public double UnitPrice { get; set; }
        public Product? Product { get; set; }


        // To add Sold Quantity & Total Price to the Report
        public int SoldQuantity { get; set; }
        public double TotalPrice => QuantityToSell * UnitPrice;
    }
}
