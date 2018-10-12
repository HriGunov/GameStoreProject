using System;
namespace GameStore.Services.Exceptions
{
    public class GuestCannotAddToCart : Exception
    {
        public GuestCannotAddToCart(string message) : base(message)
        {

        }
    }
}