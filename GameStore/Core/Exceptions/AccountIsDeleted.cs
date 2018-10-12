using System;

namespace GameStore.Core.Exceptions
{
    public class AccountIsDeleted : Exception
    {
        public AccountIsDeleted(string message) : base(message)
        {
        }
    }
}