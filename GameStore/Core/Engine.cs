using System;
using GameStore.Commands;
using GameStore.Data.Context;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandManager commandManager;
        private readonly IGameStoreContext gameStoreContext;

        public Engine(IGameStoreContext gameStoreContext, ICommandManager commandManager)
        {
            this.gameStoreContext = gameStoreContext;
            this.commandManager = commandManager;
        }

        public void Run()
        {
            string input;
            while ((input = Console.ReadLine()) != "end")
            {
                var commandExecute = commandManager.Execute(input);
            }
        }
    }
}