using System;
using System.Collections.Generic;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.AuthenticationServiceTests
{
    [TestClass]
    public class Authenticate_Should
    {
        [TestMethod]
        public void ReturnAccount_When_InputIsValid()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("ReturnAccount_When_InputIsValid").Options;

            var accountToBeFound = new Account
            {
                UserName = "TestUsername",
                PasswordHash = "TestPassword",
                FirstName = "FirstName",
                LastName = "LastName",
                CreatedOn = DateTime.Now,
                ShoppingCart = new ShoppingCart(),
                OrderProducts = new List<Order>(),
                Comments = new List<Comment>(),
                IsAdmin = true
            };

            using (var curContext = new GameStoreContext(options))
            {
                curContext.Accounts.Add(accountToBeFound);
                curContext.SaveChanges();
            }

            var mockAccountsService = new Mock<IAccountsService>();
            mockAccountsService.Setup(service => service.FindAccount(It.IsAny<string>(), true))
                .Returns(accountToBeFound);

            Account accFound;
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new AuthenticationService(mockAccountsService.Object);
                accFound = sut.Authenticate(accountToBeFound.UserName, accountToBeFound.PasswordHash);
            }

            Assert.IsTrue(accFound.UserName == accountToBeFound.UserName);
        }

        [TestMethod]
        public void ReturnNull_When_PasswordsDontMatch()
        {
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("ReturnNull_When_PasswordsDontMatch").Options;

            var accountToBeFound = new Account
            {
                UserName = "TestUsername",
                PasswordHash = "TestPassword",
                FirstName = "FirstName",
                LastName = "LastName",
                CreatedOn = DateTime.Now,
                ShoppingCart = new ShoppingCart(),
                OrderProducts = new List<Order>(),
                Comments = new List<Comment>(),
                IsAdmin = true
            };

            using (var curContext = new GameStoreContext(options))
            {
                curContext.Accounts.Add(accountToBeFound);
                curContext.SaveChanges();
            }

            var mockAccountsService = new Mock<IAccountsService>();
            mockAccountsService.Setup(service => service.FindAccount(It.IsAny<string>(), true))
                .Returns(accountToBeFound);

            Account accFound;
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new AuthenticationService(mockAccountsService.Object);
                accFound = sut.Authenticate(accountToBeFound.UserName, "Some Password");
            }

            Assert.IsTrue(accFound == null);
        }
    }
}