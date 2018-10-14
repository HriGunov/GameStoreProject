using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Exceptions;
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
            // Move this check to Commands
            if (account.IsGuest)
                throw new UserException("Guests cannot add to their carts.");

            if (product == null)
                throw new UserException($"Product {product.Name} doesn't exist.");

            if (ProductExistsInCart(product, account))
                throw new UserException($"Product {product.Name} already exists in the user's cart.");

            var tempCart = storeContext.Accounts.ToList().FirstOrDefault(c => c.Username == account.Username)
                ?.ShoppingCart;

            var shoppingCart = new ShoppingCartProducts
            {
                ShoppingCart = tempCart,
                Product = product
            };

            tempCart?.ShoppingCartProducts.Add(shoppingCart);
            storeContext.SaveChanges();

            return tempCart;
        }

        /// <summary>
        ///     Adds multiple products to the account's cart.
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="account">Account type</param>
        /// <returns></returns>
        public ShoppingCart AddToCart(IEnumerable<Product> product, Account account)
        {
            // Move this check to Commands
            if (account.IsGuest)
                throw new UserException("Guests cannot add to their carts.");

            if (!product.Any())
                throw new UserException("No products given to add.");

            var tempCart = storeContext.Accounts.ToList().FirstOrDefault(c => c.Username == account.Username)
                ?.ShoppingCart;

            foreach (var p in product)
            {
                if (ProductExistsInCart(p, account))
                    throw new UserException($"Product {p.Name} already exists in the user's cart.");

                var shoppingCart = new ShoppingCartProducts
                {
                    ShoppingCart = tempCart,
                    Product = p
                };

                tempCart?.ShoppingCartProducts.Add(shoppingCart);
            }

            storeContext.SaveChanges();

            return tempCart;
        }

        private bool ProductExistsInCart(Product product, Account account)
        {
            return storeContext.ShoppingCartProducts.Any(s =>
                s.ShoppingCartId == account.ShoppingCartId && s.ProductId == product.Id);
        }
    }
}