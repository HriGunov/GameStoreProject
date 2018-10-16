﻿using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.Tests.CommentServiceTests
{
    [TestClass]
    public class AddCommentShould
    {
        [TestMethod]
        public void AddComment_WhenInput_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>().UseInMemoryDatabase(databaseName: $"AddComment_WhenInput_IsValid").Options;

            var commentToAdd = new Comment()
            {
                Account = new Account(),
                Product = new Product(),
                Text = "TestDescriptin",
                TimeStamp = DateTime.Now

            };

            var productToBeReturnedByMock= new Product()
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };

            var accountToBeReturnedByMock = new Account()
            {
                Username = $"TestUsername",
                Password = "TestPassword",
                FirstName = "FirstName",
                LastName = "LastName",
                CreatedOn = DateTime.Now,
                ShoppingCart = new ShoppingCart(),
                OrderProducts = new List<Order>(),
                Comments = new List<Comment>(),
                IsAdmin = true
            };
            var mockAccountsService = new Mock<IAccountsService>();
            mockAccountsService.Setup(service => service.FindAccount(It.IsAny<string>())).Returns(accountToBeReturnedByMock);

            var mockProductsService = new Mock<IProductsService>();
            mockProductsService.Setup(service => service.FindProduct(It.IsAny<string>())).Returns(productToBeReturnedByMock);

            //Act
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new CommentService(curContext, mockAccountsService.Object, mockProductsService.Object);
                sut.AddCommentToProduct(productToBeReturnedByMock.Name, accountToBeReturnedByMock.Username, "TestDescriptins");
                curContext.SaveChanges();
            }


            //Assert
            using (var curContext = new GameStoreContext(options))
            {
                Assert.IsTrue(curContext.Comments.Count() == 1);
                Assert.IsTrue(curContext.Comments.FirstOrDefault().Text == "TestDescriptins");
                Assert.IsTrue(curContext.Comments.Include(c => c.Account).FirstOrDefault().Account.Username == accountToBeReturnedByMock.Username);


            }
        }
    }
}
