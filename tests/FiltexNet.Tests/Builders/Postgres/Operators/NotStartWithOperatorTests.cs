using FiltexNet.Builders.Postgres.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Postgres.Operators
{
    public class NotStartWithOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeString, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value NOT ILIKE $1 || '%'", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeStringArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumber()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeNumber, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeNumberArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeBoolean, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDate()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeDate, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeDateArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTime()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeTime, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeTimeArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTime()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeDateTime, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = NotStartWithOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }
    }
}