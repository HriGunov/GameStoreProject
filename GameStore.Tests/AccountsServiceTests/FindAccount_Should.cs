using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Tests.AccountsServiceTests
{
    [TestClass]
    public class FindAccount_Should
    {
        [TestMethod]
        public void FindAccount_WhenInput_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>().UseInMemoryDatabase(databaseName: $"FindAccoun_WhenInput_IsValid").Options;

            var accountToBeFound = new Account() {
                Username = $"TestUsername",
                Password = "foo",
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

            //Act
            Account accountFound;
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new AccountsService(curContext);
                accountFound = sut.FindAccount(accountToBeFound.Username);
            }
            //Assert
            Assert.IsTrue(accountFound != null);
            Assert.IsTrue(accountFound.Username == accountToBeFound.Username);
        }
    }
}
