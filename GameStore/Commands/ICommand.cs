using System.Collections.Generic;

namespace GameStore.Commands
{
    internal interface ICommand
    {
        string Execute(IEnumerable<string> parameters);
    }
}