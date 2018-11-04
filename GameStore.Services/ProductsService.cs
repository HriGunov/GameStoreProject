using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        ///     Removes the first product that matches the given id parameter in the database.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        public async Task<string> RemoveProductAsync(int id)
        {
            var product = await this.FindProductAsync(id);
            var productName = product.Name;
            if (product.IsDeleted) return $"Product {product.Name} was not found.";

            storeContext.Products.Remove(product);
            await storeContext.SaveChangesAsync();
            return $"Product {productName} has been successfully removed.";
        }

        public async Task<string> RemoveProductAsync(Product product)
        {
            var productName = product.Name;
            if (product.IsDeleted) return $"Product {product.Name} was not found.";

            storeContext.Products.Remove(product);
            await storeContext.SaveChangesAsync();
            return $"Product {productName} has been successfully removed.";
        }

        /// <summary>
        ///     Finds the product in the database that matches the given productName in the parameters and returns it as Product
        ///     type.
        /// </summary>
        /// <param name="productName">Product Name</param>
        /// <param name="includeDeleted">Is Deleted</param>
        /// <returns></returns>
        public async Task<Product> FindProductAsync(string productName, bool includeDeleted = false)
        {
            var product = await storeContext.Products.FirstOrDefaultAsync(p => p.Name == productName);

            if (product == null) return null;

            if (includeDeleted) return product;

            if (product.IsDeleted)
                return null;

            return product;
        }

        public async Task<Product> FindProductAsync(int id, bool includeDeleted = false)
        {
            var product = await storeContext.Products.FindAsync(id);

            if (product == null) return null;

            if (includeDeleted) return product;

            if (product.IsDeleted)
                return null;

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await storeContext.Products.ToListAsync();
            return !products.Any() ? null : products;
        }

        public async Task<IEnumerable<Product>> SkipAndTakeLatestProductsAsync(int productsToTake)
        {
            var products = await storeContext.Products.Include(g => g.Genre).OrderByDescending(product => product.CreatedOn)
                .Take(productsToTake).ToListAsync();

            return !products.Any() ? null : products;
        }

        public async Task<IEnumerable<Product>> FindProductsByGenreAsync(IEnumerable<Genre> productGenre)
        {
            var products = await storeContext.Products.Include(prod => prod.Genre)
                .Where(p => productGenre
                    .All(genre => p.Genre.Contains(genre))).ToListAsync();

            return !products.Any() ? null : products;
        }

        public async Task<Product> AddGenreToProductAsync(string name, Product product)
        {
            if (product.Genre.Any(g => g.Name == name))
                throw new UserException($"The {product.Name} already has this genre ({name}).");

            await storeContext.Genres.AddAsync(new Genre {Name = name, ProductId = product.Id});
            await storeContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> RemoveGenreFromProductAsync(string name, Product product)
        {
            var tempGenre = await storeContext.Genres.FirstOrDefaultAsync(g => g.Name == name && g.ProductId == product.Id);
            if (tempGenre == null)
                throw new UserException($"Product {product.Name} doesn't have {name} genre.");

            storeContext.Genres.Remove(tempGenre);
            await storeContext.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> FindProductsByGenreAsync(Genre productGenre)
        {
            var products = await storeContext.Products.Include(prod => prod.Genre).Where(p => p.Genre.Contains(productGenre))
                .ToListAsync();

            return !products.Any() ? null : products;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await this.storeContext.Products.AddAsync(product);
            await storeContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            this.storeContext.Update(product);
            await this.storeContext.SaveChangesAsync();
            return product;
        }
        public async Task<bool> ProductExistsAsync(int id)
        {
            return await this.storeContext.Products.AnyAsync(e => e.Id == id);
        }

        public async Task SaveProductImageAsync(string root, string filename, Stream stream, int productId)
        {
            var product = await this.FindProductAsync(productId);
            if (product == null) throw new Exception("Product Not Found");

            var imageName = Guid.NewGuid() + Path.GetExtension(filename);
            var path = Path.Combine(root, imageName);

            using (var fileStream = File.Create(path))
            {
                await stream.CopyToAsync(fileStream);
            }

            product.ProductImageName = imageName;
            storeContext.SaveChanges();
        }
    }
}