using System;

namespace FiltexNet.Exceptions
{
    public class FieldException : Exception
    {
        private const string ErrInvalidFieldType = "invalid field type";
        private const string ErrInvalidFieldName = "invalid field name";
        private const string ErrInvalidFieldLabel = "invalid field label";

        private FieldException(string message) : base(message)
        {
        }

        public static FieldException NewInvalidFieldTypeError()
        {
            return new FieldException(ErrInvalidFieldType);
        }

        public static FieldException NewInvalidFieldNameError()
        {
            return new FieldException(ErrInvalidFieldName);
        }

        public static FieldException NewInvalidFieldLabelError()
        {
            return new FieldException(ErrInvalidFieldLabel);
        }
    }
}