using System;
using System.Collections.Generic;
using System.Text;
using GameStore.Data.Context;
using GameStore.Data.Models;

namespace GameStore.Services
{
    public class IAccountsService
    {
        private readonly IGameStoreContext storeContext;

        public IAccountsService(IGameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        public Account AddAccount(string firstName, string lastName, string userName, bool isAdmin = false)
        {
            var account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                Username = userName,
                CreatedOn = DateTime.Now,
                IsAdmin = isAdmin
            };

            this.storeContext.Accounts.Add(account);
            this.storeContext.SaveChanges();

            return account;
        }
    }
}