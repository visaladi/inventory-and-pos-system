using IMS.CoreBusiness;
using IMS.WebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using static IMS.WebApp.Controls.Common.AutoCompleteComponent;
using System.Security.Claims;

namespace IMS.WebApp.Components.Pages.Sell_Page
{
    public partial class POS_Page_1
    {
        public string UserEmail = "";

        [Parameter]
        public string UserId { get; set; }
        private List<CartItemViewModel> cartItems;
        private SellViewModel sellViewModel = new SellViewModel();


        protected override async Task OnInitializedAsync()
        {
            var cart = await ShoppingCartService.GetCartByUserIdAsync(UserId);
            cartItems = cart?.Items?.Select(ci => new CartItemViewModel
            {
                CartItemId = ci.CartItemId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                QuantityToSell = 0,
                UnitPrice = ci.Product.Price,
                Product = ci.Product
            }).ToList() ?? new List<CartItemViewModel>();

            // Generate sales order number
            sellViewModel.SalesOrderNumber = GenerateSalesOrderNumber();

        }

        private string GenerateSalesOrderNumber()
        {
            // return $"SO-{DateTime.Now:yyyyMMddHHmmss}-{UserId[0,3]}";


            var now = DateTime.Now;
            var year = now.ToString("yy"); // Last 2 digits of the year
            var month = now.ToString("MM"); // Month number
            var day = now.ToString("dd"); // Day
            var hour = now.ToString("HH"); // Hour
            var minute = now.ToString("mm"); // Minute
                                             // var second = now.ToString("ss"); // Second

            var branchIdPart = UserId.Length >= 3 ? UserId.Substring(0, 3) : UserId;

            return $"SO-{year}{month}{day}{hour}{minute}-{branchIdPart}";
        }

        private async Task Sell()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            UserEmail = authState.User.Identity?.Name;

            foreach (var item in cartItems)
            {
                if (item.QuantityToSell > 0 && item.QuantityToSell <= item.Quantity)
                {
                    await SellProductUseCase.ExecuteAsync(
                        sellViewModel.SalesOrderNumber,
                        item.Product,
                        item.QuantityToSell,
                        item.UnitPrice,
                        UserId, // Pass user information
                        UserEmail
                    );

                    item.Quantity -= item.QuantityToSell;
                    if (item.Quantity <= 0)
                    {
                        await ShoppingCartService.RemoveCartItemAsync(item.CartItemId);
                    }
                    else
                    {
                        await ShoppingCartService.UpdateCartItemAsync(item.CartItemId, item.Quantity);
                    }
                }
            }

            // Refresh the cartItems after selling
            cartItems = (await ShoppingCartService.GetCartByUserIdAsync(UserId))?.Items?.Select(ci => new CartItemViewModel
            {
                CartItemId = ci.CartItemId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                QuantityToSell = 0,
                UnitPrice = ci.Product.Price,
                Product = ci.Product
            }).ToList() ?? new List<CartItemViewModel>();

            // Generate a new sales order number after selling
            sellViewModel.SalesOrderNumber = GenerateSalesOrderNumber();
        }


        public async Task<List<ItemViewModel>?> SearchProduct(string name)
        {
            var list = await ViewProductsByNameUseCase.ExecuteAsync(name);
            if (list == null) return null;
            return list.Select(x => new ItemViewModel { Id = x.ProductID, Name = x.ProductName })?.ToList();
        }

        private async Task GoTo_Cart(string userId)
        {
            NavigationManager.NavigateTo($"/usercart/{userId}");
        }

    }
