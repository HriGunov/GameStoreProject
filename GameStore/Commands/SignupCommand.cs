using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class SignupCommand : ICommand
    {
        private readonly IAccountsService accountsService;
        private readonly IConsoleManager consoleManager;
        private readonly ICryptographicService cryptographicService;
        private readonly IEngine engine;

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
            consoleManager.LogMessage(username);

            consoleManager.LogMessage("Enter Password", true);
            var password = consoleManager.ListenForCommand();
            consoleManager.LogMessage(new string('*', password.Length));

            consoleManager.LogMessage("Enter First Name", true);
            var firstName = consoleManager.ListenForCommand();
            consoleManager.LogMessage(firstName);

            consoleManager.LogMessage("Enter Last Name", true);
            var lastName = consoleManager.ListenForCommand();
            consoleManager.LogMessage(lastName);

            accountsService.AddAccount(firstName, lastName, username, cryptographicService.ComputeHash(password));

            return $"Account {username} has been created.";
        }
    }
}