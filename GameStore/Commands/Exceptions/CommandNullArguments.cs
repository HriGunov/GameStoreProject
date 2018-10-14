using System;

namespace GameStore.Commands.Exceptions
{
    public class CommandNullArguments : Exception
    {
        public CommandNullArguments(string message) : base(message)
        {
        }
    }
}