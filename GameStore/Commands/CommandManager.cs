using System;
using System.Linq;
using Autofac;

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
            return Scope.ResolveNamed<ICommand>(commandName.ToLower()+"command");
        }
    }
}