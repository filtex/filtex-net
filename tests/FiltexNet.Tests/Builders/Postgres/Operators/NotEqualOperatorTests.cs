using System;
using FiltexNet.Builders.Postgres.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Postgres.Operators
{
    public class NotEqualOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeString, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value NOT ILIKE $1", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumber()
        {
            // Arrange
            var value = 100.0;

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeNumber, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value <> $1", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsBoolean()
        {
            // Arrange
            var value = true;

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeBoolean, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value <> $1", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDate()
        {
            // Arrange
            var value = DateTime.Now;

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeDate, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value <> $1", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTime()
        {
            // Arrange
            var value = 60;

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeTime, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value <> $1", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTime()
        {
            // Arrange
            var value = DateTime.Now;

            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeDateTime, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value <> $1", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsStringArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeStringArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeNumberArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeDateArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeTimeArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            // Act
            var expression = NotEqualOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }
    }
}