using System;

namespace FiltexNet.Exceptions
{
    public class LookupException : Exception
    {
        private const string ErrInvalidLookupKey = "invalid lookup key";
        private const string ErrInvalidLookupValues = "invalid lookup values";

        private LookupException(string message) : base(message)
        {
        }

        public static LookupException NewInvalidLookupKeyError()
        {
            return new LookupException(ErrInvalidLookupKey);
        }

        public static LookupException NewInvalidLookupValuesError()
        {
            return new LookupException(ErrInvalidLookupValues);
        }
    }
}