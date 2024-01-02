using System.Collections.Generic;
using System.Linq;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;

namespace FiltexNet.Options
{
    public class FieldOption : IOption
    {
        internal bool _isArray;
        internal bool _isNullable;
        internal string _label;
        internal string _lookup;
        internal string _name;
        internal FieldType _type;

        private FieldOption()
        {
        }

        public static FieldOption New()
        {
            return new FieldOption();
        }

        public FieldOption String()
        {
            _type = FieldType.FieldTypeString;
            return this;
        }

        public FieldOption Number()
        {
            _type = FieldType.FieldTypeNumber;
            return this;
        }

        public FieldOption Boolean()
        {
            _type = FieldType.FieldTypeBoolean;
            return this;
        }

        public FieldOption Date()
        {
            _type = FieldType.FieldTypeDate;
            return this;
        }

        public FieldOption Time()
        {
            _type = FieldType.FieldTypeTime;
            return this;
        }

        public FieldOption DateTime()
        {
            _type = FieldType.FieldTypeDateTime;
            return this;
        }

        public FieldOption Array()
        {
            _isArray = true;
            return this;
        }

        public FieldOption Nullable()
        {
            _isNullable = true;
            return this;
        }

        public FieldOption Name(string name)
        {
            _name = name;
            return this;
        }

        public FieldOption Label(string label)
        {
            _label = label;
            return this;
        }

        public FieldOption Lookup(string lookup)
        {
            _lookup = lookup;
            return this;
        }

        internal Field Build((string, Lookup[])[] lookups)
        {
            if (_type == null || _type == FieldType.FieldTypeUnknown)
            {
                throw FieldException.NewInvalidFieldTypeError();
            }

            if (string.IsNullOrEmpty(_name))
            {
                throw FieldException.NewInvalidFieldNameError();
            }

            if (string.IsNullOrEmpty(_label))
            {
                throw FieldException.NewInvalidFieldLabelError();
            }

            var fieldType = _type;
            if (_isArray)
            {
                if (_type == FieldType.FieldTypeString)
                {
                    fieldType = FieldType.FieldTypeStringArray;
                }
                else if (_type == FieldType.FieldTypeNumber)
                {
                    fieldType = FieldType.FieldTypeNumberArray;
                }
                else if (_type == FieldType.FieldTypeBoolean)
                {
                    fieldType = FieldType.FieldTypeBooleanArray;
                }
                else if (_type == FieldType.FieldTypeDate)
                {
                    fieldType = FieldType.FieldTypeDateArray;
                }
                else if (_type == FieldType.FieldTypeTime)
                {
                    fieldType = FieldType.FieldTypeTimeArray;
                }
                else if (_type == FieldType.FieldTypeDateTime)
                {
                    fieldType = FieldType.FieldTypeDateTimeArray;
                }
            }

            var fieldValues = lookups.FirstOrDefault(x => x.Item1 == _lookup).Item2 ?? System.Array.Empty<Lookup>();

            var operators = new List<Operator>();

            if (!_isArray)
            {
                operators.Add(Operator.OperatorEqual);
                operators.Add(Operator.OperatorNotEqual);
            }

            if ((fieldType == FieldType.FieldTypeNumber ||
                 fieldType == FieldType.FieldTypeNumberArray ||
                 fieldType == FieldType.FieldTypeDate ||
                 fieldType == FieldType.FieldTypeDateArray ||
                 fieldType == FieldType.FieldTypeTime ||
                 fieldType == FieldType.FieldTypeTimeArray ||
                 fieldType == FieldType.FieldTypeDateTime ||
                 fieldType == FieldType.FieldTypeDateTimeArray) && fieldValues.Length == 0)
            {
                operators.Add(Operator.OperatorGreaterThan);
                operators.Add(Operator.OperatorGreaterThanOrEqual);
                operators.Add(Operator.OperatorLessThan);
                operators.Add(Operator.OperatorLessThanOrEqual);
            }

            if (_isArray || _isNullable)
            {
                operators.Add(Operator.OperatorBlank);
                operators.Add(Operator.OperatorNotBlank);
            }

            if (_isArray)
            {
                operators.Add(Operator.OperatorContain);
                operators.Add(Operator.OperatorNotContain);
            }
            else if (fieldType == FieldType.FieldTypeString && fieldValues.Length == 0)
            {
                operators.Add(Operator.OperatorContain);
                operators.Add(Operator.OperatorNotContain);
                operators.Add(Operator.OperatorStartWith);
                operators.Add(Operator.OperatorNotStartWith);
                operators.Add(Operator.OperatorEndWith);
                operators.Add(Operator.OperatorNotEndWith);
            }

            if (!_isArray)
            {
                operators.Add(Operator.OperatorIn);
                operators.Add(Operator.OperatorNotIn);
            }

            return new Field
            {
                Name = _name,
                Type = fieldType.ToString(),
                Label = _label,
                Operators = operators.Select(x => x.ToString()).ToArray(),
                Values = fieldValues
            };
        }
    }
}