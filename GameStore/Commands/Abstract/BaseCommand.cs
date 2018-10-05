using System.Collections.Generic;
using GameStore.Data.Context;

namespace GameStore.Commands.Abstract
{
    public abstract class BaseCommand : ICommand
    {
        public BaseCommand(IGameStoreContext scope)
        {
            Scope = scope;
        }

        public IGameStoreContext Scope { get; }

        public abstract string Execute(IEnumerable<string> parameters);
    }
}