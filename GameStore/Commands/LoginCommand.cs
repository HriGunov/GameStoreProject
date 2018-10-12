using System;
using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core;
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
        private readonly IConsoleManager consoleManager;

        public LoginCommand(IEngine engine, IConsoleManager consoleManager,
            IAccountsService accountsService,
            IAuthenticationService authenticationService, ICryptographicService cryptographicService)
        {
            this.engine = engine;
            this.consoleManager = consoleManager;
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

            consoleManager.LogMessage("=== Logging In ===", true);            
            string username;
            string password;
            if (parameters.Count == 2)
            {
                username = parameters[0];
                password = cryptographicService.ComputeHash(parameters[1]);
            }
            else
            {
                
                consoleManager.LogMessage("Please Enter Your Username.",true);
                username = consoleManager.ListenForCommand();
                consoleManager.LogMessage("Please Enter Password.",true);
                password = cryptographicService.ComputeHash(consoleManager.ListenForCommand());
            }

            var result = authenticationService.Authenticate(username, password);

            if (result != null)
            {
                engine.CurrentUser = result;
                consoleManager.LogMessage("Successful Login.",true);
                return $"Welcome {engine.CurrentUser.Username}!";
            }

            return "Invalid Username or Password";
        }
    }
}