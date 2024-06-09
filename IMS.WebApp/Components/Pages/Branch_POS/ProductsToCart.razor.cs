using IMS.CoreBusiness;
using Microsoft.AspNetCore.Identity;

namespace IMS.WebApp.Components.Pages.Branch_POS
{
    public partial class ProductsToCart
    {
        private string email = "";
        private string errorMsg = "";
        private IEnumerable<Product> products;
        private string selectedUserId;
        private List<IdentityUser>? users;
        private int selectedQuantity;

        private Dictionary<int, int> quantities = new Dictionary<int, int>();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            email = authState.User.Identity?.Name;
            products = (await ViewProductsByNameUseCase.ExecuteAsync("")).ToList();

            try
            {
                users = UserManager.Users.ToList();
            }
            catch (Exception ex)
            {
                errorMsg = $"Error retrieving users: {ex.Message}";
            }

            foreach (var product in products)
            {
                quantities[product.ProductID] = 0;
            }
        }


        private async Task AddAllToCart()
        {
            if (string.IsNullOrEmpty(selectedUserId))
            {
                errorMsg = "No User is Selected!";
                return;
            }

            foreach (var product in products)
            {
                if (product != null)
                {
                    if (quantities.TryGetValue(product.ProductID, out var selectedQuantity) && selectedQuantity > 0)
                    {
                        product.Quantity -= selectedQuantity;
                        await ProductRepository.UpdateProductAsync(product);
                        await ShoppingCartService.AddItemToCartAsync(selectedUserId, product.ProductID, selectedQuantity);

                        errorMsg = $"{selectedQuantity} {product.ProductName} added to {selectedUserId}'s cart";
                    }
                }
            }

            products = (await ViewProductsByNameUseCase.ExecuteAsync("")).ToList();
        }


        private async Task GoTo_Cart(string userId)
        {
            NavigationManager.NavigateTo($"/usercart/{userId}");
        }

    }
}
