using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using GameStore.Data.Context;

namespace GameStore.Injections
{
    class GameStoreModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreContext>().As<IGameStoreContext>();
            
            base.Load(builder);
        }
    }
}
