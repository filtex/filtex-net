using System;
using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo.Operators
{
    public class NotInOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var item = "Filtex";
            var value = new[]
            {
                item
            };

            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var items = nin.AsBsonArray;
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(item, items[0].AsString);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumber()
        {
            // Arrange
            var item = 100.0;
            var value = new[]
            {
                item
            };

            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeNumber, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var items = nin.AsBsonArray;
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(item, items[0].AsDouble);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDate()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var item = new DateTime(now.Year, now.Month, now.Day);
            var value = new[]
            {
                item
            };

            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeDate, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var items = nin.AsBsonArray;
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(item.ToUniversalTime(), items[0].ToUniversalTime());
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTime()
        {
            // Arrange
            var item = 60;
            var value = new[]
            {
                item
            };

            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeTime, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var items = nin.AsBsonArray;
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(item, items[0].RawValue);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTime()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var item = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var value = new[]
            {
                item
            };

            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeDateTime, "Value", value);

            // Assert
            Assert.NotNull(expression);

            var field = expression.Condition["Value"];
            Assert.NotNull(field);

            var inner = field.AsBsonDocument;
            Assert.NotNull(inner);

            var nin = inner["$nin"];
            Assert.NotNull(nin);

            var items = nin.AsBsonArray;
            Assert.NotNull(items);
            Assert.Single(items);
            Assert.Equal(item.ToUniversalTime(), items[0].ToUniversalTime());
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = NotInOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Assert
            Assert.Null(expression);
        }
    }
}