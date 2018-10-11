namespace GameStore.Core.ConsoleSections.Abstract
{
    internal interface ILoggerSection
    {
        void AddToLog(string msg);
        void ShowLog(IConsoleManager consoleManager);
    }
}