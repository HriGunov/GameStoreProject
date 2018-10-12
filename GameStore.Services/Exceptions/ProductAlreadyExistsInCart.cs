using System;
namespace GameStore.Services.Exceptions
{
    public class ProductAlreadyExistsInCart : Exception
    {
        public ProductAlreadyExistsInCart(string message) : base(message)
        {

        }
    }
}