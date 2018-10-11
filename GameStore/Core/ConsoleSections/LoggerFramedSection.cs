using System.Collections.Generic;
using System.Linq;
using GameStore.Core.ConsoleSections.Abstract;

namespace GameStore.Core.ConsoleSections
{
    internal class LoggerFramedSection : FramedSection, ILoggerSection
    {
        public LoggerFramedSection(Position topLeftCorner, Position bottomRight) : base(topLeftCorner, bottomRight)
        {
            Log = new List<string>();
        }

        public LoggerFramedSection(int topLeftY, int topLeftX, int bottomRightY, int bottomRightX) :
            this(new Position(topLeftY, topLeftX), new Position(bottomRightY, bottomRightX))
        {
        }

        public List<string> Log { get; }

        public void AddToLog(string msg)
        {
            Log.Add(msg);
        }

        public void ShowLog(IConsoleManager consoleManager)
        {
            var topMsgs = Log.TakeLast(10);
            var msgCounter = 0;

            foreach (var msg in topMsgs)
            {
                if (msg.Length >= BottomRight.X - TopLeftCorner.X - 1)
                    consoleManager.SetText("Message was too long.".PadRight(BottomRight.X - TopLeftCorner.X - 1),
                        TopLeftCorner.X + 1 + msgCounter, TopLeftCorner.X + 1);
                else
                    consoleManager.SetText(msg.PadRight(BottomRight.X - TopLeftCorner.X - 1),
                        TopLeftCorner.X + 1 + msgCounter, TopLeftCorner.X + 1);
                msgCounter++;
            }
        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            base.DrawSection(consoleManager);
            ShowLog(consoleManager);
        }
    }
}