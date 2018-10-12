using System;
using GameStore.Commands;
using GameStore.Commands.Abstract;
using GameStore.Core;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using GameStore.Commands.Exceptions;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandManager commandManager;
        private readonly ICommentService commentService;
        private readonly IGameStoreContext gameStoreContext;
        private readonly IConsoleManager consoleManager;
        private readonly IMessageLog messageLog;

        public Engine(IGameStoreContext gameStoreContext, ICommandManager commandManager,
            ICommentService commentService, IConsoleManager consoleManager, IMessageLog messageLog)
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

            var nameSection = new TopLeftCornerUserSection(consoleManager, 0, 0);

            var testFrameBig = new FramedSection(1, 36, 28, 119, "Main View");
            //Message logger uses the width of this section as constraint
            consoleManager.LogMessage("Welcome to GameStore!",true);
            consoleManager.LogMessage("For more information use the Help commmand.");
            var testLogger = new LoggerFramedSection(messageLog, 1, 0, 28, 35, "Message Log");

            nameSection.ImprintOnConsoleMatrix(CurrentUser);
            testFrameBig.DrawSection(consoleManager);
            testLogger.DrawSection(consoleManager);
            consoleManager.Print();

            while ((line = consoleManager.ListenForCommand()) != "end")
            {
                consoleManager.LogMessage(line);


                nameSection.ImprintOnConsoleMatrix(CurrentUser);                
                testFrameBig.DrawSection(consoleManager);
                testLogger.DrawSection(consoleManager);
                consoleManager.Print();

                // Change that to custom exceptions
                try
                {
                   consoleManager.LogMessage(commandManager.Execute(line));
                }
                catch (CommandDoesNotExist)
                {
                    consoleManager.LogMessage("Invalid command.");
                }
            }
        }
    }
}