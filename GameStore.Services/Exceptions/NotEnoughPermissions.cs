using System;
namespace GameStore.Services.Exceptions
{
    public class NotEnoughPermissions : Exception
    {
        public NotEnoughPermissions(string message) : base(message)
        {

        }
    }
}