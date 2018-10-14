using System.Linq;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.Abstract;

namespace GameStore.Core.ConsoleSections
{
    internal class LoggerFramedSection : FramedSection, ILoggerSection
    {
        private readonly IMessageLog messageLog;

        public LoggerFramedSection(IMessageLog messageLog) : this(messageLog, 1, 0, 28, 35, "Message Log")
        {

        }
        public LoggerFramedSection(IMessageLog messageLog, Position topLeftCorner, Position bottomRight,string title ="") : base(topLeftCorner, bottomRight, title)
        {
            this.messageLog = messageLog;
        }

        public LoggerFramedSection(IMessageLog messageLog, int topLeftY, int topLeftX, int bottomRightY,
            int bottomRightX, string title = "") :
            this(messageLog, new Position(topLeftY, topLeftX), new Position(bottomRightY, bottomRightX), title)
        {
        }


        public void ShowLog(IConsoleManager consoleManager)
        {
            var topMsgs = messageLog.Log.TakeLast(BottomRightCorner.Y - TopLeftCorner.Y-1);

            int lineCounter = 0;
            var sectionWidth = BottomRightCorner.X - TopLeftCorner.X-2;
            foreach (var msg in topMsgs)
            {
                 
                consoleManager.SetText(msg.PadRight(BottomRightCorner.X- TopLeftCorner.X-1), TopLeftCorner.Y + 1 + lineCounter, TopLeftCorner.X + 1);
                 
                lineCounter++;
            }
        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            base.DrawSection(consoleManager);
            ShowLog(consoleManager);
        }
    }
}