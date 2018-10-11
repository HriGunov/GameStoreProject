using GameStore.Core.Abstract;
using System;
using System.Text;

namespace GameStore.Core
{
    public class ConsoleManager : IConsoleManager
    {


        private char[][] ConsoleMatrix;
        private int Width;
        private int Heigth;
        private readonly IEngine engine;

        public ConsoleManager(IEngine engine)
        {
            Console.SetWindowSize(120, 30);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            InitializeConsoleMatrix();
            this.engine = engine;
        }

        /// <summary>
        /// Sets the cursor position to the bottom left of the console and adds a visual cue for waiting command.
        /// </summary>
        public string ListenForCommand()
        {
            Console.SetCursorPosition(0, Heigth - 1);
            Console.Write("Enter action -> ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Used on class construction and when the console is resized
        /// </summary>
        private void InitializeConsoleMatrix()
        {
            Heigth = Console.WindowHeight;
            Width = Console.WindowWidth;

            //The heigth of the Console matrix is smaller by 1 because the last row is reserved for the command line input.
            ConsoleMatrix = new char[Heigth - 1][];
            for (int y = 0; y < Heigth - 1; y++)
            {
                ConsoleMatrix[y] = new char[Width];
                for (int x = 0; x < Width; x++)
                {
                    ConsoleMatrix[y][x] = ' ';
                }
            }
        }


        public void SetChar(char charToSet, int y, int x)
        {
            ConsoleMatrix[y][x] = charToSet;
        }
        public void SetText(string text, int y, int x)
        {
            for (int i = 0; i < text.Length; i++)
            {
                SetChar(text[i], y, x + i);
            }
        }

        /// <summary>
        /// Clears the matrix
        /// </summary>
        public void Clear()
        {

            for (int y = 0; y < ConsoleMatrix.Length; y++)
            {
                for (int x = 0; x < ConsoleMatrix[0].Length; x++)
                {
                    ConsoleMatrix[y][x] = ' ';
                }
            }
        }

        /// <summary>
        /// Prints ConsoleMatrix on to the real Console
        /// </summary>
        public void Print()
        {
            var sb = new StringBuilder(ConsoleMatrix.Length * ConsoleMatrix[0].Length);

            for (int y = 0; y < ConsoleMatrix.Length; y++)
            {

                foreach (var ch in ConsoleMatrix[y])
                {
                    sb.Append(ch);
                }
            }

            Console.SetCursorPosition(0, 0);
            var str = sb.ToString();
            Console.Write(str);
        }

    }
}
