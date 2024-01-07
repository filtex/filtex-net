using System;
using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class NotContainOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeString, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var regex = inner["$regex"];
            Assert.NotNull(regex);
            Assert.Equal("^((?!" + value + ").)*$", regex.AsString);

            var options = inner["$options"];
            Assert.NotNull(options);
            Assert.Equal("i", options.AsString);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsStringArray()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeStringArray, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var values = nin.AsBsonArray;
            Assert.Single(values);
            Assert.Equal(value, values[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            var value = 100.0;

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var values = nin.AsBsonArray;
            Assert.Single(values);
            Assert.Equal(value, values[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            var value = true;

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeBooleanArray, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var values = nin.AsBsonArray;
            Assert.Single(values);
            Assert.Equal(value, values[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateArray()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateArray, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var values = nin.AsBsonArray;
            Assert.Single(values);
            Assert.Equal(value, values[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            var value = 60;

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeTimeArray, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var values = nin.AsBsonArray;
            Assert.Single(values);
            Assert.Equal(value, values[0].RawValue);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var values = nin.AsBsonArray;
            Assert.Single(values);
            Assert.Equal(value, values[0]);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumber()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumber, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDate()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeDate, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTime()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTime()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTime, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}