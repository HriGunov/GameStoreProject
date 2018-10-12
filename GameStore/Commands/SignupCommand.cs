using GameStore.Commands.Abstract;
using GameStore.Core;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;

namespace GameStore.Commands
{
    public class SignupCommand : ICommand
    {
        private readonly IAccountsService accountsService;
        private readonly ICryptographicService cryptographicService;
        private readonly IEngine engine;
        private readonly IConsoleManager consoleManager;

        public SignupCommand(IEngine engine, IConsoleManager consoleManager, IAccountsService accountsService,
            ICryptographicService cryptographicService)
        {
            this.engine = engine;
            this.consoleManager = consoleManager;
            this.accountsService = accountsService;
            this.cryptographicService = cryptographicService;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser != null && engine.CurrentUser.IsGuest != true)
                return "You're already logged in.";

            consoleManager.LogMessage("=== Signing Up ===", true);
            consoleManager.LogMessage("Enter Username", true);
            var username = consoleManager.ListenForCommand();
            consoleManager.LogMessage("Enter Password",true);
            var password = consoleManager.ListenForCommand();
            consoleManager.LogMessage("Enter First Name", true);
            var firstName = consoleManager.ListenForCommand();
            consoleManager.LogMessage("Enter Last Name", true);
            var lastName = consoleManager.ListenForCommand();

            accountsService.AddAccount(firstName, lastName, username, cryptographicService.ComputeHash(password));
            return $"Account {username} has been created.";
        }
    }
}