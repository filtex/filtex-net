using FiltexNet.Builders.Memory.Logics;
using FiltexNet.Builders.Memory.Types;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Logics
{
    public class AndLogicTests
    {
        [Fact]
        public void Build_ShouldReturnTrue_WhenAllExpressionsReturnTrue()
        {
            // Arrange
            var firstExpression = new MemoryExpression
            {
                Fn = _ => true
            };
            var secondExpression = new MemoryExpression
            {
                Fn = _ => true
            };
            var thirdExpression = new MemoryExpression
            {
                Fn = _ => true
            };

            var expression = AndLogic.Build(new MemoryExpression[]
            {
                firstExpression,
                secondExpression,
                thirdExpression
            });

            // Act
            var result = expression.Fn(null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenOneExpressionReturnFalse()
        {
            // Arrange
            var firstExpression = new MemoryExpression
            {
                Fn = _ => true
            };
            var secondExpression = new MemoryExpression
            {
                Fn = _ => false
            };
            var thirdExpression = new MemoryExpression
            {
                Fn = _ => true
            };

            var expression = AndLogic.Build(new MemoryExpression[]
            {
                firstExpression,
                secondExpression,
                thirdExpression
            });

            // Act
            var result = expression.Fn(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenAllExpressionsReturnFalse()
        {
            // Arrange
            var firstExpression = new MemoryExpression
            {
                Fn = _ => false
            };
            var secondExpression = new MemoryExpression
            {
                Fn = _ => false
            };
            var thirdExpression = new MemoryExpression
            {
                Fn = _ => false
            };

            var expression = AndLogic.Build(new MemoryExpression[]
            {
                firstExpression,
                secondExpression,
                thirdExpression
            });

            // Act
            var result = expression.Fn(null);

            // Assert
            Assert.False(result);
        }
    }
}