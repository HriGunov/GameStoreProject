using System;
namespace GameStore.Commands.Exceptions
{
    public class CommandDoesNotExist : Exception
    {
        public CommandDoesNotExist(string message) : base(message)
        {

        }
    }
}