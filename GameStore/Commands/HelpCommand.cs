using System.Collections.Generic;
using GameStore.Commands.Abstract;

namespace GameStore.Commands
{
    internal class HelpCommand : ICommand
    {
        public string Execute(List<string> parameters)
        {
            return "Ask a developer for help. :D";
        }
    }
}