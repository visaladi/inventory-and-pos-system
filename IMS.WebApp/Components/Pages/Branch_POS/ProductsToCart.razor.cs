using IMS.CoreBusiness;

namespace IMS.WebApp.Components.Pages.Branch_POS
{
    public partial class ProductsToCart
    {
        private string email = "";
        private IEnumerable<Product> products;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            email = authState.User.Identity?.Name;

            products = (await ViewProductsByNameUseCase.ExecuteAsync("")).ToList();
        }


        private async Task AddToCart(int productId)
        {
            #region AddToCart_Test
            // var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            // var userId = authState.User.Identity?.Name; // Get signed-in user's ID

            // if (userId != null)
            // {
            //     await ShoppingCartService.AddItemToCartAsync(userId, productId, 1); // Add 1 unit by default
            // }
            // else
            // {
            //     // Handle scenario where user is not signed in (e.g., display a message or redirect to login)
            // }

            // var user = await UserManager.GetUserAsync(await AuthenticationStateProvider.GetAuthenticationStateAsync());




            // var user = await UserManager.GetUserAsync(await AuthenticationStateProvider.GetAuthenticationStateAsync());
            // var userId = user?.Id;
            // // var user = await UserManager.GetUserAsync(HttpContext.User);
            // // var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier); // Assuming UserId is stored in NameIdentifier claim

            // if (userId != null)
            // {
            //     await ShoppingCartService.AddItemToCartAsync(userId, productId, 1);
            // }
            // else
            // {
            //     // Handle scenario where user is not signed in
            // }
            #endregion

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);
            var userId = user?.Id;

            if (userId != null)
            {
                await ShoppingCartService.AddItemToCartAsync(userId, productId, 1);
            }
            else
            {
            }

            NavigationManager.NavigateTo($"/usercart/{userId}");
        }

        #region GetUserEmail

        // private string GetUserEmail()
        // {
        // var claimsPrincipal = await HttpContext.GetCurrentUser();
        // if (claimsPrincipal?.Identity?.IsAuthenticated == true)
        // {
        //     return claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        // }
        // return "";


        // if (HttpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false)
        // {
        //     return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        // }

        // return "Not Signed In";
        // }
        #endregion
    }
}
