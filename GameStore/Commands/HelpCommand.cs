using GameStore.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Commands
{
    class HelpCommand : ICommand
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
