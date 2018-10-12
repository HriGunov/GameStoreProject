using System;

namespace GameStore.Services.Exceptions
{
    public class UsernameInvalidFormat : Exception
    {
        public UsernameInvalidFormat(string message) : base(message)
        {
        }
    }
}