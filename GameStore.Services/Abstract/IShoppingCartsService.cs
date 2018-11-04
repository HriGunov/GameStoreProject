using System.Collections.Generic;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IShoppingCartsService
    {
        ShoppingCart AddToCart(IEnumerable<int> productsId, string accountId);
        ShoppingCart AddToCart(int productId, string accountId);
        void ClearUserCart(string accountId);
        ShoppingCart GetUserCart(string accountId);
    }
}