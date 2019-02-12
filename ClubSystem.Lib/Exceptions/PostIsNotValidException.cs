using System;

namespace ClubSystem.Lib.Exceptions
{
    public class PostIsNotValidException : Exception
    {
        public PostIsNotValidException()
        {
        }

        public PostIsNotValidException(string message) : base(message)
        {
        }

        public PostIsNotValidException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}