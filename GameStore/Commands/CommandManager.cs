using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core.Registration;
using GameStore.Commands.Exceptions;
using GameStore.Commands.Abstract;

namespace GameStore.Commands
{
    public class CommandManager : ICommandManager
    {
        private HashSet<string> knownCommands;
        public CommandManager(ILifetimeScope scope)
        {
            Scope = scope;
            knownCommands = new HashSet<string>(FindAllCommands()); 
        }

        public ILifetimeScope Scope { get; }

        public string Execute(string commandLine)
        {              
            var args = commandLine.Split();

            if (!knownCommands.Contains(args[0].ToLower()))
            {
                return "Invalid command.";
            }
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
                throw new CommandDoesNotExist($"Command ({commandName}) doesn't exist.");
            }
        }

        private IEnumerable<string> FindAllCommands()
        {
            return  AppDomain.CurrentDomain
               .GetAssemblies()
               .SelectMany(s => s.GetTypes())
               .Where(p => typeof(ICommand).IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface).Select(command =>
               {
                   return command.Name.ToLower().Replace("command","");                    
            }); 
        }

    }
}