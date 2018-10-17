using Autofac;
using GameStore.Data.Context;
using GameStore.Data.Context.Abstract;

namespace GameStore.Data.Injections
{
    public class GameStoreDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreContext>().As<IGameStoreContext>().SingleInstance();
        }
    }
}