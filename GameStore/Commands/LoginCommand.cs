using System;
using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IAccountsService accountsService;
        private readonly IAuthenticationService authenticationService;
        private readonly ICryptographicService cryptographicService;
        private readonly IEngine engine;

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
            if (engine.CurrentUser != null)
                return "You're already logged in.";

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