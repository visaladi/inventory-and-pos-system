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
        }

        private void SellPage()
        {
            NavigationManager.NavigateTo($"/sellpage/{UserId}");
        }

    }
}
