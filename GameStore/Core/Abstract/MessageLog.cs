using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.Core.Abstract
{
    public class MessageLog : IMessageLog
    {
        public MessageLog()
        {
            Log = new List<string>();
        }
        public List<string> Log { get; set ; }

        public int WidthConstraint { get; set; }

        public void AddToLog(string message)
        {

            if (message.Length > WidthConstraint)
            {
                var wordsQueue = new Queue<string>(message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                if (wordsQueue.Max(word => word.Length) > WidthConstraint)
                {
                    throw new ArgumentException("Message is too long and in incorect format.");
                }
                else
                {
                    var sb = new StringBuilder();
                    sb.Append(">");
                    while (wordsQueue.Any())
                    {
                        if ((sb.Length + wordsQueue.Peek().Length + 1) <= WidthConstraint)
                        {
                            sb.Append(" " + wordsQueue.Dequeue());
                        }
                        else
                        {
                            Log.Add(sb.ToString());
                            sb.Clear();
                        }
                    }
                    if (sb.Length > 0)
                    {
                        Log.Add(sb.ToString());
                    }
                }
            }
            else
            {
                Log.Add(">" + message);
            }
            Log.Add("");
        }
    }
}
