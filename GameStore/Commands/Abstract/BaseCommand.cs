using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Text;
using GameStore.Data.Context;
namespace GameStore.Commands.Abstract
{
   abstract class BaseCommand : ICommand
    {
        private readonly IGameStoreContext scope;

        public BaseCommand(IGameStoreContext scope)
        {
            this.scope = scope;
        }

        public IGameStoreContext Scope => scope;

        public abstract string Execute(IEnumerable<string> parameters);
         
    }
}
