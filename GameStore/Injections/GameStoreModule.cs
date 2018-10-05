﻿using Autofac;
using GameStore.Core;
using GameStore.Data.Context;
using GameStore.Services;

namespace GameStore.Injections
{
    public class GameStoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>();
            builder.RegisterType<GameStoreContext>().As<IGameStoreContext>();
            builder.RegisterType<AccountsService>().As<IAccountsService>();
            base.Load(builder);
        }
    }
}