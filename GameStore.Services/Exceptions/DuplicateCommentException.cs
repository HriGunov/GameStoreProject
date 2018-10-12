using System;
namespace GameStore.Services.Exceptions
{
    public class DuplicateCommentException : Exception
    {
        public DuplicateCommentException(string message) : base(message)
        {

        }
    }
}