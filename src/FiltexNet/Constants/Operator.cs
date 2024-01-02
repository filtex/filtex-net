using System;

namespace FiltexNet.Constants
{
    public class Operator
    {
        public static readonly Operator OperatorUnknown = new("", "");
        public static readonly Operator OperatorEqual = new("equal", "Equal");
        public static readonly Operator OperatorNotEqual = new("not-equal", "Not Equal");
        public static readonly Operator OperatorContain = new("contain", "Contain");
        public static readonly Operator OperatorNotContain = new("not-contain", "Not Contain");
        public static readonly Operator OperatorStartWith = new("start-with", "Start With");
        public static readonly Operator OperatorNotStartWith = new("not-start-with", "Not Start With");
        public static readonly Operator OperatorEndWith = new("end-with", "End With");
        public static readonly Operator OperatorNotEndWith = new("not-end-with", "Not End With");
        public static readonly Operator OperatorBlank = new("blank", "Blank");
        public static readonly Operator OperatorNotBlank = new("not-blank", "Not Blank");
        public static readonly Operator OperatorGreaterThan = new("greater-than", "Greater Than");
        public static readonly Operator OperatorGreaterThanOrEqual = new("greater-than-or-equal", "Greater Than Or Equal");
        public static readonly Operator OperatorLessThan = new("less-than", "Less Than");
        public static readonly Operator OperatorLessThanOrEqual = new("less-than-or-equal", "Less Than Or Equal");
        public static readonly Operator OperatorIn = new("in", "In");
        public static readonly Operator OperatorNotIn = new("not-in", "Not In");

        private Operator(string name, string label)
        {
            Name = name;
            Label = label;
        }

        public string Name { get; }
        public string Label { get; }

        public static Operator ParseOperator(string str)
        {
            var list = new[]
            {
                OperatorEqual,
                OperatorNotEqual,
                OperatorContain,
                OperatorNotContain,
                OperatorStartWith,
                OperatorNotStartWith,
                OperatorEndWith,
                OperatorNotEndWith,
                OperatorBlank,
                OperatorNotBlank,
                OperatorGreaterThan,
                OperatorGreaterThanOrEqual,
                OperatorLessThan,
                OperatorLessThanOrEqual,
                OperatorIn,
                OperatorNotIn
            };

            foreach (var item in list)
            {
                if (item.Equals(str))
                {
                    return item;
                }
            }

            return OperatorUnknown;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(string str)
        {
            return string.Equals(str, Name, StringComparison.InvariantCultureIgnoreCase) ||
                   string.Equals(str, Label, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}