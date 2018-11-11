using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Tests.ProductServiceTests
{
    [TestClass]
    public class RemoveProducts_Should
    {
        [TestMethod]
        public async Task RemoveProductById_WhenId_IsValid()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
              .UseInMemoryDatabase("RemoveProductById_WhenId_IsValid").Options;
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
                var results = await sut.RemoveProductAsync(product1.Id);

                //Assert

                Assert.IsTrue(curContext.Products.Count() == 1);
                Assert.IsTrue(curContext.Products.First() == product2);

            }
        }

        [TestMethod]
        public async Task RemoveProduct_ByProduct()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
              .UseInMemoryDatabase("RemoveProduct_ByProduct").Options;
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
                var results = await sut.RemoveProductAsync(product1);

                //Assert

                Assert.IsTrue(curContext.Products.Count() == 1);
                Assert.IsTrue(curContext.Products.First() == product2);

            }
        }
    }
}
