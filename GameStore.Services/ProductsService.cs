using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            if (FindProduct(productName) != null)
                throw new UserException($"Product ({productName}) already exists.");

            var product = new Product
            {
                   
                Name = productName,
                Description = productDescription,
                Price = productPrice,
                CreatedOn = DateTime.Now
            };
            if (product.Name.Length > 20)
            {
                product.Name = product.Name.Substring(0, 20);
            }

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
        public Product FindProduct(string productName,bool includeDeleted = false)
        {
            var product = GetProducts().SingleOrDefault(p => p.Name == productName);

            if (product == null)
            {
                return null;
            }
            else
            {
                if (includeDeleted)
                {
                    return product;
                }
                else
                {
                    if (product.IsDeleted)
                    {
                        return null;
                    }
                    else
                    {
                        return product;
                    }
                }
            }
        }

        public IEnumerable<Product> FindProductsByGenre(IEnumerable<Genre> productGenre)
        {

            var products = GetProducts().Where(p => { return productGenre.All(genre => p.Genre.Contains(genre)); });

            return !products.Any() ? null : products;
        }

        public IEnumerable<Product> GetProducts()
        {
             
            return storeContext.Products               
                .Include(s => s.ShoppingCartProducts)
                .ThenInclude(cart => cart.Product)
                .Include(s => s.ShoppingCartProducts)
                .ThenInclude(cart => cart.ShoppingCart)
                .Include(c => c.Comments)
                .ThenInclude(comment => comment.Account)
                .Include(c => c.Comments)
                .ThenInclude(comment => comment.Product)
                .Include(g => g.Genre)
                .ThenInclude(p => p.Product)
                .ToList();
        }

        public string AddGenreToProduct(string name, Product product)
        {
            if (FindProduct(product.Name).Genre.Any(g => g.Name == name))
                throw new UserException($"The {product.Name} already has this genre ({name}).");

            storeContext.Genres.Add(new Genre {Name = name, ProductId = product.Id});
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
            var products = GetProducts().Where(p => p.Genre.Contains(productGenre));

            return !products.Any() ? null : products;
        }

        public void LoadProductsLoadedFromJSON(string jsonString)
        {
            List<Product> results = JsonConvert.DeserializeObject<List<Product>>(jsonString);

            foreach (var product in results)
            {
                var colision = FindProduct(product.Name, true);
                if (colision == null)
                {
                    AddProduct(product);
                }
                else
                {
                    if (colision.IsDeleted == true)
                    {
                        colision.IsDeleted = false;                        
                    }
                }
            }
            storeContext.SaveChanges();

        }

        public void DeleteProductsLoadedFromJSON(string jsonString)
        {
            List<Product> results = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            foreach (var product in results)
            {
                var colision = FindProduct(product.Name);
                if (colision != null)
                {
                    if (colision.IsDeleted == false)
                    {
                        colision.IsDeleted = true;                        
                    }
                }
            }
            storeContext.SaveChanges();
        }



        public Product AddProduct(Product product)
        {
            return AddProduct(product.Name, product.Description, product.Price, product.Genre);
        }
    }
}