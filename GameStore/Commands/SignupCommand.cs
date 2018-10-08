using GameStore.Core;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Services;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Commands
{
    public class SignupCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IAccountsService accountsService;
        private readonly ICryptographicService cryptographicService;

        public SignupCommand(IEngine engine, IAccountsService accountsService, ICryptographicService cryptographicService)
        {
            this.engine = engine;
            this.accountsService = accountsService;
            this.cryptographicService = cryptographicService;
        }

        public string Execute(List<string> parameters)
        {
            Console.WriteLine("=== Sign Up ===");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine().Trim();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine().Trim();
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine().Trim();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine().Trim();

            accountsService.AddAccount(firstName, lastName, username, cryptographicService.ComputeHash(password));
            return "GG";
        }
    }
}