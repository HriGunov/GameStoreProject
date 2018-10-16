using System.Collections.Generic;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.ProductServiceTests
{
    [TestClass]
    public class FindProductShould
    {
        [TestMethod]
        public void FindProduct_WhenInput_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("FindProduct_WhenInput_IsValid").Options;

            var productToAdd = new Product
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new ProductsService(curContext);
                curContext.Products.Add(productToAdd);
                curContext.SaveChanges();
            }

            //Act
            Product productFound;
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new ProductsService(curContext);
                productFound = sut.FindProduct(productToAdd.Name);
            }

            //Assert
            Assert.IsTrue(productToAdd.Name == productFound.Name);
        }
    }
}