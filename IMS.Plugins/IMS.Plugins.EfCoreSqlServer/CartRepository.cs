using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EfCoreSqlServer
{
    public class CartRepository : ICartRepository
    {
        private readonly IMSContext context;

        public CartRepository(IMSContext context)
        {
            this.context = context;
        }

        public async Task AddItemToCartAsync(string userId, int productId, int quantity = 1)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                context.Carts.Add(cart);
                await context.SaveChangesAsync();
            }

            var cartItem = await context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem { CartId = cart.CartId, ProductId = productId, Quantity = quantity };
                context.CartItems.Add(cartItem);
                await context.SaveChangesAsync();
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            await context.SaveChangesAsync();
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await context.Carts
                .Include(cart => cart.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task UpdateCartItemAsync(int cartItemId, int quantity)
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                context.CartItems.Remove(cartItem);
                await context.SaveChangesAsync();
            }
        }
    }
}
