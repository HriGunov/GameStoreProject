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
    public class RemoveCommentShould
    {
        [TestMethod]
        public async Task RemoveComments_WhenProductID_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("RemoveComments_WhenProductID_IsValid").Options;


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
                await sut.RemoveComment(commentToAdd.Id);

                //Assert

                Assert.IsTrue(!curContext.Comments.Any(comment => comment.IsDeleted == false));

            }
        }
    }
}
