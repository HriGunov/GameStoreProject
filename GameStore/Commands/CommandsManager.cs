using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace GameStore.Commands
{
    public class CommandsManager : ICommandsManager
    {

        public CommandsManager(ILifetimeScope scope)
        {
            Scope = scope;
        }

        public ILifetimeScope Scope { get; }


        public string Execute(string commandLine)
        {
            if (string.IsNullOrEmpty(commandLine))
            {
                throw new ArgumentException("Null commandline passed.", nameof(commandLine));
            }

            var tokens = commandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var commandToExecute = FindCommand(tokens[0]);

            return commandToExecute.Execute(tokens.Skip(1));
        }
        public ICommand FindCommand(string commandName)
        {
            return Scope.ResolveNamed<ICommand>(commandName.ToLower());
        }
 
    }
}
