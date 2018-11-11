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
    public class GetCommentsFromProduct_Should
    {
        [TestMethod]
        public async Task ReturnComments_WhenProductID_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("ReturnComments_WhenProductID_IsValid").Options;

        

            var product = new Product
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };

            var commentToAdd = new Comment
            {
                Account = new Account(),                 
                Text = "TestDescription",
            };

            using (var curContext = new GameStoreContext(options))
            {
                curContext.Products.Add(product);
                commentToAdd.ProductId = product.Id;
                curContext.Comments.Add(commentToAdd);
                curContext.SaveChanges();
                //Act
                var sut = new CommentService(curContext);
                var foo = await sut.GetCommentsFromProductAsync(product.Id);

                //Assert

                 
                Assert.IsTrue(foo.First() == commentToAdd);
            }
        }
    }
}
