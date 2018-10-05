using System;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly IGameStoreContext gameStoreContext;

        public Engine(IGameStoreContext gameStoreContext)
        {
            this.gameStoreContext = gameStoreContext;
        }

        public void Run()
        {
            
        }

        public void AddCommentToProduct(Comment comment, Product product)
        {
            product.Comments.Add(comment);
            gameStoreContext.SaveChanges();
        }

        public void AddProductToCart(ShoppingCart cartToAddTo, Product productToAdd)
        {
            cartToAddTo.Products.Add(productToAdd);
            gameStoreContext.SaveChanges();
        }

        public void AddOrder(Account account, ShoppingCart cart)
        {
            throw new NotImplementedException();
        }
    }
}