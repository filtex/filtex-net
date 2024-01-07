using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class NotEndWithOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeString, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var not = inner["$not"];
            Assert.NotNull(not);

            var notInner = not.AsBsonDocument;
            Assert.NotNull(notInner);

            var regex = notInner["$regex"];
            Assert.NotNull(regex);
            Assert.Equal(value + "$", regex.AsString);

            var options = notInner["$options"];
            Assert.NotNull(options);
            Assert.Equal("i", options.AsString);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumber()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeNumber, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDate()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeDate, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTime()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTime()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeDateTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = NotEndWithOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}