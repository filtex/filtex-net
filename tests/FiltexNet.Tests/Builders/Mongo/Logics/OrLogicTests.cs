using FiltexNet.Builders.Mongo.Logics;
using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Logics
{
    public class OrLogicTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenThereAreExpressions()
        {
            // Arrange
            // Act
            var expression = OrLogic.Build(new[]
            {
                EqualOperator.Build(FieldType.FieldTypeString, "Value", "Filtex")
            });

            // Assert
            Assert.NotNull(expression);
            Assert.NotNull(expression.Condition);

            var value = expression.Condition["$or"];
            Assert.NotNull(value);
            var array = value.AsBsonArray;
            Assert.Single(array);
        }
    }
}