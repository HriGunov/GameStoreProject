using System;
using System.Linq;
using Autofac;
using Autofac.Core.Registration;
using GameStore.Commands.Abstract;

namespace GameStore.Commands
{
    public class CommandManager : ICommandManager
    {
        public CommandManager(ILifetimeScope scope)
        {
            Scope = scope;
        }

        public ILifetimeScope Scope { get; }

        public string Execute(string commandLine)
        {
            if (string.IsNullOrEmpty(commandLine))
                throw new ArgumentException("Null commandline passed.", nameof(commandLine));

            var args = commandLine.Split();

            var commandToExecute = FindCommand(args[0]);

            return commandToExecute.Execute(args.Skip(1).ToList());
        }

        public ICommand FindCommand(string commandName)
        {
            try
            {
                return Scope.ResolveNamed<ICommand>(commandName.ToLower() + "command");
            }
            catch (ComponentNotRegisteredException)
            {
                throw new ArgumentException($"Command ({commandName}) doesn't exist.");
            }
        }
    }
}