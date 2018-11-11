using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Tests.ShoppingCartsServiceTests
{
    [TestClass]
    public class GetUserCartShould
    {
        [TestMethod]
        public async Task ReturnCart_AccountId_IsValid()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("ReturnCart_AccountId_IsValid").Options;

             

            var validUser = new Account
            {

                UserName = $"TestUsername",
                FirstName = "FirstName",
                LastName = "LastName"
                
            };
            var cart = new ShoppingCart
            {
                ShoppingCartProducts = new List<ShoppingCartProducts>()
            };

            var cartProd = new ShoppingCartProducts()
            { };

            var product = new Product
            {
                Name = "Test",
                Description = "test description",
                ShoppingCartProducts = new List<ShoppingCartProducts>(),
                Comments = new List<Comment>()
            };

            
            using (var curContext = new GameStoreContext(options))
            {
                curContext.Accounts.Add(validUser);
                curContext.ShoppingCarts.Add(cart);
                validUser.ShoppingCart = cart;
                cartProd.Product = product;
                cartProd.ShoppingCart = cart;
                cart.ShoppingCartProducts.Add(cartProd);
                curContext.SaveChanges();

                //Act
                var sut = new ShoppingCartsService(curContext);
                var result = await sut.GetUserCartAsync(validUser.Id);

                Assert.AreEqual(cart, result);

            }
 
        }
     
    }
}
