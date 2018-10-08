using System.Collections.Generic;

namespace GameStore.Commands
{
    public interface ICommand
    {
        string Execute(List<string> parameters);
    }
}