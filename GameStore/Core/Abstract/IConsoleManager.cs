namespace GameStore.Core.Abstract
{
    public interface IConsoleManager
    {
        void Clear();
        string ListenForCommand();
        void Print();
        void SetChar(char charToSet, int y, int x);
        void SetText(string text, int y, int x);

        void LogMessage(string message);
    }
}