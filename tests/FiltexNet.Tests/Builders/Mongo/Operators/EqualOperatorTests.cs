using System;
using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class EqualOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeString, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var regex = inner["$regex"];
            Assert.NotNull(regex);
            Assert.Equal("^" + value + "$", regex.AsString);

            var options = inner["$options"];
            Assert.NotNull(options);
            Assert.Equal("i", options.AsString);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumber()
        {
            // Arrange
            var value = 100.0;

            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeNumber, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var eq = inner["$eq"];
            Assert.NotNull(eq);
            Assert.Equal(value, eq.AsDouble);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsBoolean()
        {
            // Arrange
            var value = true;

            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeBoolean, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var eq = inner["$eq"];
            Assert.NotNull(eq);
            Assert.Equal(value, eq.AsBoolean);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDate()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);

            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeDate, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var eq = inner["$eq"];
            Assert.NotNull(eq);
            Assert.Equal(value.ToUniversalTime(), eq.ToUniversalTime());
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTime()
        {
            // Arrange
            var value = 60;

            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeTime, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var eq = inner["$eq"];
            Assert.NotNull(eq);
            Assert.Equal(value, eq.RawValue);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTime()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeDateTime, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var valueInner = expression.Condition["Value"];
            Assert.NotNull(valueInner);

            var inner = valueInner.AsBsonDocument;
            Assert.NotNull(inner);

            var eq = inner["$eq"];
            Assert.NotNull(eq);
            Assert.Equal(value.ToUniversalTime(), eq.ToUniversalTime());
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = EqualOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}