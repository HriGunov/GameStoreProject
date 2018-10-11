namespace GameStore.Core.ConsoleSections.Abstract
{
    public interface ISection
    {
        Position BottomRight { get; set; }
        Position TopLeftCorner { get; set; }

        void DrawSection(IConsoleManager consoleManager);
        void SetChar(char charToSet, int y, int x);
        void SetText(string text, int y, int x);
    }
}