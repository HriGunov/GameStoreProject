using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class RemoveAccountCommand : ICommand
    {
        private readonly IAccountsService accountsService;
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;

        public RemoveAccountCommand(IEngine engine, IConsoleManager consoleManager, IAccountsService accountsService)
        {
            this.engine = engine;
            this.consoleManager = consoleManager;
            this.accountsService = accountsService;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser == null)
                return "You must be logged in...";

            if (!engine.CurrentUser.IsAdmin)
                return "You don't have enough permissions.";

            consoleManager.LogMessage("=== Removing Account ===", true);

            consoleManager.LogMessage("Enter Name", true);
            var tempAccount = consoleManager.ListenForCommand();
            consoleManager.LogMessage(tempAccount);

            // Removing the product
            accountsService.RemoveAccount(engine.CurrentUser, accountsService.FindAccount(tempAccount));

            return $"Product {tempAccount} has been removed successfully.";
        }
    }
}