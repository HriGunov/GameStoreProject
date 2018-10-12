using GameStore.Commands.Abstract;
using GameStore.Commands.Exceptions;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandManager commandManager;
        private readonly ICommentService commentService;
        private readonly IConsoleManager consoleManager;
        private readonly IGameStoreContext gameStoreContext;
        private readonly IMessageLog messageLog;

        public Engine(IGameStoreContext gameStoreContext, ICommandManager commandManager,
            ICommentService commentService, IConsoleManager consoleManager, IMessageLog messageLog,
            IAccountsService accountsService)
        {
            this.gameStoreContext = gameStoreContext;
            this.commandManager = commandManager;
            this.commentService = commentService;
            this.consoleManager = consoleManager;
            this.messageLog = messageLog;
        }

        public Account CurrentUser { get; set; }

        public void Run()
        {
            string line;

            var headerSection = new HeaderSection(this);

            var testFrameBig = new HomeSection(1, 36, 28, 119, "Main View");
            //Message logger uses the width of this section as constraint
            consoleManager.LogMessage("Welcome to GameStore!", true);
            consoleManager.LogMessage("For more information use the Help commmand.");
            var testLogger = new LoggerFramedSection(messageLog, 1, 0, 28, 35, "Message Log");

            headerSection.DrawSection(consoleManager);
            testFrameBig.DrawSection(consoleManager);
            testLogger.DrawSection(consoleManager);
            consoleManager.Print();

            while ((line = consoleManager.ListenForCommand()) != "end")
            {
                // Change that to custom exceptions
                try
                {
                    consoleManager.LogMessage(commandManager.Execute(line));
                }
                catch (CommandDoesNotExist)
                {
                    consoleManager.LogMessage("Invalid command.");
                }

                headerSection.DrawSection(consoleManager);
                testFrameBig.DrawSection(consoleManager);
                testLogger.DrawSection(consoleManager);
                consoleManager.Print();
            }
        }
    }
}