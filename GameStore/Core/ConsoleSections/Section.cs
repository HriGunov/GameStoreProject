using System;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.Abstract;

namespace GameStore.Core.ConsoleSections
{
    internal class Section : ISection
    {
        public char[][] SectionMatrix;

        public Section(Position topLeftCorner, Position bottomRight)
        {
            TopLeftCorner = topLeftCorner;
            BottomRight = bottomRight;
        }

        public Position TopLeftCorner { get; set; }
        public Position BottomRight { get; set; }

        public virtual void DrawSection(IConsoleManager consoleManager)
        {
            CheckSectionState();
        }

        public void SetChar(char charToSet, int y, int x)
        {
            SectionMatrix[y][x] = charToSet;
        }

        public void SetText(string text, int y, int x)
        {
            for (var i = 0; i < text.Length; i++) SetChar(text[i], y, x + i);
        }

        private void CheckSectionState()
        {
            var width = BottomRight.X - TopLeftCorner.X;
            var heigth = BottomRight.X - TopLeftCorner.Y;

            if (width <= 0 || heigth <= 0) throw new Exception("Wrong corners of frame section.");

            if (SectionMatrix == null)
            {
                SectionMatrix = new char[heigth][];
                for (var y = 0; y < heigth; y++)
                {
                    SectionMatrix[y] = new char[width];
                    for (var x = 0; x < width; x++) SectionMatrix[y][x] = ' ';
                }
            }
        }
    }
}