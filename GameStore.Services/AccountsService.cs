﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class AccountsService : IAccountsService
    {
        private const string Pattern = @"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$";
        private static readonly Regex usernameFormat = new Regex(Pattern);

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
            if (!IsUserNameAllowed(userName))
                throw new UserException($"Account username ({userName}) is not in valid format.");

            if (FindAccount(userName) != null)
                throw new UserException($"Account ({userName}) already exists.");

            var account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                Username = userName,
                Password = password,
                CreatedOn = DateTime.Now,
                ShoppingCart = new ShoppingCart(),
                OrderProducts = new List<Order>(),
                Comments = new List<Comment>(),
                IsAdmin = isAdmin
            };

            storeContext.Accounts.Add(account);
            storeContext.SaveChanges();

            return account;
        }

        /// <summary>
        ///     Adds an already setup Account Type.
        /// </summary>
        /// <param name="account">Account Type</param>
        /// <returns></returns>
        public Account AddAccount(Account account)
        {
            return AddAccount(account.FirstName, account.LastName, account.Username, account.Password, account.IsAdmin,
                account.IsGuest);
        }

        /// <summary>
        ///     Deletes an account by changing it's IsDeleted flag and sets the DeletedBy to the commandExecutor's name.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be removed</param>
        /// <returns></returns>
        public string RemoveAccount(Account commandExecutor, Account accountName)
        {
            // Move that to Command.
            if (!IsAdmin(commandExecutor))
                throw new UserException("You don't have enough permissions.");

            var account = GetAccounts().FirstOrDefault(acc => acc.Username == accountName.Username);
            if (account == null || account.IsDeleted) return $"Account {accountName.Username} was not found.";

            account.IsDeleted = true;
            account.DeletedBy = commandExecutor.Username;
            storeContext.SaveChanges();
            return $"Account {accountName} has been successfully removed.";
        }

        /// <summary>
        ///     Checks if an account has admin privileges
        /// </summary>
        /// <param name="accountName">Account Name</param>
        public bool IsAdmin(Account account)
        {
            return GetAccounts().SingleOrDefault(acc => acc.Username == account.Username && acc.IsAdmin) !=
                   null;
        }

        /// <summary>
        /// Adds credit card to the given user.
        /// </summary>
        /// <param name="cardNumber">Card number.</param>
        /// <param name="account">Account Type</param>
        public void AddCreditCard(string cardNumber, Account account)
        {
            GetAccounts().SingleOrDefault(a => a.Username == account.Username).CreditCard = cardNumber;
            storeContext.SaveChanges();
        }

        /// <summary>
        ///     Finds the account in the database that matches the given accountName in the parameters and returns it as Account
        ///     type.
        /// </summary>
        /// <param name="accountName">Account Name</param>
        /// <returns></returns>
        public Account FindAccount(string accountName)
        {
            var account = GetAccounts().SingleOrDefault(p => p.Username == accountName);

            return account == null || account.IsDeleted ? null : account;
        }

        /// <summary>
        ///     Restores an account by changing it's IsDeleted flag and clears the DeletedBy field.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be restored</param>
        /// <returns></returns>
        public string RestoreAccount(Account commandExecutor, Account accountName)
        {
            // Move that to Command.
            if (!IsAdmin(commandExecutor))
                throw new UserException("You don't have enough permissions.");

            var account = GetAccounts().SingleOrDefault(acc => acc.Username == accountName.Username);
            if (account == null) return $"Account {accountName} was not found.";
            if (!account.IsDeleted) return $"Account {accountName} is already restored.";

            account.IsDeleted = false;
            account.DeletedBy = null;
            storeContext.SaveChanges();
            return $"Account {accountName} has been successfully restored.";
        }

        /// <summary>
        ///     Returns the Guest Account from the database.
        /// </summary>
        /// <returns></returns>
        public Account GetGuestAccount()
        {
            return GetAccounts().SingleOrDefault(acc => acc.IsGuest);
        }

        /// <summary>
        ///     Returns all Accounts from the database. (Not good for big/major applications & security reasons -> Danail's point
        ///     of view)
        /// </summary>
        /// <returns></returns>
        public IQueryable<Account> GetAccounts()
        {
            return storeContext.Accounts
                .Include(s => s.ShoppingCart)
                .ThenInclude(s => s.ShoppingCartProducts)
                .ThenInclude(cart => cart.Product)
                .ThenInclude(g => g.Genre)
                .Include(s => s.ShoppingCart)
                .ThenInclude(s => s.ShoppingCartProducts)
                .ThenInclude(sh => sh.Product)
                .ThenInclude(c => c.Comments)
                .ThenInclude(comment => comment.Account)
                .Include(s => s.ShoppingCart)
                .ThenInclude(s => s.ShoppingCartProducts)
                .ThenInclude(sh => sh.Product)
                .ThenInclude(c => c.Comments)
                .ThenInclude(comment => comment.Product)
                .Include(s => s.ShoppingCart)
                .ThenInclude(s => s.ShoppingCartProducts)
                .ThenInclude(cart => cart.ShoppingCart)
                .Include(c => c.Comments)
                .ThenInclude(comment => comment.Account)
                .Include(c => c.Comments)
                .ThenInclude(comment => comment.Product);
        }

        /// <summary>
        ///     Determines whether the username meets conditions.
        ///     Username conditions:
        ///     Must be 1 to 24 character in length
        ///     Must start with letter a-zA-Z
        ///     May contain letters, numbers or '.','-' or '_'
        ///     Must not end in '.','-','._' or '-_'
        /// </summary>
        /// <param name="userName">Given userName</param>
        /// <returns>True if the username is valid</returns>
        private bool IsUserNameAllowed(string userName)
        {
            return !string.IsNullOrEmpty(userName) && usernameFormat.IsMatch(userName);
        }
    }
}