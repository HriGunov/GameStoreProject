using System;
namespace GameStore.Services.Exceptions
{
    public class ProductDoesntExists : Exception
    {
        public ProductDoesntExists(string message) : base(message)
        {

        }
    }
}