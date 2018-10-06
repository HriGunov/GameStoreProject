using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IShoppingCartsService
    {
        ShoppingCart AddToCart(Product product, Account account);
    }
}