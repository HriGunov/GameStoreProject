using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

            account.ShoppingCart = GetUserCart(account);

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
                if (p != null)
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
            }
            storeContext.SaveChanges();

            account.ShoppingCart = GetUserCart(account);

            return tempCart;
        }

        /// <summary>
        /// Gets the user cart.
        /// </summary>
        /// <returns>The user cart.</returns>
        /// <param name="account">Account Type</param>
        public ShoppingCart GetUserCart(Account account)
        {
            return storeContext.ShoppingCarts
                               .Include(s => s.ShoppingCartProducts)
                                   .ThenInclude(sh => sh.Product)
                                   .ThenInclude(c => c.Comments)
                                   .ThenInclude(comment => comment.Account)
                               .Include(s => s.ShoppingCartProducts)
                                   .ThenInclude(sh => sh.Product)
                                   .ThenInclude(c => c.Comments)
                                   .ThenInclude(comment => comment.Product)
                               .Include(s => s.ShoppingCartProducts)
                                   .ThenInclude(sh => sh.Product)
                                   .ThenInclude(g => g.Genre)
                               .Include(s => s.ShoppingCartProducts)
                                   .ThenInclude(sh => sh.ShoppingCart)
                               .FirstOrDefault(s => s.ShoppingCartProducts.Any(sh => sh.ShoppingCartId == account.ShoppingCartId));
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