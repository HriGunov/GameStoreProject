using System;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;

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
            // Completed: CreateAccount, DeleteAccount, RestoreAccount => They All Work As Intended... {TESTED}
        }

        public void CreateAccount(string firstName, string lastName, string username, string password, bool isAdmin = false)
        {
            var newAccount = new Account {FirstName = firstName, LastName = lastName, Username = username, Password = password, IsAdmin = isAdmin};

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

        public void DeleteAccount(string commandExecutorName, string usernameToDelete)
        {
            if (IsAdmin(commandExecutorName))
            {
                var userObject = gameStoreContext.Accounts.SingleOrDefault(acc => acc.Username == usernameToDelete);
                if (userObject != null)
                {
                    userObject.IsDeleted = true;
                    gameStoreContext.SaveChanges();
                }
            }
        }

        public void RestoreAccount(string commandExecutorName, string usernameToRestore)
        {
            if (IsAdmin(commandExecutorName))
            {
                var userObject = gameStoreContext.Accounts.SingleOrDefault(acc => acc.Username == usernameToRestore);
                if (userObject != null)
                {
                    userObject.IsDeleted = false;
                    gameStoreContext.SaveChanges();
                }
            }
        }

        public bool IsAdmin(string username)
        {
            return gameStoreContext.Accounts.SingleOrDefault(acc => acc.Username == username && acc.IsAdmin) !=
                   null;
        }

        public void AddOrder(Account account, ShoppingCart cart)
        {
            throw new NotImplementedException();
        }
    }
}