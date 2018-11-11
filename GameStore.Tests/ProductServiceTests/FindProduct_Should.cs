using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Tests.ProductServiceTests
{
    [TestClass]
    public class FindProduct_Should
    {
        [TestMethod]
        public async Task FindProduct_PassedCorrect_Id()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
               .UseInMemoryDatabase("FindProduct_PassedCorrect_Id").Options;
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
                var results = await sut.FindProductAsync(product2.Id);

                //Assert

                Assert.IsTrue(results == product2);
            }
        }

        [TestMethod]
        public async Task FindProduct_PassedCorrect_Name()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
               .UseInMemoryDatabase("FindProduct_PassedCorrect_Name").Options;
            var product1 = new Product
            {
                Name = "Test1",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };


            var product2 = new Product
            {
                Name = "Test2",
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
                var results = await sut.FindProductAsync(product2.Name);

                //Assert

                Assert.IsTrue(results == product2);
            }
        }
    }
}
