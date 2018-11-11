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

namespace GameStore.Tests.CommentServiceTests
{
    [TestClass]
    public class GetCommentsFromAccount_Should
    {

        [TestMethod]
        public async Task ReturnComments_WhenAccountId_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("ReturnComments_WhenAccountId_IsValid").Options;



            var validUser = new Account
            {

                UserName = $"TestUsername",
                FirstName = "FirstName",
                LastName = "LastName"

            };
            var commentToAdd = new Comment
            {
                 
                Text = "TestDescription",
            };

            using (var curContext = new GameStoreContext(options))
            {
                curContext.Accounts.Add(validUser);
                commentToAdd.AccountId = validUser.Id;
                curContext.Comments.Add(commentToAdd);
                curContext.SaveChanges();
                //Act
                var sut = new CommentService(curContext);
                var result = await sut.GetCommentsByUserAsync(validUser.Id);

                //Assert 
                Assert.IsTrue(result.First() == commentToAdd);
            }
        }
    }
}
