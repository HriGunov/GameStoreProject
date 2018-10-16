using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Tests.ProductServiceTests
{
    [TestClass]
    public class AddProduct_Should
    { 
        [TestMethod]
        public void AddProduct_WhenInput_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>().UseInMemoryDatabase(databaseName: $"AddProduct_WhenInput_IsValid").Options;

            var productToAdd = new Product()
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };
            //Act
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new ProductsService(curContext);
                curContext.Products.Add(productToAdd);
                curContext.SaveChanges();
            }


            //Assert
            using (var curContext = new GameStoreContext(options))
            {
                Assert.IsTrue(curContext.Products.Count() == 1);
            }          
        }

        [TestMethod]
        [ExpectedException(typeof(UserException))]
        public void ThrorwException_WhenInput_AlreadyExists()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>().UseInMemoryDatabase(databaseName: $"ThrorwException_WhenInput_AlreadyExists").Options;

            var productToAdd = new Product()
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };
            //Act
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new ProductsService(curContext);
                curContext.Products.Add(productToAdd);

                curContext.SaveChanges();
            }
            //Act
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new ProductsService(curContext);
                sut.AddProduct(productToAdd);
               
            }
 
        } 
    }

}