using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Core.ConsoleSections
{
    class Section 
    {
        public char[][] SectionMatrix;

        public Section(Position topLeftCorner, Position bottomRight)
        {
            TopLeftCorner = topLeftCorner;
            BottomRight = bottomRight;
        }

        public Position TopLeftCorner { get; set; }
        public Position BottomRight { get; set; }

        private void CheckSectionState()
        {
            int width = BottomRight.X - TopLeftCorner.X;
            int heigth = BottomRight.X - TopLeftCorner.Y;

            if (width <= 0 || heigth <= 0)
            {
                throw new Exception("Wrong corners of frame section.");
            }

            if (SectionMatrix == null)
            {
                SectionMatrix = new char[heigth][];
                for (int y = 0; y < heigth ; y++)
                {
                    SectionMatrix[y] = new char[width];
                    for (int x = 0; x < width; x++)
                    {
                        SectionMatrix[y][x] = ' ';
                    }
                }
            }

        }
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
            for (int i = 0; i < text.Length; i++)
            {
                SetChar(text[i], y, x + i);
            }
        }

    }
}
