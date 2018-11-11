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
   public class RemoveCommentsFromProduct_Should
    {
        [TestMethod]
        public async Task RemoveComments_WhenProductID_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("RemoveComments_WhenProductID_IsValid").Options;



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
                product.Comments.Add(commentToAdd);

                commentToAdd.ProductId = product.Id;
                commentToAdd.Product = product;



                curContext.Comments.Add(commentToAdd);
                curContext.SaveChanges();
                //Act
                var sut = new CommentService(curContext);
                await sut.RemoveCommentsFromProductAsync(product.Id);

                //Assert


                Assert.IsTrue( product.Comments.Any());

                Assert.IsTrue(!product.Comments.Any(comment => comment.IsDeleted == false));
            }
        }
    }
}
