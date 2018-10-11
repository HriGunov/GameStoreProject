using System;
using System.Linq;
using Autofac;
using GameStore.Commands;
using GameStore.Commands.Abstract;
using GameStore.Core;
using GameStore.Core.Abstract;
using GameStore.Data.Injections;
using GameStore.Services.Injections;

namespace GameStore.Injections
{
    public class GameStoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>();
            builder.RegisterType<CommandManager>().As<ICommandManager>();
            builder.RegisterModule<GameStoreDataModule>();
            builder.RegisterModule<GameStoreServicesModule>();
            //builder.RegisterType<ConsoleManager>().As<IConsoleManager>();

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