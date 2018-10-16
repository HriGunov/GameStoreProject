using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.AccountsServiceTests
{
    [TestClass]
    public class AddAccount_Should
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(11)]
        [DataRow(111)]
        public void AddAccount_WhenInput_IsValid(int numberOfAccountsToAdd)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase($"AddAccount_WhenInput_IsValid{numberOfAccountsToAdd}").Options;


            //Act
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new AccountsService(curContext);
                for (var i = 1; i <= numberOfAccountsToAdd; i++)
                {
                    var validUser = new Account
                    {
                        Username = $"TestUsername{i}", Password = "foo", FirstName = "FirstName", LastName = "LastName"
                    };


                    sut.AddAccount(validUser);
                }
            }

            using (var curContext = new GameStoreContext(options))
            {
                Assert.IsTrue(curContext.Accounts.Count() == numberOfAccountsToAdd);
            }
        }

        [TestMethod]
        [DataRow("!@#$%&._")]
        [DataRow("Test 123")]
        [DataRow(" 1223423 ")]
        [DataRow("testing._")]
        [DataRow("testing.")]
        [DataRow("13!@")]
        [ExpectedException(typeof(UserException))]
        public void ThrowException_WhenUsername_IsInvalid(string name)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase($"ThrowException_WhenUsername_IsInvalid->{name}").Options;


            //Act
            using (var curContext = new GameStoreContext(options))
            {
                var sut = new AccountsService(curContext);

                var validUser = new Account
                    {Username = name, Password = "foo", FirstName = "FirstName", LastName = "LastName"};

                sut.AddAccount(validUser);
            }
        }
    }
}