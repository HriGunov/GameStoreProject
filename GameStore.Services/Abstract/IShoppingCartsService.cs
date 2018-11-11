using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IShoppingCartsService
    {
       Task<ShoppingCart> AddToCartAsync(IEnumerable<int> productsId, string accountId);
        Task<ShoppingCart> AddToCartAsync(int productId, string accountId);
        Task ClearUserCartAsync(string accountId);
        Task<ShoppingCart> GetUserCartAsync(string accountId);
        Task<bool> ProductExistsInCartAsync(int productId, string accountId);
        Task<Product> RemoveProductCartAsync(string accountId, int productId);
    }
}