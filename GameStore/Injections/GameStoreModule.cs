using Autofac;
using GameStore.Commands;
using GameStore.Core;
using GameStore.Data.Context;
using GameStore.Services;
using GameStore.Services.Abstract;
using System;
using System.Linq;
using System.Reflection;

namespace GameStore.Injections
{
    public class GameStoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>();
            builder.RegisterType<GameStoreContext>().As<IGameStoreContext>();
            builder.RegisterType<AccountsService>().As<IAccountsService>();
            builder.RegisterType<ProductsService>().As<IProductsService>();
            builder.RegisterType<ShoppingCartsService>().As<IShoppingCartsService>();
            builder.RegisterType<CommandManager>().As<ICommandManager>();
            RegisterCommands(builder);
            base.Load(builder);
        }

        private void RegisterCommands(ContainerBuilder builder)
        {
            var type = typeof(ICommand);
            var commands = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && !p.IsAbstract &&!p.IsInterface);

            foreach (var command in commands)
            {
                builder.RegisterType(command).Named<ICommand>(command.Name.ToLower()).SingleInstance();
            }
            
        }
    }
}