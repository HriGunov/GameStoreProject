using System;
using System.Linq;
using GameStore.Commands;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly IGameStoreContext gameStoreContext;
        private readonly ICommandsManager commandsManager;

        public Engine(IGameStoreContext gameStoreContext,ICommandsManager commandsManager)
        {
            this.gameStoreContext = gameStoreContext;
            this.commandsManager = commandsManager;
        }

        public void Run()
        {
            var line = Console.ReadLine();
            while (line != "end")
            {
                var commandMSG =commandsManager.Execute(line);
            }
        }

    }
}