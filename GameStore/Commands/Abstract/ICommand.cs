using System.Collections.Generic;

namespace GameStore.Commands.Abstract
{
    public interface ICommand
    {
        string Execute(List<string> parameters);
    }
}