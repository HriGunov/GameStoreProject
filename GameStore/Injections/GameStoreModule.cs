using System;
using System.Linq;
using Autofac;
using GameStore.Commands;
using GameStore.Commands.Abstract;
using GameStore.Core;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Core.ConsoleSections.MainWindowSections.Abstract;

namespace GameStore.Injections
{
    public class GameStoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            builder.RegisterType<CommandManager>().As<ICommandManager>().SingleInstance();
            builder.RegisterType<ConsoleManager>().As<IConsoleManager>().SingleInstance();
            builder.RegisterType<MessageLog>().As<IMessageLog>().SingleInstance();
            builder.RegisterType<HomeSection>().AsSelf().SingleInstance();
            builder.RegisterType<ProductsSection>().As<IProductsSection>().SingleInstance();
            builder.RegisterType<OrdersSection>().As<IOrdersSection>().SingleInstance();
            builder.RegisterModule<GameStoreDataModule>();
            builder.RegisterModule<GameStoreServicesModule>();

            RegisterCommands(builder);

            base.Load(builder);
        }

        private void RegisterCommands(ContainerBuilder builder)
        {
            AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ICommand).IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface)
                .ToList()
                .ForEach(command =>
                {
                    builder.RegisterType(command).Named<ICommand>(command.Name.ToLower()).SingleInstance();
                });
        }
    }
}