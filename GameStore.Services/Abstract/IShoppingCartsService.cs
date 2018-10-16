using System.Collections.Generic;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IShoppingCartsService
    {
        ShoppingCart AddToCart(Product product, Account account);
        ShoppingCart AddToCart(IEnumerable<Product> product, Account account);
        ShoppingCart GetUserCart(Account account);
        void ClearUserCart(Account account);
    }
}