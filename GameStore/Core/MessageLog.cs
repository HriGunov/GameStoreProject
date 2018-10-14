using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStore.Core.Abstract;

namespace GameStore.Core
{
    public class MessageLog : IMessageLog
    {
        public MessageLog()
        {
            Log = new List<string>();
        }

        public List<string> Log { get; set; }

        public int WidthConstraint { get; set; } = 34;

        public void AddToLog(string message, bool centered)
        {
            if (message.Length + 1 > WidthConstraint)
            {
                var wordsQueue = new Queue<string>(message.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries));
                if (wordsQueue.Max(word => word.Length) > WidthConstraint)
                    throw new ArgumentException("Message is too long.");

                var sb = new StringBuilder();
                sb.Append(">");
                while (wordsQueue.Any())
                    if (sb.Length + wordsQueue.Peek().Length + 1 <= WidthConstraint)
                    {
                        sb.Append(" " + wordsQueue.Dequeue());
                    }
                    else
                    {
                        Log.Add(sb.ToString());
                        sb.Clear();
                    }

                if (sb.Length > 0) Log.Add(sb.ToString());
            }
            else
            {
                if (centered)
                {
                    var sb = new StringBuilder();
                    sb.Append(">");
                    var leftEdgeLength = (WidthConstraint - 1) / 2 - message.Length / 2;
                    sb.Append(new string(' ', leftEdgeLength));
                    sb.Append(message);
                    Log.Add(sb.ToString());
                }
                else
                {
                    Log.Add("> " + message);
                }
            }

            Log.Add("");
        }
    }
}