using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStore.Data.Context;
using GameStore.Data.Models;

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
        /// Creats a Account Entitiy and adds it to the DB
        /// </summary>  
        public Account AddAccount(string firstName, string lastName, string userName, string password, bool isAdmin = false)
        {
            var account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                Username = userName,
                Password = password,
                CreatedOn = DateTime.Now,
                IsAdmin = isAdmin
            };

            this.storeContext.Accounts.Add(account);
            this.storeContext.SaveChanges();

            return account;
        }

        /// <summary>
        /// Deletes an account by changing it's IsDeleted flag and sets the deletion timestamp.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be removed/undeleted</param>
        /// <returns></returns>
        public string RemoveAccount(string commandExecutor, string accountName)
        {
            if (!IsAdmin(commandExecutor))
                return @"You don't have enough permissions.";

            var account = this.storeContext.Accounts.FirstOrDefault(acc => acc.Username == accountName);
            if (account == null || account.IsDeleted) return $"Account {accountName} was not found.";

            account.IsDeleted = true;
            account.DeletedBy = commandExecutor;
            this.storeContext.SaveChanges();
            return $"Account {accountName} has been successfully removed.";
        }
        /// <summary>
        /// Restores an account by changing it's IsDeleted flag and removes the deletion timestamp.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be restored/undeleted</param>
        /// <returns></returns>
        public string RestoreAccount(string commandExecutor, string accountName)
        {
            if (!IsAdmin(commandExecutor))
                return @"You don't have enough permissions.";

            var account = this.storeContext.Accounts.FirstOrDefault(acc => acc.Username == accountName);
            if (account == null) return $"Account {accountName} was not found.";
            if (!account.IsDeleted) return $"Account {accountName} is already restored.";

            account.IsDeleted = false;
            account.DeletedBy = null;
            this.storeContext.SaveChanges();
            return $"Account {accountName} has been successfully restored.";
        }

        public bool IsAdmin(string accountName)
        {
            return this.storeContext.Accounts.SingleOrDefault(acc => acc.Username == accountName && acc.IsAdmin).IsAdmin;
        }
    }
}