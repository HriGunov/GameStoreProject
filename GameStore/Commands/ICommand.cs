using System.Collections.Generic;

namespace GameStore.Commands
{
    public interface ICommand
    {
        string Execute(IEnumerable<string> parameters);
    }
}