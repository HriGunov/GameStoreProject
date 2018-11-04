using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GameStore.Services
{
    public class ProductsService : IProductsService
    {
        private readonly GameStoreContext storeContext;

        public ProductsService(GameStoreContext storeContext)
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
        public Product AddProduct(string productName,string imageName, string productDescription, decimal productPrice,
            ICollection<Genre> productGenres = null)
        {
            if (FindProduct(productName) != null)
                throw new UserException($"Product ({productName}) already exists.");

            var product = new Product
            {
                Name = productName,
                ProductImageName = imageName,
                Description = productDescription,
                Price = productPrice,
                CreatedOn = DateTime.Now
            };

            if (productGenres != null)
                product.Genre = productGenres;

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
        public Product FindProduct(string productName, bool includeDeleted = false)
        {
            var product = storeContext.Products.FirstOrDefault(p => p.Name == productName);

            if (product == null) return null;

            if (includeDeleted) return product;

            if (product.IsDeleted)
                return null;
            return product;
        }

        public Product FindProduct(int id, bool includeDeleted = false)
        {
            var product = storeContext.Products.Find(id);

            if (product == null) return null;

            if (includeDeleted) return product;

            if (product.IsDeleted)
                return null;
            return product;
        }
        public IEnumerable<Product> SkipAndTakeLatestProducts(int productsToSkip,int productsToTake)
        {
            return this.storeContext.Products.Include(g => g.Genre).FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
        }

        public IEnumerable<Product> SkipAndTakeLatestProducts(int productsToTake)
        {
            return storeContext.Products.Include(g => g.Genre).OrderByDescending(product => product.CreatedOn).Take(productsToTake).ToList();
        }

        public IEnumerable<Product> FindProductsByGenre(IEnumerable<Genre> productGenre)
        {
            var products = storeContext.Products.Include( prod => prod.Genre)
                .Where(p =>productGenre
                    .All(genre => p.Genre.Contains(genre)));
            

            return !products.Any() ? null : products;
        } 
        public string AddGenreToProduct(string name, Product product)
        {
            if (FindProduct(product.Name).Genre.Any(g => g.Name == name))
                throw new UserException($"The {product.Name} already has this genre ({name}).");

            storeContext.Genres.Add(new Genre { Name = name, ProductId = product.Id });
            storeContext.SaveChanges();

            return $"Added {name} to {product.Name}.";
        }

        public string RemoveGenreFromProduct(string name, Product product)
        {
            var tempGenre = storeContext.Genres.FirstOrDefault(g => g.Name == name && g.ProductId == product.Id);
            if (tempGenre == null)
                return $"Product {product.Name} doesn't have {name} genre.";

            storeContext.Genres.Remove(tempGenre);
            storeContext.SaveChanges();

            return $"Removed {name} from {product.Name}.";
        }

        public IEnumerable<Product> FindProductsByGenre(Genre productGenre)
        {
            var products = storeContext.Products.Include(prod => prod.Genre).Where(p => p.Genre.Contains(productGenre)).ToList();

            return !products.Any() ? null : products;
        }

        public void LoadProductsLoadedFromJSON(string jsonString)
        {
            List<Product> results = JsonConvert.DeserializeObject<List<Product>>(jsonString);

            foreach (var product in results)
            {
                var collision = FindProduct(product.Name, true);
                if (collision == null)
                {
                    AddProduct(product);
                }
                else
                {
                    if (collision.IsDeleted) collision.IsDeleted = false;
                }
            }

            storeContext.SaveChanges();
        }

        public void DeleteProductsLoadedFromJSON(string jsonString)
        {
            List<Product> results = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            foreach (var product in results)
            {
                var collision = FindProduct(product.Name);
                if (collision != null)
                    if (collision.IsDeleted == false)
                        collision.IsDeleted = true;
            }

            storeContext.SaveChanges();
        }


        public Product AddProduct(Product product)
        {
            return AddProduct(product.Name, product.ProductImageName, product.Description, product.Price, product.Genre);
        }

        
    }
}