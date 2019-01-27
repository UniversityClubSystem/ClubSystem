using System;

namespace ClubSystem.Lib.Exceptions
{
    public class UserNameCannotBeNullException : Exception
    {
        public UserNameCannotBeNullException()
        {
        }

        public UserNameCannotBeNullException(string message) : base(message)
        {
        }

        public UserNameCannotBeNullException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}