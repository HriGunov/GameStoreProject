using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Core.ConsoleSections
{
    class Position
    {
        public Position(int y, int x)
        {
            Y = y;
            X = x;
        }

        public int Y { get; set; }
        public int X { get; set; }
    }
}
