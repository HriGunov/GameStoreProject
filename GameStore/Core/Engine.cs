using System;
using GameStore.Commands;
using GameStore.Commands.Abstract;
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
        private IConsoleManager consoleManager;

        public Engine(IGameStoreContext gameStoreContext, ICommandManager commandManager,
            ICommentService commentService)
        {
            this.gameStoreContext = gameStoreContext;
            this.commandManager = commandManager;
            this.commentService = commentService;
        }

        public Account CurrentUser { get; set; }

        public void Run()
        {
            consoleManager = new ConsoleManager(this);
            var mockAcc = new Account
            {
                FirstName = "Hristo", LastName = "Gunov", Username = "hrigunov", Password = "hritest", IsGuest = false
            };
            string line;
            var counter = 0;

            var nameSection = new TopLeftCornerUserSection(consoleManager, 0, 0);
            var testFrameSmall = new FramedSection(1, 1, 35, 25);
            var testFrameBig = new FramedSection(1, 36, 25, 119);
            var testLogger = new LoggerFramedSection(1, 1, 25, 35);

            while ((line = consoleManager.ListenForCommand()) != "end")
            {
                nameSection.ImprintOnConsoleMatrix(mockAcc);
                //testFrameSmall.DrawSection(consoleManager);
                testFrameBig.DrawSection(consoleManager);
                testLogger.AddToLog(line);
                testLogger.DrawSection(consoleManager);
                consoleManager.Print();

                // Change that to custom exceptions
                try
                {
                    Console.WriteLine(commandManager.Execute(line));
                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex.Message);
                }

                //counter++;
            }
        }
    }
}