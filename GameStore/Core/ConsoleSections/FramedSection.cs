using System;

namespace GameStore.Core.ConsoleSections
{
    internal class FramedSection : Section
    {
        private readonly string title;

        public FramedSection(Position topLeftCorner, Position bottomright, string title = "") : base(topLeftCorner, bottomright)
        {
            this.title = title;
        }

        public FramedSection(int topLeftY, int topLeftX, int bottomrightY, int bottomrightX, string title = "") :
            this(new Position(topLeftY, topLeftX), new Position(bottomrightY, bottomrightX), title)
        {

        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            base.DrawSection(consoleManager);

            var width = BottomRight.X - TopLeftCorner.X;
            var heigth = BottomRight.Y - TopLeftCorner.Y;

            if (width <= 0 || heigth <= 0) throw new Exception("Wrong corners of frame section.");

            consoleManager.SetChar('╔', TopLeftCorner.Y, TopLeftCorner.X);
            consoleManager.SetChar('╗', TopLeftCorner.Y, BottomRight.X);
            consoleManager.SetChar('╚', BottomRight.Y, TopLeftCorner.X);
            consoleManager.SetChar('╝', BottomRight.Y, BottomRight.X);

          /*  if (title != "")
            {
                for (int x = 0; x < (width-2)/2 -title.Length/2; x++)
                {
                    consoleManager.SetChar('═', TopLeftCorner.Y, TopLeftCorner.X + x);
                    consoleManager.SetChar('═', BottomRight.Y, TopLeftCorner.X + x);
                }
            }*/
            for (int x = 1; x < width; x++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y, TopLeftCorner.X + x);
                consoleManager.SetChar('═', BottomRight.Y, TopLeftCorner.X + x);
            }

            if (title != "")
            {
                //can be used to draw only the top bit
                var startTitlePosition = ((width) / 2) - title.Length / 2;
                /*
                for (int x = 1; x <= startTitlePosition; x++)
                {
                    consoleManager.SetChar('═', TopLeftCorner.Y, TopLeftCorner.X + x);
                    consoleManager.SetChar('═', TopLeftCorner.Y, TopLeftCorner.X + x + startTitlePosition + title.Length);
                }*/
                consoleManager.SetText(title, TopLeftCorner.Y, TopLeftCorner.X + startTitlePosition + 1);
            }
            for (int y = 1; y < heigth; y++)
            {
                consoleManager.SetChar('║', TopLeftCorner.Y + y, TopLeftCorner.X);
                consoleManager.SetChar('║', TopLeftCorner.Y + y, BottomRight.X);
            }
        }
        


    }
}