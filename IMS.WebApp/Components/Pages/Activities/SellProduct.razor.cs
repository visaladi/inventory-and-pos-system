using IMS.CoreBusiness;
using IMS.WebApp.ViewModels;
using Microsoft.JSInterop;
using static IMS.WebApp.Controls.Common.AutoCompleteComponent;

namespace IMS.WebApp.Components.Pages.Activities
{
    public partial class SellProduct
    {
        private SellViewModel sellViewModel = new SellViewModel();

        private Product? selectedProduct = null;

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                JSRuntime.InvokeVoidAsync("PreventFormSubmition", "sell-form");
            }
        }

        private async Task Sell()
        {
            if (this.selectedProduct != null)
            {
                await SellProductUseCase.ExecuteAsync(
                    this.sellViewModel.SalesOrderNumber,
                    this.sellViewModel.Product,
                    this.sellViewModel.QuantityToSell,
                    this.sellViewModel.UnitPrice
                    // "Frank" // Doneby
                    );
            }

            this.sellViewModel = new SellViewModel();
            this.selectedProduct = null;
        }

        public async Task<List<ItemViewModel>?> SearchProduct(string name)
        {
            var list = await ViewProductsByNameUseCase.ExecuteAsync(name);

            if (list == null) return null;

            return list.Select(x => new ItemViewModel { Id = x.ProductID, Name = x.ProductName })?.ToList();
        }

        private async Task OnItemSelected(ItemViewModel item)
        {
            this.selectedProduct = await ViewProductByIdUseCase.ExecuteAsync(item.Id);

            sellViewModel.ProductId = item.Id;
            sellViewModel.Product = this.selectedProduct;
            sellViewModel.UnitPrice = this.selectedProduct.Price;
        }
    }
}
