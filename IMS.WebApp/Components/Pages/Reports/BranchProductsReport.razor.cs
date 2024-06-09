using IMS.CoreBusiness;
using IMS.UseCases.Products;
using IMS.UseCases;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace IMS.WebApp.Components.Pages.Reports
{
    public partial class BranchProductsReport
    {
        private string email = "";
        private string errorMsg = "";
        private List<IdentityUser>? users;
        private List<Product> allProducts;
        private Dictionary<IdentityUser, List<Product>> branchProducts;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            email = authState.User.Identity?.Name;

            try
            {
                users = UserManager.Users.ToList();
                allProducts = (await ViewProductsByNameUseCase.ExecuteAsync("")).ToList();
                await LoadBranchProducts();
            }
            catch (Exception ex)
            {
                errorMsg = $"Error initializing page: {ex.Message}";
            }
        }

        private async Task LoadBranchProducts()
        {
            branchProducts = new Dictionary<IdentityUser, List<Product>>();

            try
            {
                foreach (var user in users)
                {
                    var products = new List<Product>();
                    foreach (var product in allProducts)
                    {
                        var productCopy = new Product
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            ImgUrl = product.ImgUrl,
                            Quantity = product.Quantity,
                            BranchQty = await GetBranchQuantityAsync(user.Id, product.ProductID)
                        };
                        products.Add(productCopy);
                    }
                    branchProducts[user] = products;
                }
            }
            catch (Exception ex)
            {
                errorMsg = $"Error loading branch products: {ex.Message}";
            }
        }

        private async Task<int> GetBranchQuantityAsync(string userId, int productId)
        {
            try
            {
                var cart = await ShoppingCartService.GetCartByUserIdAsync(userId);
                if (cart != null)
                {
                    var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);
                    if (cartItem != null)
                    {
                        return cartItem.Quantity;
                    }
                }
                return 0; // Return 0 if item not found in the cart
            }
            catch (Exception ex)
            {
                errorMsg = $"Error retrieving branch quantity for user {userId} and product {productId}: {ex.Message}";
                return -1; // Return -1 to indicate error
            }
        }
    }
}
