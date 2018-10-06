using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Services
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IGameStoreContext storeContext;

        public ShoppingCartsService(IGameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        /// <summary>
        ///     Adds the given product in the parameters to the account's cart.
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="account">Account type</param>
        /// <returns></returns>
        public ShoppingCart AddToCart(Product product, Account account)
        {
            var tempAccount = storeContext.Accounts.ToList().FirstOrDefault(c => c.Username == account.Username)?.ShoppingCart;

            tempAccount?.Products.Add(product);
            storeContext.SaveChanges();

            return tempAccount;
        }

        /// <summary>
        ///     Adds multiple products to the account's cart.
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="account">Account type</param>
        /// <returns></returns>
        public ShoppingCart AddToCart(IEnumerable<Product> product, Account account)
        {
            var tempAccount = storeContext.Accounts.ToList().FirstOrDefault(c => c.Username == account.Username)?.ShoppingCart;

            foreach (var p in product) tempAccount?.Products.Add(p);

            storeContext.SaveChanges();

            return tempAccount;
        }
    }
}