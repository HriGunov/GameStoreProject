using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Core.ConsoleSections
{
    class FramedSection : Section
    {
        public FramedSection(Position topLeftCorner, Position bottomRigth) : base(topLeftCorner, bottomRigth)
        {
        }

        public FramedSection(int topLeftY, int topLeftX, int bottomRigthY, int bottomRigthX) : 
            this(new Position(topLeftY,topLeftX),new Position(bottomRigthY,bottomRigthX))
        {
            
        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            base.DrawSection(consoleManager);

            int width = BottomRigth.X - TopLeftCorner.X;
            int heigth = BottomRigth.Y - TopLeftCorner.Y;

            if (width <= 0 || heigth <= 0)
            {
                throw new Exception("Wrong corners of frame section.");
            }
            consoleManager.SetChar('╔', TopLeftCorner.Y, TopLeftCorner.X);
            consoleManager.SetChar('╗', TopLeftCorner.Y, BottomRigth.X);
            consoleManager.SetChar('╚', BottomRigth.Y, TopLeftCorner.X);
            consoleManager.SetChar('╝', BottomRigth.Y, BottomRigth.X);

            for (int x = 1; x < width; x++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y, TopLeftCorner.X + x);
                consoleManager.SetChar('═', BottomRigth.Y, TopLeftCorner.X + x);
            }

            for (int y = 1; y < heigth; y++)
            {
                consoleManager.SetChar('║', TopLeftCorner.Y + y, TopLeftCorner.X);
                consoleManager.SetChar('║', TopLeftCorner.Y + y, BottomRigth.X);
            }

        }
        


    }
}
