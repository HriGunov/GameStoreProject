using System;
namespace GameStore.Services.Exceptions
{
    public class NoProductsArgument : Exception
    {
        public NoProductsArgument(string message) : base(message)
        {

        }
    }
}