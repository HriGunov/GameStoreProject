using System;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IGameStoreContext storeContext;

        public AccountsService(IGameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        /// <summary>
        ///     Creates an Account entity and adds it to the DB
        /// </summary>
        public Account AddAccount(string firstName, string lastName, string userName, string password,
            bool isAdmin = false, bool isGuest = false)
        {
            var account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                Username = userName,
                Password = password,
                CreatedOn = DateTime.Now,
                ShoppingCart = new ShoppingCart(),
                IsAdmin = isAdmin
            };

            storeContext.Accounts.Add(account);
            storeContext.SaveChanges();

            return account;
        }

        /// <summary>
        ///     Deletes an account by changing it's IsDeleted flag and sets the DeletedBy to the commandExecutor's name.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be removed</param>
        /// <returns></returns>
        public string RemoveAccount(string commandExecutor, string accountName)
        {
            if (!IsAdmin(commandExecutor))
                return @"You don't have enough permissions.";

            var account = storeContext.Accounts.ToList().FirstOrDefault(acc => acc.Username == accountName);
            if (account == null || account.IsDeleted) return $"Account {accountName} was not found.";

            account.IsDeleted = true;
            account.DeletedBy = commandExecutor;
            storeContext.SaveChanges();
            return $"Account {accountName} has been successfully removed.";
        }

        /// <summary>
        ///     Checks if an account has admin privileges
        /// </summary>
        /// <param name="accountName"></param>
        public bool IsAdmin(string accountName)
        {
            return storeContext.Accounts.ToList().SingleOrDefault(acc => acc.Username == accountName && acc.IsAdmin) !=
                   null;
        }

        /// <summary>
        ///     Finds the account in the database that matches the given accountName in the parameters and returns it as Account
        ///     type.
        /// </summary>
        /// <param name="accountName">Account Name</param>
        /// <returns></returns>
        public Account FindAccount(string accountName)
        {
            var account = storeContext.Accounts
                                      .Include(s => s.ShoppingCart)
                                      .Include(c => c.Comments)
                                      .ToList()
                                      .FirstOrDefault(p => p.Username == accountName);

            if (account == null || account.IsDeleted) return null;

            return account;
        }

        /// <summary>
        ///     Restores an account by changing it's IsDeleted flag and clears the DeletedBy field.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be restored</param>
        /// <returns></returns>
        public string RestoreAccount(string commandExecutor, string accountName)
        {
            if (!IsAdmin(commandExecutor))
                return @"You don't have enough permissions.";

            var account = storeContext.Accounts.ToList().FirstOrDefault(acc => acc.Username == accountName);
            if (account == null) return $"Account {accountName} was not found.";
            if (!account.IsDeleted) return $"Account {accountName} is already restored.";

            account.IsDeleted = false;
            account.DeletedBy = null;
            storeContext.SaveChanges();
            return $"Account {accountName} has been successfully restored.";
        }
    }
}