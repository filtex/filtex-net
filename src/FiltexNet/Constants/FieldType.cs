using System.Collections.Generic;

namespace FiltexNet.Constants
{
    public class FieldType
    {
        public static readonly FieldType FieldTypeUnknown = new("");
        public static readonly FieldType FieldTypeString = new("string");
        public static readonly FieldType FieldTypeNumber = new("number");
        public static readonly FieldType FieldTypeBoolean = new("boolean");
        public static readonly FieldType FieldTypeDate = new("date");
        public static readonly FieldType FieldTypeTime = new("time");
        public static readonly FieldType FieldTypeDateTime = new("datetime");
        public static readonly FieldType FieldTypeStringArray = new("string-array");
        public static readonly FieldType FieldTypeNumberArray = new("number-array");
        public static readonly FieldType FieldTypeBooleanArray = new("boolean-array");
        public static readonly FieldType FieldTypeDateArray = new("date-array");
        public static readonly FieldType FieldTypeTimeArray = new("time-array");
        public static readonly FieldType FieldTypeDateTimeArray = new("datetime-array");

        public FieldType(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static FieldType ParseFieldType(string type)
        {
            var map = new Dictionary<string, FieldType>
            {
                { FieldTypeString.Name, FieldTypeString },
                { FieldTypeNumber.Name, FieldTypeNumber },
                { FieldTypeBoolean.Name, FieldTypeBoolean },
                { FieldTypeDate.Name, FieldTypeDate },
                { FieldTypeTime.Name, FieldTypeTime },
                { FieldTypeDateTime.Name, FieldTypeDateTime },
                { FieldTypeStringArray.Name, FieldTypeStringArray },
                { FieldTypeNumberArray.Name, FieldTypeNumberArray },
                { FieldTypeBooleanArray.Name, FieldTypeBooleanArray },
                { FieldTypeDateArray.Name, FieldTypeDateArray },
                { FieldTypeTimeArray.Name, FieldTypeTimeArray },
                { FieldTypeDateTimeArray.Name, FieldTypeDateTimeArray }
            };

            if (map.TryGetValue(type, out var fieldType))
            {
                return fieldType;
            }

            return FieldTypeUnknown;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool IsArray()
        {
            return Name == FieldTypeStringArray.Name ||
                   Name == FieldTypeNumberArray.Name ||
                   Name == FieldTypeBooleanArray.Name ||
                   Name == FieldTypeDateArray.Name ||
                   Name == FieldTypeTimeArray.Name ||
                   Name == FieldTypeDateTimeArray.Name;
        }
    }
}