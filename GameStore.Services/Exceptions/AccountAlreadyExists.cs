using System;
namespace GameStore.Services.Exceptions
{
    public class AccountAlreadyExists : Exception
    {
        public AccountAlreadyExists(string message) : base(message)
        {

        }
    }
}