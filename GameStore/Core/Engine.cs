using GameStore.Data.Context;
using GameStore.Data.Models;
using System;
using System.Linq;

namespace GameStore.Core
{
    public class Engine
    {
        private readonly IGameStoreContext gameStoreContext;

        public Engine(IGameStoreContext gameStoreContext)
        {
            this.gameStoreContext = gameStoreContext;
        }

        public void Run()
        {

        }

        public void CreateAccount(string username, string password)
        {
            var newAccount = new Account() { Username = username, Password = password };

            gameStoreContext.Accounts.Add(newAccount);
            gameStoreContext.SaveChanges();

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
        public void DeleteAccount(string usernameToDelete)
        {
            // check for admin rigths
            if (true)
            {
                gameStoreContext.Accounts.FirstOrDefault(acc => acc.Username == usernameToDelete).IsDeleted = true;
                gameStoreContext.SaveChanges();
            }

        }

        public void AddOrder(Account account, ShoppingCart cart)
        {
            throw new NotImplementedException();
        }
    }
}
