using System;
using System.Text;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections;
using GameStore.Core.ConsoleSections.Abstract;

namespace GameStore.Core
{
    public class ConsoleManager : IConsoleManager
    {
        private readonly IMessageLog messageLog;
        private char[][] ConsoleMatrix;
        private int Heigth;
        private int Width;

        public ConsoleManager(IMessageLog messageLog)
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                Console.SetWindowSize(120, 30);
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            }

            InitializeConsoleMatrix();
            this.messageLog = messageLog;
            LoggerSection = new LoggerFramedSection(messageLog, 1, 0, 28, 35, "Message Log");
        }

        public ILoggerSection LoggerSection { get; set; }

        /// <summary>
        ///     Sets the cursor position to the bottom left of the console and adds a visual cue for waiting command.
        /// </summary>
        public string ListenForCommand()
        {
            Console.SetCursorPosition(0, Heigth - 1);
            Console.Write("Enter command -> ");
            return Console.ReadLine();
        }

        public void SetChar(char charToSet, int y, int x)
        {
            ConsoleMatrix[y][x] = charToSet;
        }

        public void SetText(string text, int y, int x)
        {
            for (var i = 0; i < text.Length; i++) SetChar(text[i], y, x + i);
        }

        /// <summary>
        ///     Clears the matrix
        /// </summary>
        public void Clear()
        {
            for (var y = 0; y < ConsoleMatrix.Length; y++)
            for (var x = 0; x < ConsoleMatrix[0].Length; x++)
                ConsoleMatrix[y][x] = ' ';
        }

        /// <summary>
        ///     Prints ConsoleMatrix on to the real Console
        /// </summary>
        public void Print()
        {
            var sb = new StringBuilder(ConsoleMatrix.Length * ConsoleMatrix[0].Length);

            for (var y = 0; y < ConsoleMatrix.Length; y++)
                foreach (var ch in ConsoleMatrix[y])
                    sb.Append(ch);

            Console.SetCursorPosition(0, 0);
            var str = sb.ToString();
            Console.Write(str);
        }

        public void LogMessage(string message, bool centered = false)
        {
            messageLog.AddToLog(message, centered);
            LoggerSection.DrawSection(this);
            Print();
        }

        /// <summary>
        ///     Used on class construction and when the console is resized
        /// </summary>
        private void InitializeConsoleMatrix()
        {
            Heigth = Console.WindowHeight;
            Width = Console.WindowWidth;

            //The heigth of the Console matrix is smaller by 1 because the last row is reserved for the command line input.
            ConsoleMatrix = new char[Heigth - 1][];
            for (var y = 0; y < Heigth - 1; y++)
            {
                ConsoleMatrix[y] = new char[Width];
                for (var x = 0; x < Width; x++) ConsoleMatrix[y][x] = ' ';
            }
        }
    }
}