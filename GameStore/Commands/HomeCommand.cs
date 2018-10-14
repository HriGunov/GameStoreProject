using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;

namespace GameStore.Commands
{
    internal class HomeCommand : ICommand
    {
        private readonly IEngine engine;

        public HomeCommand(IEngine engine)
        {
            this.engine = engine;
        }

        public string Execute(List<string> parameters)
        {
            engine.MainSection = engine.DefaultMainSection;
            return ".:You're now at the Home Section:.";
        }
    }
}