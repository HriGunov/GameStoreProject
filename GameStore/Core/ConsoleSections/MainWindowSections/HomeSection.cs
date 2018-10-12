using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStore.Core.Abstract;
using GameStore.Core.Logo;

namespace GameStore.Core.ConsoleSections.MainWindowSections
{
    class HomeSection : FramedSection
    {
        public HomeSection(int topLeftY, int topLeftX, int bottomrightY, int bottomrightX,string title = "") : base(topLeftY, topLeftX, bottomrightY, bottomrightX, title)
        {
        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            base.DrawSection(consoleManager);

          
            var sectionWidth = BottomRight.X - TopLeftCorner.X;
            var sectionHeigth = BottomRight.Y - TopLeftCorner.Y;

            var foo = Logo.Logo.Text.Replace("\r", "");
            var logoLines = foo.Split(new char[] { '\n' });
            var logoWidth = logoLines.Max(line => line.Length);
            var logoHeigth = logoLines.Length;

            var messagePivotPoint = new Position((sectionHeigth / 2) - (logoHeigth / 2), (sectionWidth / 2) - (logoWidth / 2));

            for (int y = 0; y < logoHeigth; y++)
            {
                for (int x = 0; x < logoLines[y].Length; x++)
                {
                    consoleManager.SetChar(logoLines[y][x], TopLeftCorner.Y+ messagePivotPoint.Y + y, TopLeftCorner.X+ messagePivotPoint.X + x);
                }
            }

        }
    }
}
