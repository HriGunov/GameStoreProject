using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IProductsService
    {
        Task<Product> AddGenreToProductAsync(string name, Product product);
        Task<Product> AddProductAsync(Product product);
        Task<Product> FindProductAsync(int id, bool includeDeleted = false);
        Task<Product> FindProductAsync(string productName, bool includeDeleted = false);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> FindProductsByGenreAsync(Genre productGenre);
        Task<IEnumerable<Product>> FindProductsByGenreAsync(IEnumerable<Genre> productGenre);
        Task<Product> RemoveGenreFromProductAsync(string name, Product product);
        Task<string> RemoveProductAsync(int id);
        Task<string> RemoveProductAsync(Product product);
        Task<IEnumerable<Product>> SkipAndTakeLatestProductsAsync(int productsToTake, Expression<Func<Product, bool>> filter = null);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> ProductExistsAsync(int id);
        Task SaveProductImageAsync(string root, string filename, Stream stream, int productId);
    }
}