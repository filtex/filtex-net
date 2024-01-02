using System.Collections.Generic;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Constants
{
    public class LogicTests
    {
        [Fact]
        public void ToTokenType_ShouldReturnNoneToken_WhenValueIsNotValid()
        {
            // Arrange
            var samples = new[]
            {
                Logic.LogicUnknown
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.ToTokenType();

                // Assert
                Assert.Equal(TokenType.TokenTypeNone, result);
            }
        }

        [Fact]
        public void ToTokenType_ShouldReturnToken_WhenValueIsValid()
        {
            // Arrange
            var samples = new Dictionary<Logic, TokenType>
            {
                {
                    Logic.LogicAnd,
                    TokenType.TokenTypeAnd
                },
                {
                    Logic.LogicOr,
                    TokenType.TokenTypeOr
                }
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.Key.ToTokenType();

                // Assert
                Assert.Equal(sample.Value, result);
            }
        }

        [Fact]
        public void ParseLogic_ShouldReturnUnknown_WhenValueIsNotValid()
        {
            // Arrange
            var samples = new[]
            {
                "",
                "test"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = Logic.ParseLogic(sample);

                // Assert
                Assert.Equal(Logic.LogicUnknown, result);
            }
        }

        [Fact]
        public void ParseLogic_ShouldReturnLogic_WhenValueIsValid()
        {
            // Arrange
            var samples = new Dictionary<string, Logic>
            {
                { "and", Logic.LogicAnd },
                { "And", Logic.LogicAnd },
                { "AND", Logic.LogicAnd },
                { "or", Logic.LogicOr },
                { "Or", Logic.LogicOr },
                { "OR", Logic.LogicOr }
            };

            foreach (var sample in samples)
            {
                // Act
                var result = Logic.ParseLogic(sample.Key);

                // Assert
                Assert.Equal(sample.Value, result);
            }
        }
    }
}