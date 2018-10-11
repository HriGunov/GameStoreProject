using GameStore.Commands;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections;
using GameStore.Data.Context;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services;
using GameStore.Services.Abstract;
using System;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandManager commandManager;
        private readonly ICommentService commentService;
        private readonly IConsoleManager consoleManager;
        private readonly IGameStoreContext gameStoreContext;

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
            //consoleManager = new ConsoleManager(this);
            string line;
            //int counter = 0;
            while ((line = Console.ReadLine()) != "end")
            {
                //consoleManager.SetText(line, counter, 0);

                //var nameSection = new TopLeftCornerUserSection(consoleManager, 0, 0);
                //nameSection.ImprintOnConsoleMatrix(acc);

                //consoleManager.Print();

                // Change that to custom exceptions
                try
                {
                    Console.WriteLine(commandManager.Execute(line));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //counter++;
            }
        }
    }
}