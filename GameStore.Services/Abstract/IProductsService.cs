using System.Collections.Generic;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IProductsService
    {
        Product AddProduct(string productName, string productDescription, decimal productPrice,
            ICollection<Genre> productGenres = null);

        string RemoveProduct(string productName);
        Product FindProduct(string productName, bool isDeleted = false);
        IEnumerable<Product> FindProductsByGenre(IEnumerable<Genre> productGenre);
        IEnumerable<Product> FindProductsByGenre(Genre productGenre);
        IEnumerable<Product> GetProducts();
        void LoadProductsLoadedFromJSON(string jsonString);
        void DeleteProductsLoadedFromJSON(string jsonString);
        string AddGenreToProduct(string name, Product product);
        string RemoveGenreFromProduct(string name, Product product);
    }
}