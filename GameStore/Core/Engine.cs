using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections;
using GameStore.Core.ConsoleSections.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Exceptions;
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
            IAccountsService accountsService, HomeSection homeSection)
        {
            this.gameStoreContext = gameStoreContext;
            this.commandManager = commandManager;
            this.commentService = commentService;
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

            var testProducts = new ProductsSection
            {
                ProductsInView = new[]
                {
                    new Product {Name = "Test Product", Price = 1},
                    new Product {Name = "Test Product2", Genre = {new Genre {Name = "Action"}}, Price = 2}
                }
            };

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
                catch (UserException)
                {
                    consoleManager.LogMessage("Invalid command.");
                }
                // catch (Exception) - Log

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