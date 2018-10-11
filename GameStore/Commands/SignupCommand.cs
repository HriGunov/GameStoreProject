using System;
using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class SignupCommand : ICommand
    {
        private readonly IAccountsService accountsService;
        private readonly ICryptographicService cryptographicService;
        private readonly IEngine engine;

        public SignupCommand(IEngine engine, IAccountsService accountsService,
            ICryptographicService cryptographicService)
        {
            this.engine = engine;
            this.accountsService = accountsService;
            this.cryptographicService = cryptographicService;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser != null)
                return "You're already logged in.";

            Console.WriteLine("=== Sign Up ===");
            Console.Write("Enter Username: ");
            var username = Console.ReadLine().Trim();
            Console.Write("Enter Password: ");
            var password = Console.ReadLine().Trim();
            Console.Write("Enter First Name: ");
            var firstName = Console.ReadLine().Trim();
            Console.Write("Enter Last Name: ");
            var lastName = Console.ReadLine().Trim();

            accountsService.AddAccount(firstName, lastName, username, cryptographicService.ComputeHash(password));
            return "GG";
        }
    }
}