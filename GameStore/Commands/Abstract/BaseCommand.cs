using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Text;
using GameStore.Data.Context;
namespace GameStore.Commands.Abstract
{
    public abstract class BaseCommand : ICommand
    {
        public BaseCommand(IGameStoreContext scope)
        {
            this.Scope = scope;
        }

        public IGameStoreContext Scope { get; }

        public abstract string Execute(IEnumerable<string> parameters);

    }
}
