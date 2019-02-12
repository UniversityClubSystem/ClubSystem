using System;

namespace ClubSystem.Lib.Exceptions
{
    public class UserCannotBeNullException : Exception
    {
        public UserCannotBeNullException()
        {
        }

        public UserCannotBeNullException(string message) : base(message)
        {
        }

        public UserCannotBeNullException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}