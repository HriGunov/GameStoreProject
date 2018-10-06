using Autofac;
using GameStore.Commands;
using GameStore.Core;
using GameStore.Data.Context;
using GameStore.Services;
using GameStore.Services.Abstract;

namespace GameStore.Injections
{
    public class GameStoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>();
            builder.RegisterType<GameStoreContext>().As<IGameStoreContext>();
            builder.RegisterType<AccountsService>().As<IAccountsService>();
            builder.RegisterType<ProductsService>().As<IProductsService>();
            builder.RegisterType<CommandManager>().As<ICommandManager>();

            base.Load(builder);
        }
    }
}