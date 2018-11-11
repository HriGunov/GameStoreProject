using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.CommentServiceTests
{
    [TestClass]
    public class AddCommentShould
    {
        [TestMethod]
        public async Task AddComment_WhenInput_IsValid()
        {
             
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("AddComment_WhenInput_IsValid").Options;

            var commentToAdd = new Comment
            {
                Account = new Account(),
                Product = new Product(),
                Text = "TestDescription",
                TimeStamp = DateTime.Now
            };

            var validUser = new Account
            {

                UserName = $"TestUsername",
                FirstName = "FirstName",
                LastName = "LastName"

            };

            var product = new Product
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };

            using (var curContext = new GameStoreContext(options))
            { 
                curContext.Products.Add(product);
                curContext.Accounts.Add(validUser);
                curContext.SaveChanges();
                //Act
                var sut = new CommentService(curContext);
                var foo = await sut.AddCommentToProductAsync(product.Id, validUser.Id, "TestDescription");
           
            //Assert
            
                Assert.IsTrue(curContext.Comments.Count() == 1);
                Assert.IsTrue(curContext.Comments.FirstOrDefault().Text == "TestDescription");
                Assert.IsTrue(curContext.Comments.Include(c => c.Account).FirstOrDefault().Account.Id ==
                              validUser.Id);
            } 
        }
    }
}