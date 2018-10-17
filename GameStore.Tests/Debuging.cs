using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;
using GameStore.Data.Context;
using GameStore.Services;
using System.Linq;

namespace GameStore.Tests
{
    [TestClass]
    public class Debuging
    {
        [TestMethod]
        public void Run()
        {
            var testJson = File.ReadAllText(@"C:\Users\Laptopa\Documents\Hristo Gunov\Projects\GitLab\gamestoreteamproject\GameStore.Data\NewProducts.json");
            var options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase("Run").Options;

           
            using (var context = new GameStoreContext(options))
            {
                var productService = new ProductsService(context);
                
                var loadedProducts = loader.LoadProducts(testJson);

                foreach (var product in loadedProducts)
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                
            }
            using (var context = new GameStoreContext(options))
            {
                Assert.IsTrue(context.Products.Count() == 6);
            }
        }
    }
}
