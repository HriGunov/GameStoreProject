using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly GameStoreContext storeContext;

        public ShoppingCartsService(GameStoreContext storeContext, IAccountsService accountService,
            IProductsService productsService)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        /// <summary>
        ///     Adds the given product in the parameters to the account's cart.
        /// </summary>
        /// <param name="product">Product Type</param>
        /// <param name="account">Account Type</param>
        /// <returns></returns>
        public async Task<ShoppingCart> AddToCart(int productId, string accountId)
        {
            var product = await storeContext.Products.FindAsync(productId);
            var account = await storeContext.Accounts.Where(acc => acc.Id == accountId).Include(acc => acc.ShoppingCart)
                .SingleAsync();
            // Move this check to Commands 

            if (product == null)
                throw new UserException("No product given to add.");

            if (await ProductExistsInCart(product, account))
                throw new UserException($"Product {product.Name} already exists in the user's cart.");

            var tempCart = account.ShoppingCart;

            if (tempCart == null)
                throw new UserException($"User ({account.UserName}) doesn't have Shopping Cart.");

            var shoppingCart = new ShoppingCartProducts
            {
                ShoppingCartId = tempCart.Id,
                ProductId = product.Id
            };

            await storeContext.ShoppingCartProducts.AddAsync(shoppingCart);
            await storeContext.SaveChangesAsync(); 

            return tempCart;
        }

        /// <summary>
        ///     Adds multiple products to the account's cart.
        /// </summary>
        /// <param name="product">Product Type</param>
        /// <param name="account">Account Type</param>
        /// <returns></returns>
        public async Task<ShoppingCart> AddToCart(IEnumerable<int> productsId, string accountId)
        {
            var account = await storeContext.Accounts.Where(acc => acc.Id == accountId).Include(acc => acc.ShoppingCart)
                .SingleAsync();
            var products =  await storeContext.Products.Where(prod => productsId.Contains(prod.Id)).ToListAsync();

            var tempCart = account.ShoppingCart;

            if (tempCart == null)
                throw new UserException($"User ({account.UserName}) doesn't have Shopping Cart.");

            foreach (var p in products)
                if (p != null)
                {
                    if ( await ProductExistsInCart(p, account))
                        throw new UserException($"Product {p.Name} already exists in the user's cart.");

                    var shoppingCart = new ShoppingCartProducts
                    {
                        ShoppingCartId = tempCart.Id,
                        ProductId = p.Id
                    };

                     await storeContext.ShoppingCartProducts.AddAsync(shoppingCart);
                    account.ShoppingCart.ShoppingCartProducts.Add(shoppingCart);
                }

            await storeContext.SaveChangesAsync();

            return account.ShoppingCart;
        }

        /// <summary>
        ///     Clears the user's cart.
        /// </summary>
        /// <param name="account">Account Type</param>
        public async Task ClearUserCart(string accountId)
        {
            var cart = await GetUserCart(accountId);

            var userCartProducts = cart.ShoppingCartProducts.ToList();
            foreach (var product in userCartProducts) storeContext.ShoppingCartProducts.Remove(product);
            await storeContext.SaveChangesAsync();

            // account.ShoppingCart.ShoppingCartProducts = new List<ShoppingCartProducts>();
        }

        /// <summary>
        ///     Gets the user cart.
        /// </summary>
        /// <returns>The user cart.</returns>
        /// <param name="account">Account Type</param>
        public async Task<ShoppingCart> GetUserCart(string accountId)
        {
            var account = await storeContext.Accounts.Where(acc => acc.Id == accountId).Include(acc => acc.ShoppingCart)
                .SingleAsync();
            return account.ShoppingCart;
        }

        /// <summary>
        ///     Checks if the product already exists in the user's cart.
        /// </summary>
        /// <param name="product">Product Type</param>
        /// <param name="account">Account Type</param>
        /// <returns></returns>
        private Task<bool> ProductExistsInCart(Product product, Account account)
        {
            return  storeContext.ShoppingCartProducts.AnyAsync(s =>
                s.ShoppingCartId == account.ShoppingCartId && s.ProductId == product.Id);
        }
    }
}