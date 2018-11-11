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
    public class RemoveCommentsFromAccount_Should
    {
        [TestMethod]
        public async Task RemoveComments_WhenAccountID_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("RemoveComments_WhenAccountID_IsValid").Options;



            var validUser = new Account
            {

                UserName = $"TestUsername",
                FirstName = "FirstName",
                LastName = "LastName"

            };

            var commentToAdd = new Comment
            {

                Account = new Account(),
                Text = "TestDescription",
            };

            using (var curContext = new GameStoreContext(options))
            {
                curContext.Accounts.Add(validUser);
                validUser.Comments.Add(commentToAdd);

                commentToAdd.AccountId = validUser.Id;
                commentToAdd.Account = validUser;



                curContext.Comments.Add(commentToAdd);
                curContext.SaveChanges();
                //Act
                var sut = new CommentService(curContext);
                await sut.RemoveCommentsFromAccountAsync(validUser.Id);

                //Assert


                Assert.IsTrue(validUser.Comments.Any());
                Assert.IsTrue(!validUser.Comments.Any(comment => comment.IsDeleted == false));

            }
        }
    }
}
