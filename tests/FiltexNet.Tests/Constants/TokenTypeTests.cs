using System.Collections.Generic;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Constants
{
    public class TokenTypeTests
    {
        [Fact]
        public void ToOperator_ShouldReturnUnknown_WhenValueIsNotValid()
        {
            // Arrange
            var samples = new[]
            {
                TokenType.TokenTypeNone,
                TokenType.TokenTypeComma,
                TokenType.TokenTypeSlash,
                TokenType.TokenTypeField,
                TokenType.TokenTypeValue,
                TokenType.TokenTypeStringValue,
                TokenType.TokenTypeNumberValue,
                TokenType.TokenTypeBooleanValue,
                TokenType.TokenTypeDateValue,
                TokenType.TokenTypeTimeValue,
                TokenType.TokenTypeDateTimeValue,
                TokenType.TokenTypeLiteral,
                TokenType.TokenTypeAnd,
                TokenType.TokenTypeOr,
                TokenType.TokenTypeSpace,
                TokenType.TokenTypeOpenBracket,
                TokenType.TokenTypeCloseBracket
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.ToOperator();

                // Assert
                Assert.Equal(Operator.OperatorUnknown, result);
            }
        }

        [Fact]
        public void ToOperator_ShouldReturnOperator_WhenValueIsValid()
        {
            // Arrange
            var samples = new Dictionary<TokenType, Operator>
            {
                {
                    TokenType.TokenTypeEqual,
                    Operator.OperatorEqual
                },
                {
                    TokenType.TokenTypeNotEqual,
                    Operator.OperatorNotEqual
                },
                {
                    TokenType.TokenTypeGreaterThan,
                    Operator.OperatorGreaterThan
                },
                {
                    TokenType.TokenTypeGreaterThanOrEqual,
                    Operator.OperatorGreaterThanOrEqual
                },
                {
                    TokenType.TokenTypeLessThan,
                    Operator.OperatorLessThan
                },
                {
                    TokenType.TokenTypeLessThanOrEqual,
                    Operator.OperatorLessThanOrEqual
                },
                {
                    TokenType.TokenTypeBlank,
                    Operator.OperatorBlank
                },
                {
                    TokenType.TokenTypeNotBlank,
                    Operator.OperatorNotBlank
                },
                {
                    TokenType.TokenTypeContain,
                    Operator.OperatorContain
                },
                {
                    TokenType.TokenTypeNotContain,
                    Operator.OperatorNotContain
                },
                {
                    TokenType.TokenTypeStartWith,
                    Operator.OperatorStartWith
                },
                {
                    TokenType.TokenTypeNotStartWith,
                    Operator.OperatorNotStartWith
                },
                {
                    TokenType.TokenTypeEndWith,
                    Operator.OperatorEndWith
                },
                {
                    TokenType.TokenTypeNotEndWith,
                    Operator.OperatorNotEndWith
                },
                {
                    TokenType.TokenTypeIn,
                    Operator.OperatorIn
                },
                {
                    TokenType.TokenTypeNotIn,
                    Operator.OperatorNotIn
                }
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.Key.ToOperator();

                // Assert
                Assert.Equal(sample.Value, result);
            }
        }
    }
}