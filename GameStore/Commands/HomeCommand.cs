using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Commands
{
    class HomeCommand : ICommand
    {
        private readonly IEngine engine;

        public HomeCommand(IEngine engine)
        {
            this.engine = engine;
        }
        public string Execute(List<string> parameters)
        {
            engine.MainSection = engine.DefaultMainSection;
            return "";
        }
    }
}
