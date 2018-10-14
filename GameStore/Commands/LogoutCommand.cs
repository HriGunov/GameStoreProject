using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Commands
{
    class LogoutCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IAccountsService accountsService;

        public LogoutCommand(IEngine engine, IAccountsService accountsService)
        {
            this.engine = engine;
            this.accountsService = accountsService;
        }
        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser.IsGuest == true)
            {
                return "You have to be logged in first.";
            }
            engine.CurrentUser = accountsService.GetGuestAccount();

            engine.MainSection = engine.DefaultMainSection;
            return "You logged out.";
        }
    }
}
