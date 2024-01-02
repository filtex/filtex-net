using System.Collections.Generic;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Constants
{
    public class OperatorTests
    {
        [Fact]
        public void String_ShouldReturnCorrectValue()
        {
            // Arrange
            var samples = new Dictionary<Operator, string>
            {
                {
                    Operator.OperatorEqual, "equal"
                },
                {
                    Operator.OperatorNotEqual, "not-equal"
                },
                {
                    Operator.OperatorContain, "contain"
                },
                {
                    Operator.OperatorNotContain, "not-contain"
                },
                {
                    Operator.OperatorStartWith, "start-with"
                },
                {
                    Operator.OperatorNotStartWith, "not-start-with"
                },
                {
                    Operator.OperatorEndWith, "end-with"
                },
                {
                    Operator.OperatorNotEndWith, "not-end-with"
                },
                {
                    Operator.OperatorBlank, "blank"
                },
                {
                    Operator.OperatorNotBlank, "not-blank"
                },
                {
                    Operator.OperatorGreaterThan, "greater-than"
                },
                {
                    Operator.OperatorGreaterThanOrEqual, "greater-than-or-equal"
                },
                {
                    Operator.OperatorLessThan, "less-than"
                },
                {
                    Operator.OperatorLessThanOrEqual, "less-than-or-equal"
                },
                {
                    Operator.OperatorIn, "in"
                },
                {
                    Operator.OperatorNotIn, "not-in"
                }
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.Key.ToString();

                // Assert
                Assert.Equal(sample.Value, result);
            }
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenValueIsNotMatched()
        {
            // Arrange
            var samples = new Dictionary<Operator, string>
            {
                { Operator.OperatorEqual, "equals" },
                { Operator.OperatorNotEqual, "Not-Equals" },
                { Operator.OperatorContain, "contains" },
                { Operator.OperatorNotContain, "Contain" }
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.Key.Equals(sample.Value);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenValueIsMatched()
        {
            // Arrange
            var samples = new Dictionary<Operator, string>
            {
                { Operator.OperatorEqual, "equal" },
                { Operator.OperatorNotEqual, "Not-Equal" },
                { Operator.OperatorContain, "contain" },
                { Operator.OperatorNotContain, "not Contain" },
                { Operator.OperatorStartWith, "start-with" },
                { Operator.OperatorNotStartWith, "not-start-with" },
                { Operator.OperatorEndWith, "end-with" },
                { Operator.OperatorNotEndWith, "not End With" },
                { Operator.OperatorBlank, "BLANK" },
                { Operator.OperatorNotBlank, "not-blank" },
                { Operator.OperatorGreaterThan, "greater-than" },
                { Operator.OperatorGreaterThanOrEqual, "greater-than-or-equal" },
                { Operator.OperatorLessThan, "less-than" },
                { Operator.OperatorLessThanOrEqual, "less-than-or-equal" },
                { Operator.OperatorIn, "IN" },
                { Operator.OperatorNotIn, "NOT-IN" }
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.Key.Equals(sample.Value);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void ParseOperator_ShouldReturnUnknown_WhenValueIsNotMatched()
        {
            // Arrange
            var samples = new[]
            {
                "Equals",
                "Contained",
                "NotEqual"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = Operator.ParseOperator(sample);

                // Assert
                Assert.Equal(Operator.OperatorUnknown, result);
            }
        }

        [Fact]
        public void ParseOperator_ShouldReturnCorrectValue_WhenValueIsMatched()
        {
            // Arrange
            var samples = new Dictionary<string, Operator>
            {
                { "equal", Operator.OperatorEqual },
                { "Not-Equal", Operator.OperatorNotEqual },
                { "contain", Operator.OperatorContain },
                { "not Contain", Operator.OperatorNotContain },
                { "start-with", Operator.OperatorStartWith },
                { "not-start-with", Operator.OperatorNotStartWith },
                { "end-with", Operator.OperatorEndWith },
                { "not End With", Operator.OperatorNotEndWith },
                { "BLANK", Operator.OperatorBlank },
                { "not-blank", Operator.OperatorNotBlank },
                { "greater-than", Operator.OperatorGreaterThan },
                { "greater-than-or-equal", Operator.OperatorGreaterThanOrEqual },
                { "less-than", Operator.OperatorLessThan },
                { "less-than-or-equal", Operator.OperatorLessThanOrEqual },
                { "IN", Operator.OperatorIn },
                { "NOT-IN", Operator.OperatorNotIn }
            };

            foreach (var sample in samples)
            {
                // Act
                var result = Operator.ParseOperator(sample.Key);

                // Assert
                Assert.Equal(sample.Value, result);
            }
        }
    }
}