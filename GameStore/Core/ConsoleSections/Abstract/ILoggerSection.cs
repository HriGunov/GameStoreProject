using System.Collections.Generic;

namespace GameStore.Core.ConsoleSections.Abstract
{
    interface ILoggerSection
    {
        
        void AddToLog( string msg);
        void ShowLog(IConsoleManager consoleManager);
    }
}
