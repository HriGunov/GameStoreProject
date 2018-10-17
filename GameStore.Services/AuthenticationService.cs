using System;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountsService accountsService;

        public AuthenticationService(IAccountsService accountsService)
        {
            this.accountsService = accountsService ?? throw new ArgumentNullException(nameof(accountsService));
        }

        /// <summary>
        ///     Checks if username exists and passwords match
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public Account Authenticate(string username, string password)
        {
            var foundAccount = accountsService.FindAccount(username, true);
            return foundAccount == null ? null : foundAccount.Password == password ? foundAccount : null;
        }
    }
}