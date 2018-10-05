using System;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly IGameStoreContext gameStoreContext;

        public Engine(IGameStoreContext gameStoreContext)
        {
            this.gameStoreContext = gameStoreContext;
        }

        public void Run()
        {

        }

    }
}