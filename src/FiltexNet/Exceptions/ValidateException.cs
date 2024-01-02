using System;

namespace FiltexNet.Exceptions
{
    public class ValidateException : Exception
    {
        private const string ErrInvalidField = "invalid field";
        private const string ErrInvalidOperator = "invalid operator";
        private const string ErrInvalidValue = "invalid value";
        private const string ErrInvalidLogic = "invalid logic";
        private const string ErrInvalidToken = "invalid token";
        private const string ErrInvalidLastToken = "invalid last token";
        private const string ErrMismatchedBrackets = "mismatched brackets";
        private const string ErrCouldNotBeValidated = "could not be validated";

        private ValidateException(string message) : base(message)
        {
        }

        public static ValidateException NewInvalidFieldError()
        {
            return new ValidateException(ErrInvalidField);
        }

        public static ValidateException NewInvalidOperatorError()
        {
            return new ValidateException(ErrInvalidOperator);
        }

        public static ValidateException NewInvalidValueError()
        {
            return new ValidateException(ErrInvalidValue);
        }

        public static ValidateException NewInvalidLogicError()
        {
            return new ValidateException(ErrInvalidLogic);
        }

        public static ValidateException NewInvalidTokenError()
        {
            return new ValidateException(ErrInvalidToken);
        }

        public static ValidateException NewInvalidLastTokenError()
        {
            return new ValidateException(ErrInvalidLastToken);
        }

        public static ValidateException NewMismatchedBracketsError()
        {
            return new ValidateException(ErrMismatchedBrackets);
        }

        public static ValidateException NewCouldNotBeValidatedError()
        {
            return new ValidateException(ErrCouldNotBeValidated);
        }
    }
}