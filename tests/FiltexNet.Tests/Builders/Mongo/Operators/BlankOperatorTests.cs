using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class BlankOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.True(exists.AsBoolean);

            var eq = inner["$eq"];
            Assert.NotNull(eq);
            Assert.Equal("", eq.AsString);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.False(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.False(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.False(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.False(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.False(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.False(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumber()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeNumber, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDate()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeDate, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTime()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTime()
        {
            // Arrange
            // Act
            var expression = BlankOperator.Build(FieldType.FieldTypeDateTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}