using System.Collections.Generic;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IProductsService
    {
        Product AddProduct(string productName, string productDescription, decimal productPrice,
            ICollection<Genre> productGenres = null, ICollection<Comment> productComments = null);

        string RemoveProduct(string productName);
        Product FindProduct(string productName);
        IEnumerable<Product> FindProductsByGenre(IEnumerable<Genre> productGenre);
    }
}