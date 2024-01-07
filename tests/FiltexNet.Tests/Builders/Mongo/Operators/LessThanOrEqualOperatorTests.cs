using System;
using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class LessThanOrEqualOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumber()
        {
            // Arrange
            var value = 100.0;

            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeNumber, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var lte = inner["$lte"];
            Assert.NotNull(lte);
            Assert.Equal(value, lte.AsDouble);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDate()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);

            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeDate, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var lte = inner["$lte"];
            Assert.NotNull(lte);
            Assert.Equal(value.ToUniversalTime(), lte.ToUniversalTime());
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTime()
        {
            // Arrange
            var value = 60;

            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeTime, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var lte = inner["$lte"];
            Assert.NotNull(lte);
            Assert.Equal(value, lte.RawValue);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTime()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeDateTime, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var lte = inner["$lte"];
            Assert.NotNull(lte);
            Assert.Equal(value.ToUniversalTime(), lte.ToUniversalTime());
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsString()
        {
            // Arrange
            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = LessThanOrEqualOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}