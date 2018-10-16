using System;
using System.Collections.Generic;
using GameStore.Commands.Abstract;

namespace GameStore.Commands
{
    internal class HelpCommand : ICommand
    {
        private readonly ICommandManager commandManager;

        public HelpCommand(ICommandManager commandManager)
        {
            this.commandManager = commandManager;
        }

        public string Execute(List<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}