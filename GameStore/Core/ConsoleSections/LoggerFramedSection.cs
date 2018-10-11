using GameStore.Core.ConsoleSections.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Core.ConsoleSections
{
    class LoggerFramedSection : FramedSection, ILoggerSection
    {
        private List<string> log;
        
        public LoggerFramedSection(Position topLeftCorner, Position bottomRigth) : base(topLeftCorner, bottomRigth)
        {
            Log = new List<string>();
        }

        public LoggerFramedSection(int topLeftY, int topLeftX, int bottomRigthY, int bottomRigthX) :
            this(new Position(topLeftY, topLeftX), new Position(bottomRigthY, bottomRigthX))
        {
        }

        public List<string> Log { get => log;  private set => log = value; }

        public void AddToLog(string msg)
        {
            Log.Add(msg);
        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            base.DrawSection(consoleManager);
            ShowLog(consoleManager);
        }
        public void ShowLog(IConsoleManager consoleManager)
        {
            var topMsgs = Log.TakeLast(10);
            int msgCounter = 0;

            foreach (var msg in topMsgs)
            {
                consoleManager.SetText(msg, TopLeftCorner.Y + 1 + msgCounter, TopLeftCorner.X + 1);
                msgCounter++;
            }
        }
    }
}
