using System;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.AccountsServiceTests
{
    [TestClass]
    public class DeleteAccount_Should
    {
        [TestMethod]        
        public async  Task Work_WhenAccountId_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase($"Work_WhenInput_IsValid").Options;

            var validUser = new Account
            {
                UserName = $"TestUsername ",
                FirstName = "FirstName",
                LastName = "LastName"
            };
            //Act
            using (var curContext = new GameStoreContext(options))
            { 
                   

                curContext.Accounts.Add(validUser);

                var sut = new AccountsService(curContext);

                await sut.DeleteAccountAsync(validUser.Id);

                Assert.IsTrue(curContext.Accounts.Any(acc => acc.IsDeleted == true));
            }

           
        }

        [TestMethod]
        public async Task DoNothing_WhenAccount_IsDeleted()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase($"DoNothing_WhenAccount_IsDeleted").Options;


            var validUser = new Account
            {
                IsDeleted = true,
                UserName = $"TestUsername ",
                FirstName = "FirstName",
                LastName = "LastName"

            };
            //Act
            using (var curContext = new GameStoreContext(options))
            {

                curContext.Accounts.Add(validUser);

                var sut = new AccountsService(curContext);

                await sut.DeleteAccountAsync(validUser.Id);

                Assert.IsTrue(curContext.Accounts.Any(acc => acc.IsDeleted == true));
            }

        }
        [TestMethod]
        public async Task SetDeletedOn_WhenAccount_IsDeleted()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase($"SetDeletedOn_WhenAccount_IsDeleted").Options;

            var validUser = new Account
            {
                 
                UserName = $"TestUsername ",
                FirstName = "FirstName",
                LastName = "LastName" 

            };
           
            using (var curContext = new GameStoreContext(options))
            {
                curContext.Accounts.Add(validUser);

                var sut = new AccountsService(curContext);

                await sut.DeleteAccountAsync(validUser.Id);

                Assert.IsTrue(curContext.Accounts.Any(acc => acc.DeletedOn != null));
            }
        }
    }
}