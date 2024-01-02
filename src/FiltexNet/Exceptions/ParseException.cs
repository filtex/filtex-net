using System;

namespace FiltexNet.Exceptions
{
    public class ParseException : Exception
    {
        private const string ErrOperatorCouldNotBeParsed = "invalid operator";
        private const string ErrLogicCouldNotBeParsed = "invalid logic";
        private const string ErrCouldNotBeParsed = "could not be parsed";

        private ParseException(string message) : base(message)
        {
        }

        public static ParseException NewOperatorCouldNotBeParsedError()
        {
            return new ParseException(ErrOperatorCouldNotBeParsed);
        }

        public static ParseException NewLogicCouldNotBeParsedError()
        {
            return new ParseException(ErrLogicCouldNotBeParsed);
        }

        public static ParseException NewCouldNotBeParsedError()
        {
            return new ParseException(ErrCouldNotBeParsed);
        }
    }
}