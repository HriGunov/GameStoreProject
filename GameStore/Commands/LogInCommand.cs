using GameStore.Core;
using GameStore.Services;
using GameStore.Services.Abstract;
using System.Collections.Generic;

namespace GameStore.Commands
{
    class LogInCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IAccountsService accountsService;
        private readonly IAuthenticationService authenticationService;
        private readonly ICryptographicService cryptographicService;

        public LogInCommand(IEngine engine, IAccountsService accountsService,
            IAuthenticationService authenticationService, ICryptographicService cryptographicService)
        {
            this.engine = engine;
            this.accountsService = accountsService;
            this.authenticationService = authenticationService;
            this.cryptographicService = cryptographicService;
        }
        //LogIn {username} {password}        
        public string Execute(List<string> parameters)
        {
            var username = parameters[0];
            var password = cryptographicService.ComputeHash(parameters[1]);
            var result = authenticationService.Authenticate(username, password);

            if (result != null)
            {
                engine.CurrentUser = result;
                return "Successful Login.";
            }
            return "Invalid Password or Username";
            
        }
    }
}
