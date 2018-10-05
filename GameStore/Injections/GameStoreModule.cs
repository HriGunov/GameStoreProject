using Autofac;
using GameStore.Core;
using GameStore.Data.Context;

namespace GameStore.Injections
{
    public class GameStoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>();
            builder.RegisterType<GameStoreContext>().As<IGameStoreContext>();
            base.Load(builder);
        }
    }
}