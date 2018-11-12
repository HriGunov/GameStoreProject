using System;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
    }
}