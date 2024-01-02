using System.Collections.Generic;
using FiltexNet.Utils;

namespace FiltexNet.Constants
{
    public class TokenType
    {
        public static readonly TokenType TokenTypeNone = new("");
        public static readonly TokenType TokenTypeOpenBracket = new("open-bracket");
        public static readonly TokenType TokenTypeCloseBracket = new("close-bracket");
        public static readonly TokenType TokenTypeAnd = new("and");
        public static readonly TokenType TokenTypeOr = new("or");
        public static readonly TokenType TokenTypeField = new("field");
        public static readonly TokenType TokenTypeValue = new("value");
        public static readonly TokenType TokenTypeEqual = new("equal");
        public static readonly TokenType TokenTypeNotEqual = new("not-equal");
        public static readonly TokenType TokenTypeGreaterThan = new("greater-than");
        public static readonly TokenType TokenTypeGreaterThanOrEqual = new("greater-than-or-equal");
        public static readonly TokenType TokenTypeLessThan = new("less-than");
        public static readonly TokenType TokenTypeLessThanOrEqual = new("less-than-or-equal");
        public static readonly TokenType TokenTypeBlank = new("blank");
        public static readonly TokenType TokenTypeNotBlank = new("not-blank");
        public static readonly TokenType TokenTypeContain = new("contain");
        public static readonly TokenType TokenTypeNotContain = new("not-contain");
        public static readonly TokenType TokenTypeStartWith = new("start-with");
        public static readonly TokenType TokenTypeNotStartWith = new("not-start-with");
        public static readonly TokenType TokenTypeEndWith = new("end-with");
        public static readonly TokenType TokenTypeNotEndWith = new("not-end-with");
        public static readonly TokenType TokenTypeIn = new("in");
        public static readonly TokenType TokenTypeNotIn = new("not-in");
        public static readonly TokenType TokenTypeComma = new("comma");
        public static readonly TokenType TokenTypeSlash = new("slash");
        public static readonly TokenType TokenTypeStringValue = new("string");
        public static readonly TokenType TokenTypeNumberValue = new("number");
        public static readonly TokenType TokenTypeBooleanValue = new("boolean");
        public static readonly TokenType TokenTypeDateValue = new("date");
        public static readonly TokenType TokenTypeTimeValue = new("time");
        public static readonly TokenType TokenTypeDateTimeValue = new("datetime");
        public static readonly TokenType TokenTypeLiteral = new("literal");
        public static readonly TokenType TokenTypeSpace = new("space");

        public TokenType(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public Operator ToOperator()
        {
            var map = new Dictionary<TokenType, Operator>
            {
                { TokenTypeEqual, Operator.OperatorEqual },
                { TokenTypeNotEqual, Operator.OperatorNotEqual },
                { TokenTypeGreaterThan, Operator.OperatorGreaterThan },
                { TokenTypeGreaterThanOrEqual, Operator.OperatorGreaterThanOrEqual },
                { TokenTypeLessThan, Operator.OperatorLessThan },
                { TokenTypeLessThanOrEqual, Operator.OperatorLessThanOrEqual },
                { TokenTypeBlank, Operator.OperatorBlank },
                { TokenTypeNotBlank, Operator.OperatorNotBlank },
                { TokenTypeContain, Operator.OperatorContain },
                { TokenTypeNotContain, Operator.OperatorNotContain },
                { TokenTypeStartWith, Operator.OperatorStartWith },
                { TokenTypeNotStartWith, Operator.OperatorNotStartWith },
                { TokenTypeEndWith, Operator.OperatorEndWith },
                { TokenTypeNotEndWith, Operator.OperatorNotEndWith },
                { TokenTypeIn, Operator.OperatorIn },
                { TokenTypeNotIn, Operator.OperatorNotIn }
            };

            if (map.TryGetValue(this, out var op))
            {
                return op;
            }

            return Operator.OperatorUnknown;
        }

        public bool IsFieldTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeField
            });
        }

        public bool IsOperatorTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeEqual,
                TokenTypeNotEqual,
                TokenTypeBlank,
                TokenTypeNotBlank,
                TokenTypeLessThan,
                TokenTypeLessThanOrEqual,
                TokenTypeGreaterThan,
                TokenTypeGreaterThanOrEqual,
                TokenTypeContain,
                TokenTypeNotContain,
                TokenTypeStartWith,
                TokenTypeNotStartWith,
                TokenTypeEndWith,
                TokenTypeNotEndWith,
                TokenTypeIn,
                TokenTypeNotIn
            });
        }

        public bool IsComparerTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeEqual,
                TokenTypeNotEqual,
                TokenTypeLessThan,
                TokenTypeLessThanOrEqual,
                TokenTypeGreaterThan,
                TokenTypeGreaterThanOrEqual,
                TokenTypeContain,
                TokenTypeNotContain,
                TokenTypeStartWith,
                TokenTypeNotStartWith,
                TokenTypeEndWith,
                TokenTypeNotEndWith,
                TokenTypeIn,
                TokenTypeNotIn
            });
        }

        public bool IsNotComparerTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeBlank,
                TokenTypeNotBlank
            });
        }

        public bool IsSeparatorTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeComma,
                TokenTypeSlash
            });
        }

        public bool IsPreFieldTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeOpenBracket,
                TokenTypeAnd,
                TokenTypeOr
            });
        }

        public bool IsValueTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeValue,
                TokenTypeStringValue,
                TokenTypeNumberValue,
                TokenTypeBooleanValue,
                TokenTypeDateValue,
                TokenTypeTimeValue,
                TokenTypeDateTimeValue
            });
        }

        public bool IsLogicTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeAnd,
                TokenTypeOr
            });
        }

        public bool IsOpenGroupTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeOpenBracket
            });
        }

        public bool IsCloseGroupTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeCloseBracket
            });
        }

        public bool IsMultiAllowedTokenType()
        {
            return ArrayUtils.IsInAny(this, new[]
            {
                TokenTypeIn,
                TokenTypeNotIn
            });
        }
    }
}