using System;
namespace GameStore.Services.Exceptions
{
    public class AccountDoesntExists : Exception
    {
        public AccountDoesntExists(string message) : base(message)
        {

        }
    }
}