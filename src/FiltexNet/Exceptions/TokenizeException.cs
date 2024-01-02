using System;

namespace FiltexNet.Exceptions
{
    public class TokenizeException : Exception
    {
        private const string ErrCouldNotBeTokenized = "could not be tokenized";

        private TokenizeException(string message) : base(message)
        {
        }

        public static TokenizeException NewCouldNotBeTokenizedError()
        {
            return new TokenizeException(ErrCouldNotBeTokenized);
        }
    }
}