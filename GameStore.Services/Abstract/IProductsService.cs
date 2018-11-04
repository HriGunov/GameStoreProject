using System.Collections.Generic;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IProductsService
    {
        string AddGenreToProduct(string name, Product product);
        Product AddProduct(Product product);

        Product AddProduct(string productName, string imageName, string productDescription, decimal productPrice,
            ICollection<Genre> productGenres = null);

        void DeleteProductsLoadedFromJSON(string jsonString);
        Product FindProduct(int id, bool includeDeleted = false);
        Product FindProduct(string productName, bool includeDeleted = false);
        IEnumerable<Product> FindProductsByGenre(Genre productGenre);
        IEnumerable<Product> FindProductsByGenre(IEnumerable<Genre> productGenre);
        void LoadProductsLoadedFromJSON(string jsonString);
        string RemoveGenreFromProduct(string name, Product product);
        string RemoveProduct(string productName);
        IEnumerable<Product> SkipAndTakeLatestProducts(int productsToTake);
    }
}