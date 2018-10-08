using GameStore.Core;
using GameStore.Core.Abstract;
using GameStore.Services;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;

namespace GameStore.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IAccountsService accountsService;
        private readonly IAuthenticationService authenticationService;
        private readonly ICryptographicService cryptographicService;

        public LoginCommand(IEngine engine, IAccountsService accountsService,
            IAuthenticationService authenticationService, ICryptographicService cryptographicService)
        {
            this.engine = engine;
            this.accountsService = accountsService;
            this.authenticationService = authenticationService;
            this.cryptographicService = cryptographicService;
        }

        //LogIn {username} {password}
        //LogIn 
        public string Execute(List<string> parameters)
        {
            Console.WriteLine("=== Log In ===");
            string username;
            string password;
            if (parameters.Count == 2)
            {
                username = parameters[0];
                password = cryptographicService.ComputeHash(parameters[1]);
            }
            else
            {
                Console.Write("Enter Username: ");
                username = Console.ReadLine();
                Console.Write("Enter Password: ");
                password = Console.ReadLine();
            }

            var result = authenticationService.Authenticate(username, password);

            if (result != null)
            {
                engine.CurrentUser = result;
                return $"Successful Login.{Environment.NewLine}Welcome {engine.CurrentUser.Username}!";
            }
            return "Invalid Password or Username";
        }
    }
}