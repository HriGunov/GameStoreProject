using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

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
            ICollection<Genre> productGenres = null, ICollection<Comment> productComments = null)
        {
            Product product;

            product = new Product
            {
                Name = productName,
                Description = productDescription,
                Price = productPrice,
                CreatedOn = DateTime.Now
            };

            if (productGenres != null)
                product.Genre = productGenres;

            if (productComments != null)
                product.Comments = productComments;

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
            var product = storeContext.Products.ToList().SingleOrDefault(p => p.Name == productName);
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
            var product = storeContext.Products
                                       .Include(c => c.Comments)
                                       .Include(g => g.Genre)
                                       .ToList()
                                       .SingleOrDefault(p => p.Name == productName);

            return product == null || product.IsDeleted ? null : product;
        }

        public IEnumerable<Product> FindProductsByGenre(Genre productGenre)
        {
            var products = storeContext.Products
                                       .Include(c => c.Comments)
                                       .Include(g => g.Genre)
                                       .ToList()
                                       .Where(p => p.Genre.Contains(productGenre));

            return !products.Any() ? null : products;
        }

        public IEnumerable<Product> FindProductsByGenre(IEnumerable<Genre> productGenre)
        {
            var products = storeContext.Products
                                       .Include(c => c.Comments)
                                       .Include(g => g.Genre)
                                       .ToList()
                                       .Where(p =>
            {
                foreach (var genre in productGenre)
                {
                    if (!p.Genre.Contains(genre))
                        return false;
                }
                return true;
            });

            return !products.Any() ? null : products;
        }
    }
}