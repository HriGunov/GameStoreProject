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
        private readonly IAccountsService accountService;
        private readonly IProductsService productService;
        private readonly IGameStoreContext storeContext;

        public ShoppingCartsService(IGameStoreContext storeContext, IAccountsService accountService,
            IProductsService productService)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
            this.accountService = accountService;
            this.productService = productService;
        }

        /// <summary>
        ///     Adds the given product in the parameters to the account's cart.
        /// </summary>
        /// <param name="product">Product Name</param>
        /// <param name="account">Account Type</param>
        /// <returns></returns>
        public ShoppingCart AddToCart(string product, Account account)
        {
            // Move this check to Commands
            if (account.IsGuest)
                throw new UserException("Guests cannot add to their carts.");

            var tempProduct = productService.FindProduct(product);

            if (tempProduct == null)
                throw new UserException($"Product {product} doesn't exist.");

            if (ProductExistsInCart(tempProduct, account))
                throw new UserException($"Product {tempProduct.Name} already exists in the user's cart.");

            var tempCart = account.ShoppingCart;

            if (tempCart == null)
                throw new UserException($"User ({account.Username}) doesn't have Shopping Cart.");

            var shoppingCart = new ShoppingCartProducts
            {
                ShoppingCartId = tempCart.Id,
                ProductId = tempProduct.Id
            };

            storeContext.ShoppingCartProducts.Add(shoppingCart);
            storeContext.SaveChanges();

            return tempCart;
        }

        /// <summary>
        ///     Adds the given product in the parameters to the account's cart.
        /// </summary>
        /// <param name="product">Product Type</param>
        /// <param name="account">Account Type</param>
        /// <returns></returns>
        public ShoppingCart AddToCart(Product product, Account account)
        {
            // Move this check to Commands
            if (account.IsGuest)
                throw new UserException("Guests cannot add to their carts.");

            if (product == null)
                throw new UserException("No product given to add.");

            if (ProductExistsInCart(product, account))
                throw new UserException($"Product {product.Name} already exists in the user's cart.");

            var tempCart = account.ShoppingCart;

            if (tempCart == null)
                throw new UserException($"User ({account.Username}) doesn't have Shopping Cart.");

            var shoppingCart = new ShoppingCartProducts
            {
                ShoppingCartId = tempCart.Id,
                ProductId = product.Id
            };

            storeContext.ShoppingCartProducts.Add(shoppingCart);
            storeContext.SaveChanges();

            return tempCart;
        }

        /// <summary>
        ///     Adds multiple products to the account's cart.
        /// </summary>
        /// <param name="product">Product Type</param>
        /// <param name="account">Account Type</param>
        /// <returns></returns>
        public ShoppingCart AddToCart(IEnumerable<Product> product, Account account)
        {
            // Move this check to Commands
            if (account.IsGuest)
                throw new UserException("Guests cannot add to their carts.");

            if (!product.Any())
                throw new UserException("No products given to add.");

            var tempCart = account.ShoppingCart;

            if (tempCart == null)
                throw new UserException($"User ({account.Username}) doesn't have Shopping Cart.");

            foreach (var p in product)
            {
                if (ProductExistsInCart(p, account))
                    throw new UserException($"Product {p.Name} already exists in the user's cart.");

                var shoppingCart = new ShoppingCartProducts
                {
                    ShoppingCartId = tempCart.Id,
                    ProductId = p.Id
                };

                storeContext.ShoppingCartProducts.Add(shoppingCart);
            }

            storeContext.SaveChanges();

            return tempCart;
        }

        /// <summary>
        ///     Checks if the product already exists in the user's cart.
        /// </summary>
        /// <param name="product">Product Type</param>
        /// <param name="account">Account Type</param>
        /// <returns></returns>
        private bool ProductExistsInCart(Product product, Account account)
        {
            return storeContext.ShoppingCartProducts.Any(s =>
                s.ShoppingCartId == account.ShoppingCartId && s.ProductId == product.Id);
        }
    }
}