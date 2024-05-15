using IMS.CoreBusiness;
using Microsoft.AspNetCore.Components;

namespace IMS.WebApp.Components.Pages.Carts
{
    public partial class UserCart
    {
        [Parameter]
        public string UserId { get; set; }
        private string? email;
        private Cart? cart;
        private List<CartItem> cartitems;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            email = authState.User.Identity?.Name;

            cart = await shoppingCartService.GetCartByUserIdAsync(UserId);
            cartitems = cart?.Items?.ToList() ?? new List<CartItem>();
        }

        private async Task RemoveFromCart(int cartItemId)
        {
            await shoppingCartService.RemoveCartItemAsync(cartItemId);
            // cartitems = await shoppingCartService.GetCartByUserIdAsync(UserId); // Refresh cart items after removal
        }


        #region Test 01
        // // From ChatGPT
        // private string? email;
        // private Cart? cart;
        // private IShoppingCartService cartService;

        // protected override async Task OnInitializedAsync()
        // {
        //     var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        //     email = authState.User.Identity?.Name;

        //     cartService = shoppingCartService;
        //     if (email != null)
        //     {
        //         cart = await cartService.GetCartByUserIdAsync(email);
        //     }
        // }

        // private async Task RemoveFromCart(int cartItemId)
        // {
        //     await cartService.RemoveCartItemAsync(cartItemId);
        //     if (email != null)
        //     {
        //         cart = await cartService.GetCartByUserIdAsync(email); // Refresh cart after removal
        //     }
        // }
        #endregion

    }
}
