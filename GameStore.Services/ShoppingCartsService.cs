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
            var cart = storeContext.ShoppingCarts.ToList().FirstOrDefault(c => c.Account.Username == account.Username);
            if (cart == null)
            {
                AddCart(account);
                return AddToCart(product, account);
            }

            cart?.Products.Add(product);
            storeContext.SaveChanges();

            return cart;
        }

        /// <summary>
        ///     Adds multiple products to the account's cart.
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="account">Account type</param>
        /// <returns></returns>
        public ShoppingCart AddToCart(IEnumerable<Product> product, Account account)
        {
            var cart = storeContext.ShoppingCarts.ToList().FirstOrDefault(c => c.Account.Username == account.Username);
            if (cart == null)
            {
                AddCart(account);
                return AddToCart(product, account);
            }

            foreach (var p in product) cart?.Products.Add(p);

            storeContext.SaveChanges();

            return cart;
        }

        /// <summary>
        ///     Creates a cart for the given account in the parameters.
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns></returns>
        private ShoppingCart AddCart(Account account)
        {
            var tempCart = new ShoppingCart {Account = account, AccountId = account.Id, Products = new List<Product>()};

            storeContext.ShoppingCarts.Add(tempCart);
            storeContext.SaveChanges();

            return tempCart;
        }
    }
}