using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    internal class LogoutCommand : ICommand
    {
        private readonly IAccountsService accountsService;
        private readonly IEngine engine;

        public LogoutCommand(IEngine engine, IAccountsService accountsService)
        {
            this.engine = engine;
            this.accountsService = accountsService;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser.IsGuest) return "You have to be logged in first.";
            engine.CurrentUser = accountsService.GetGuestAccount();

            engine.MainSection = engine.DefaultMainSection;
            return "You logged out.";
        }
    }
}