using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using System;
using System.Collections.Generic;

namespace GameStore.Commands
{
    internal class HelpCommand : ICommand
    {
         
        public HelpCommand()
        {
             
        }

        public string Execute(List<string> parameters)
        {
            return "Ask a developer for help. :D";
        }
    }
}