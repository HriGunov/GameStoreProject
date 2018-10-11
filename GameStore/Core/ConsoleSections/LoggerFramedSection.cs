using GameStore.Core.ConsoleSections.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.Core.ConsoleSections
{
    internal class LoggerFramedSection : FramedSection, ILoggerSection
    {
        private List<string> log;
        
        public LoggerFramedSection(Position topLeftCorner, Position bottomRight,string title ="") : base(topLeftCorner, bottomRight, title)
        {
            Log = new List<string>();
        }

        public LoggerFramedSection(int topLeftY, int topLeftX, int bottomRightY, int bottomRightX, string title = "") :
            this(new Position(topLeftY, topLeftX), new Position(bottomRightY, bottomRightX), title)
        {
        }

        public List<string> Log { get; }

        public void AddToLog(string msg)
        {
            var sectionWidth = BottomRight.X - TopLeftCorner.X - 2;

            if (msg.Length > sectionWidth)
            {
                var wordsQueue = new Queue<string>(msg.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                if (wordsQueue.Max(word => word.Length) > sectionWidth)
                {
                    throw new ArgumentException("Message is too long and in incorect format.");
                }
                else
                {
                    var sb = new StringBuilder();
                    sb.Append(">");
                    while (wordsQueue.Any())
                    {
                        if ((sb.Length + wordsQueue.Peek().Length + 1) <= sectionWidth)
                        {
                            sb.Append(" " + wordsQueue.Dequeue());
                        }
                        else
                        {
                            Log.Add(sb.ToString());
                            sb.Clear();                            
                        }
                    }
                    if (sb.Length>0)
                    {
                        Log.Add(sb.ToString());
                    }
                }
            }
            else
            {
                Log.Add(">" + msg);
            }
            Log.Add("");
        }

        public void ShowLog(IConsoleManager consoleManager)
        {
            var topMsgs = Log.TakeLast(BottomRight.Y - TopLeftCorner.Y-1);

            int lineCounter = 0;
            var sectionWidth = BottomRight.X - TopLeftCorner.X-2;
            foreach (var msg in topMsgs)
            {
                 
                consoleManager.SetText(msg.PadRight(BottomRight.X- TopLeftCorner.X-1), TopLeftCorner.Y + 1 + lineCounter, TopLeftCorner.X + 1);
                 
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