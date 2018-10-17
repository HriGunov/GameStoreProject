using System;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.Abstract;

namespace GameStore.Core.ConsoleSections
{
    public class Section : ISection
    {
       
        public Section(Position topLeftCorner, Position bottomRight)
        {
            TopLeftCorner = topLeftCorner;
            BottomRightCorner = bottomRight;
            Render = true;
        }

        public Position TopLeftCorner { get; set; }
        public Position BottomRightCorner { get; set; }
        public bool Render { get; set; }

        public virtual void DrawSection(IConsoleManager consoleManager)
        {
             
            Clear(consoleManager);
        }
 
        private void Clear(IConsoleManager consoleManager)
        {
            var width = BottomRightCorner.X - TopLeftCorner.X;
            var heigth = BottomRightCorner.Y - TopLeftCorner.Y;

            for (var y = 0; y < heigth; y++)
            for (var x = 0; x < width; x++)
                consoleManager.SetChar(' ', TopLeftCorner.Y + y, TopLeftCorner.X + x);
        }
    }
}