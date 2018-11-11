using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Tests.ProductServiceTests
{
    [TestClass]
    public class GetAllProducts_Should
    {
        [TestMethod]
        public async Task ReturnAll()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
               .UseInMemoryDatabase("ReturnAll").Options;
            var product1 = new Product
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };


            var product2 = new Product
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };


            using (var curContext = new GameStoreContext(options))
            {
                curContext.Products.Add(product1);
                curContext.Products.Add(product2);
                curContext.SaveChanges();
                //Act
                var sut = new ProductsService(curContext);
                var results = await sut.GetAllProducts();

                //Assert

                Assert.IsTrue(curContext.Products.Count() == results.Count());
            }
        }
    }
}
