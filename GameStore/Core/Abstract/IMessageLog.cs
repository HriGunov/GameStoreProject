using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Core
{
    public interface IMessageLog
    {
        List<string> Log { get; set; }
        int WidthConstraint { get; set; }
        void AddToLog(string message);
    }
}
