using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IGameStoreContext storeContext;

        public ProductsService(IGameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        /// <summary>
        ///     Creates a product from the given parameters and adds it to the database.
        /// </summary>
        /// <param name="productName">Product Name</param>
        /// <param name="productDescription">Product Description</param>
        /// <param name="productPrice">Product Price</param>
        /// <param name="productGenres">Product Genres (ICollection)</param>
        /// <returns></returns>
        public Product AddProduct(string productName, string productDescription, decimal productPrice,
            ICollection<Genre> productGenres = null)
        {
            Product product;

            if (productGenres == null)
                product = new Product
                {
                    Name = productName,
                    Description = productDescription,
                    Price = productPrice,
                    CreatedOn = DateTime.Now
                };
            else
                product = new Product
                {
                    Name = productName,
                    Description = productDescription,
                    Price = productPrice,
                    Genre = productGenres,
                    CreatedOn = DateTime.Now
                };

            storeContext.Products.Add(product);
            storeContext.SaveChanges();

            return product;
        }

        /// <summary>
        ///     Removes the first product that matches the given name parameter in the database.
        /// </summary>
        /// <param name="productName">Product Name</param>
        /// <returns></returns>
        public string RemoveProduct(string productName)
        {
            var product = storeContext.Products.ToList().FirstOrDefault(p => p.Name == productName);
            if (product == null || product.IsDeleted) return $"Product {productName} was not found.";

            product.IsDeleted = true;
            storeContext.SaveChanges();
            return $"Product {productName} has been successfully removed.";
        }

        /// <summary>
        ///     Finds the product in the database that matches the given productName in the parameters and returns it as Product
        ///     type.
        /// </summary>
        /// <param name="productName">Product Name</param>
        /// <returns></returns>
        public Product FindProduct(string productName)
        {
            var product = storeContext.Products.ToList().FirstOrDefault(p => p.Name == productName);
            if (product == null || product.IsDeleted) return null;

            return product;
        }

        /// <summary>
        ///     Finds the products in the database that match the given productName in the parameters and returns them as Product
        ///     type in a collection.
        /// </summary>
        /// <param name="productName">Product Name</param>
        /// <returns></returns>
        public IEnumerable<Product> FindProducts(string productName)
        {
            var products = storeContext.Products.ToList().Where(p => p.Name == productName).ToList();
            return !products.Any() ? null : products;
        }
    }
}