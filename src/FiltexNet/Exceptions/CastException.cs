using System;

namespace FiltexNet.Exceptions
{
    public class CastException : Exception
    {
        private const string ErrCouldNotBeCasted = "could not be casted";

        private CastException(string message) : base(message)
        {
        }

        public static CastException NewCouldNotBeCastedError()
        {
            return new CastException(ErrCouldNotBeCasted);
        }
    }
}