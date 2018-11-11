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
    public class ProductExists_Should
    {
        [TestMethod]
        public async Task Return_True_IfItExists()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("Return_True_IfItExists").Options;

            var product = new Product
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };

            //Act
            using (var curContext = new GameStoreContext(options))
            { 
                curContext.Products.Add(product);
                curContext.SaveChanges();
                var sut = new ProductsService(curContext);
                var result = await sut.ProductExistsAsync(product.Id);
                Assert.IsTrue(result);
            }
        }
    }
}
