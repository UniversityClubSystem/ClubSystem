using System;

namespace ClubSystem.Lib.Exceptions
{
    public class ClubIsNotValidException : Exception
    {
        public ClubIsNotValidException()
        {
        }

        public ClubIsNotValidException(string message) : base(message)
        {
        }

        public ClubIsNotValidException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}