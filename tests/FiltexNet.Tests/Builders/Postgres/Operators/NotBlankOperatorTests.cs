using FiltexNet.Builders.Postgres.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Postgres.Operators
{
    public class NotBlankOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeString, "Value", null, 0);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value IS NOT NULL AND Value <> ''", expression.Condition);
            Assert.Empty(expression.Args);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeStringArray, "Value", null, 0);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("ARRAY_LENGTH(Value, 1) <> 0", expression.Condition);
            Assert.Empty(expression.Args);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeNumberArray, "Value", null, 0);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("ARRAY_LENGTH(Value, 1) <> 0", expression.Condition);
            Assert.Empty(expression.Args);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null, 0);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("ARRAY_LENGTH(Value, 1) <> 0", expression.Condition);
            Assert.Empty(expression.Args);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateArray, "Value", null, 0);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("ARRAY_LENGTH(Value, 1) <> 0", expression.Condition);
            Assert.Empty(expression.Args);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeTimeArray, "Value", null, 0);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("ARRAY_LENGTH(Value, 1) <> 0", expression.Condition);
            Assert.Empty(expression.Args);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null, 0);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("ARRAY_LENGTH(Value, 1) <> 0", expression.Condition);
            Assert.Empty(expression.Args);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumber()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeNumber, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeBoolean, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDate()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDate, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTime()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeTime, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTime()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateTime, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }
    }
}