using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IShoppingCartsService
    {
       Task<ShoppingCart> AddToCart(IEnumerable<int> productsId, string accountId);
        Task<ShoppingCart> AddToCart(int productId, string accountId);
        Task ClearUserCart(string accountId);
        Task<ShoppingCart> GetUserCart(string accountId);
    }
}