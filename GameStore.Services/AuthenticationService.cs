using GameStore.Data.Models;
using GameStore.Services.Abstract;
using System;

namespace GameStore.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IAccountsService accountsService;
        private readonly ICryptographicService cryptographicService;

        public AuthenticationService(IAccountsService accountsService, ICryptographicService cryptographicService)
        {
            this.accountsService = accountsService ?? throw new ArgumentNullException(nameof(accountsService));
            this.cryptographicService = cryptographicService ?? throw new ArgumentNullException(nameof(cryptographicService));
        }

        /// <summary>
        /// Checks if username exists and passwords match
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>        
        public Account Authenticate(string username, string password)
        {
            var foundAccount = accountsService.FindAccount(username);
            return foundAccount == null ? null : foundAccount.Password == password ? foundAccount : null;
        }
    }
}