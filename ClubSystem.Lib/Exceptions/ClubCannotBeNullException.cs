using System;

namespace ClubSystem.Lib.Exceptions
{
    public class ClubCannotBeNullException : Exception
    {
        public ClubCannotBeNullException()
        {

        }

        public ClubCannotBeNullException(string message) : base(message)
        {

        }

        public ClubCannotBeNullException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}