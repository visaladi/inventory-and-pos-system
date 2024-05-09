using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ICartRepository cartRepository;

        public ShoppingCartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task AddItemToCartAsync(string userId, int productId, int quantity)
        {
            await cartRepository.AddItemToCartAsync(userId, productId, quantity);
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
