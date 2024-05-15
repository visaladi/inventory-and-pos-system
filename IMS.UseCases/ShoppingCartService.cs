using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.Extensions.Logging;

namespace IMS.UseCases
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ICartRepository cartRepository;
        private readonly ILogger<ShoppingCartService> logger;

        public ShoppingCartService(ICartRepository cartRepository, ILogger<ShoppingCartService> logger)
        {
            this.cartRepository = cartRepository;
            this.logger = logger;
        }

        public async Task AddItemToCartAsync(string userId, int productId, int quantity)
        {
            if (string.IsNullOrEmpty(userId))
            {
                // Handle case where user is not selected (null or empty userId)
                // Here you can choose an approach based on your needs:
                //  - Throw an exception (if critical)
                //  - Log a warning message
                //  - Display an error message to the user
                //throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty."); // Example: Throw exception

                logger.LogWarning("UserId Cannot be Null or Empty");
                return;
            }
            else
            {
                await cartRepository.AddItemToCartAsync(userId, productId, quantity);
            }

        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await cartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task UpdateCartItemAsync(int cartItemId, int quantity)
        {
            await cartRepository.UpdateCartItemAsync(cartItemId, quantity);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await cartRepository.RemoveCartItemAsync(cartItemId);
        }
    }
}
