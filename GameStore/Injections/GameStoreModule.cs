using Autofac;
using GameStore.Data.Context;

namespace GameStore.Injections
{
    class GameStoreModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreContext>().As<IGameStoreContext>();

            base.Load(builder);
        }
    }
}
