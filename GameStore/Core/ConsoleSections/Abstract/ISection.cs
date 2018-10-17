using GameStore.Core.Abstract;

namespace GameStore.Core.ConsoleSections.Abstract
{
    public interface ISection
    {
        Position BottomRightCorner { get; set; }
        Position TopLeftCorner { get; set; }
        bool Render { get; set; }
        void DrawSection(IConsoleManager consoleManager);
             
    }
}