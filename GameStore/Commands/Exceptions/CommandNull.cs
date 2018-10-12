using System;
namespace GameStore.Commands.Exceptions
{
    public class CommandNull : Exception
    {
        public CommandNull(string message) : base(message)
        {

        }
    }
}