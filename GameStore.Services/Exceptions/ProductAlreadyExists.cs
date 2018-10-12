using System;
namespace GameStore.Services.Exceptions
{
    public class ProductAlreadyExists : Exception
    {
        public ProductAlreadyExists(string message) : base(message)
        {

        }
    }
}