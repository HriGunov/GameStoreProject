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
    public class FindProductsByName_Should
    {
        [TestMethod]
        public async Task FindProduct_AlwaysWhenPredicate_IsTrue()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
               .UseInMemoryDatabase("FindProduct_PassedCorrect_Name").Options;
            var product1 = new Product
            {
                Name = "Banana",
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
                var results = await sut.FindProductsByName(product => true);

                //Assert

                Assert.IsTrue(results.Count() == 2);
            }
        }

        [TestMethod]
        public async Task ReturnNull_Nothing_IsFound()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
               .UseInMemoryDatabase("NotFindProduct_WhenPredicate_IsFalse").Options;
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
                var results = await sut.FindProductsByName(product => false);

                //Assert

                Assert.IsNull(results);
            }
        }
        [TestMethod]
        public async Task FindProduct_GivenNameMatching_Predicate()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
               .UseInMemoryDatabase("NotFindProduct_WhenPredicate_IsFalse").Options;
            var product1 = new Product
            {
                Name = "Banana",
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
                var results = await sut.FindProductsByName(product => product.Name.Contains("nana"));

                //Assert

                Assert.IsTrue(results.Count() == 1);
                Assert.IsTrue(results.First() == product1);

            }
        }
    }
}
