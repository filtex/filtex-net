using FiltexNet.Builders.Postgres.Logics;
using FiltexNet.Builders.Postgres.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Postgres.Logics
{
    public class OrLogicTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenThereAreExpressions()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = OrLogic.Build(new[]
            {
                EqualOperator.Build(FieldType.FieldTypeString, "Value", value, 0)
            });

            // Assert
            Assert.NotNull(expression);
            Assert.NotEmpty(expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }
    }
}