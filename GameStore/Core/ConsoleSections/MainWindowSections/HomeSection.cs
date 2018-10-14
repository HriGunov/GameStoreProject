using System.Linq;
using GameStore.Core.Abstract;

namespace GameStore.Core.ConsoleSections.MainWindowSections
{
    public class HomeSection : FramedSection
    {
        public HomeSection() : this(1, 36, 28, 119, "Main View")
        {

        }
        public HomeSection(int topLeftY, int topLeftX, int bottomrightY, int bottomrightX,string title = "") : base(topLeftY, topLeftX, bottomrightY, bottomrightX, title)
        {
        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            if (Render)
            {
                base.DrawSection(consoleManager);
                DrawLogo(consoleManager);
            }
        }

        private void DrawLogo(IConsoleManager consoleManager)
        {

            var sectionWidth = BottomRightCorner.X - TopLeftCorner.X;
            var sectionHeigth = BottomRightCorner.Y - TopLeftCorner.Y;

            var foo = Logo.Logo.Text.Replace("\r", "");
            var logoLines = foo.Split(new[] {'\n'});
            var logoWidth = logoLines.Max(line => line.Length);
            var logoHeigth = logoLines.Length;

            var messagePivotPoint = new Position(sectionHeigth / 2 - logoHeigth / 2, sectionWidth / 2 - logoWidth / 2);

            for (int y = 0; y < logoHeigth; y++)
            {
                for (int x = 0; x < logoLines[y].Length; x++)
                {
                    consoleManager.SetChar(logoLines[y][x], TopLeftCorner.Y + messagePivotPoint.Y + y, TopLeftCorner.X + messagePivotPoint.X + x);
                }
            }
        }
    }
}