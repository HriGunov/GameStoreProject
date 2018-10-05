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
            return this.storeContext.Accounts.SingleOrDefault(acc => acc.Username == accountName && acc.IsAdmin) !=
                   null;
        }
    }
}