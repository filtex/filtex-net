using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class StartWithOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeString, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var regex = inner["$regex"];
            Assert.NotNull(regex);
            Assert.Equal("^" + value, regex.AsString);

            var options = inner["$options"];
            Assert.NotNull(options);
            Assert.Equal("i", options.AsString);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumber()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeNumber, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDate()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeDate, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTime()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTime()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeDateTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = StartWithOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}