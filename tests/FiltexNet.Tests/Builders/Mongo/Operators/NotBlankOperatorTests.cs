using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class NotBlankOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.True(exists.AsBoolean);

            var ne = inner["$ne"];
            Assert.NotNull(ne);
            Assert.Equal("", ne.AsString);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.True(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.True(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.True(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.True(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.True(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Assert
            Assert.NotNull(expression);

            var value = expression.Condition["Value.0"];
            Assert.NotNull(value);

            var inner = value.AsBsonDocument;
            Assert.NotNull(inner);

            var exists = inner["$exists"];
            Assert.NotNull(exists);
            Assert.True(exists.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumber()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeNumber, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDate()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDate, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTime()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTime()
        {
            // Arrange
            // Act
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}