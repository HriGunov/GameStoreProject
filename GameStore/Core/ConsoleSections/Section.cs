using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.Abstract;
using System;

namespace GameStore.Core.ConsoleSections
{
    internal class Section : ISection
    {
        public char[][] SectionMatrix;

        public Section(Position topLeftCorner, Position bottomRight)
        {
            TopLeftCorner = topLeftCorner;
            BottomRightCorner = bottomRight;
            Render = true;
        }

        public Position TopLeftCorner { get; set; }
        public Position BottomRightCorner { get; set; }
        public bool Render { get; set; }

        private void CheckSectionState()
        {
            var width = BottomRightCorner.X - TopLeftCorner.X;
            var heigth = BottomRightCorner.Y - TopLeftCorner.Y;

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

        private void Clear(IConsoleManager consoleManager)
        {
            var width = BottomRightCorner.X - TopLeftCorner.X;
            var heigth = BottomRightCorner.Y - TopLeftCorner.Y;

            for (var y = 0; y < heigth; y++)
            {
               
                for (var x = 0; x < width; x++) consoleManager.SetChar( ' ',TopLeftCorner.Y +y, TopLeftCorner.X + x);
            }
        }
        public virtual void DrawSection(IConsoleManager consoleManager)
        {
           // CheckSectionState();
            Clear(consoleManager);
        }

        public void SetChar(char charToSet, int y, int x)
        {
            throw new NotImplementedException();
            SectionMatrix[y][x] = charToSet;
        }

        public void SetText(string text, int y, int x)
        {
            throw new NotImplementedException();

            for (var i = 0; i < text.Length; i++) SetChar(text[i], y, x + i);
        }
    }
}