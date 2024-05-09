using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public interface IShoppingCartService
    {
        Task AddItemToCartAsync(string userId, int productId, int quantity);
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task UpdateCartItemAsync(int cartItemId, int quantity);
        Task RemoveCartItemAsync(int cartItemId);
    }
}
