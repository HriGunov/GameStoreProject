using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Commands
{
    interface ICommand
    {
        string Execute(IEnumerable<string> parameters);
    }
}
