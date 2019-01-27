using System;

namespace ClubSystem.Lib.Exceptions
{
    public class PostCannotBeNullException : Exception
    {
        public PostCannotBeNullException()
        {
        }

        public PostCannotBeNullException(string message) : base(message)
        {
        }

        public PostCannotBeNullException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}