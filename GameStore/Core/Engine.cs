using System;
using System.Reflection;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections;
using GameStore.Core.ConsoleSections.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services.Abstract;
using log4net;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICommandManager commandManager;
        private readonly IConsoleManager consoleManager;
        private readonly IMessageLog messageLog;

        public Engine(ICommandManager commandManager, IConsoleManager consoleManager, IMessageLog messageLog,
            IAccountsService accountsService, HomeSection homeSection)
        {
            this.commandManager = commandManager;
            this.consoleManager = consoleManager;
            this.messageLog = messageLog;
            CurrentUser = accountsService.GetGuestAccount();
            DefaultMainSection = homeSection;
            MainSection = homeSection;
        }

        public Account CurrentUser { get; set; }
        public ISection MainSection { get; set; }
        public ISection DefaultMainSection { get; set; }


        public void Run()
        {
            string line;

            var headerSection = new HeaderSection(this);

            var testFrameBig = new HomeSection();
            //Message logger uses the width of this section as constraint
            consoleManager.LogMessage("Welcome to GameStore!", true);
            consoleManager.LogMessage("For more information use the Help commmand.");
            var testLogger = new LoggerFramedSection(messageLog);

            headerSection.DrawSection(consoleManager);
            testFrameBig.DrawSection(consoleManager);
            testLogger.DrawSection(consoleManager);
            consoleManager.Print();


            while ((line = consoleManager.ListenForCommand()) != "end")
            {
                // Change that to custom exceptions
                try
                {
                    var result = commandManager.Execute(line);
                    if (!string.IsNullOrEmpty(result)) consoleManager.LogMessage(result);
                }
                catch (UserException ex)
                {
                    consoleManager.LogMessage(ex.Message);
                }
                catch (Exception ex)
                {
                    // Log Exception
                    consoleManager.LogMessage("An error has occured. (Invalid Action)");
                    logger.Fatal($"Last User Input -> '{line}': {ex}");
                }

                headerSection.DrawSection(consoleManager);

                testFrameBig.DrawSection(consoleManager);

                testLogger.DrawSection(consoleManager);

                MainSection.DrawSection(consoleManager);

                //testProducts.DrawSection(consoleManager);

                consoleManager.Print();
            }
        }
    }
}