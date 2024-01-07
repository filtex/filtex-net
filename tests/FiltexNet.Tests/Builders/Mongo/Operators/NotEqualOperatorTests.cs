using System;
using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class NotEqualOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeString, "Value", value);

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
            Assert.Equal("^" + value + "$", regex.AsString);

            var options = notInner["$options"];
            Assert.NotNull(options);
            Assert.Equal("i", options.AsString);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumber()
        {
            // Arrange
            var value = 100.0;

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeNumber, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var ne = inner["$ne"];
            Assert.NotNull(ne);
            Assert.Equal(value, ne.AsDouble);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsBoolean()
        {
            // Arrange
            var value = true;

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeBoolean, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var ne = inner["$ne"];
            Assert.NotNull(ne);
            Assert.Equal(value, ne.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDate()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeDate, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var ne = inner["$ne"];
            Assert.NotNull(ne);
            Assert.Equal(value.ToUniversalTime(), ne.ToUniversalTime());
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTime()
        {
            // Arrange
            var value = 60;

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeTime, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var ne = inner["$ne"];
            Assert.NotNull(ne);
            Assert.Equal(value, ne.RawValue);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTime()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeDateTime, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var ne = inner["$ne"];
            Assert.NotNull(ne);
            Assert.Equal(value.ToUniversalTime(), ne.ToUniversalTime());
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}