using GameStore.Data.Models;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
    public interface IShoppingCartsService
    {
        ShoppingCart AddToCart(Product product, Account account);

        ShoppingCart AddToCart(IEnumerable<Product> product, Account account);
    }
}