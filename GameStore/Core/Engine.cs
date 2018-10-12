using System;
using GameStore.Commands;
using GameStore.Commands.Abstract;
using GameStore.Core;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

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
            ICommentService commentService,IConsoleManager consoleManager,IMessageLog messageLog)
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
            
            var mockAcc = new Account
            {
                FirstName = "Hristo", LastName = "Gunov", Username = "hrigunov", Password = "hritest", IsGuest = false
            };
            string line;
            

            var nameSection = new TopLeftCornerUserSection(consoleManager, 0, 0);
            
            var testFrameBig = new FramedSection(1, 36, 28, 119,"Main View");
            //Message logger uses the width of this section as constraint
            var testLogger = new LoggerFramedSection(messageLog,1, 0, 28, 35,"Message Log");
            messageLog.WidthConstraint = 34;
            //messageLog.AddToLog(line);
            consoleManager.LogMessage("Welcome to GameStore!",true);
            consoleManager.LogMessage("For more information use the Help commmand.");
            while ((line = consoleManager.ListenForCommand()) != "end")
            {

              
                nameSection.ImprintOnConsoleMatrix(mockAcc);
                //testFrameSmall.DrawSection(consoleManager);
                testFrameBig.DrawSection(consoleManager);                
                testLogger.DrawSection(consoleManager);
                consoleManager.Print();

                // Change that to custom exceptions
                try
                {
                   consoleManager.LogMessage(commandManager.Execute(line));
                }
                catch (Exception ex)
                {
                    consoleManager.LogMessage("Invalid command.");
                }

                
            }
        }
    }
}