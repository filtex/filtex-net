using System;
using FiltexNet.Builders.Postgres.Operators;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Postgres.Operators
{
    public class NotContainOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsString()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeString, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("Value NOT ILIKE '%' || $1 || '%'", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsStringArray()
        {
            // Arrange
            var value = "Filtex";

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeStringArray, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("NOT (LOWER($1) = ANY (LOWER(Value::TEXT)::TEXT[]))", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            var value = 100.0;

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("NOT ($1 = ANY (Value))", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            var value = true;

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeBooleanArray, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("NOT ($1 = ANY (Value))", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateArray()
        {
            // Arrange
            var value = DateTime.Now;

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateArray, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("NOT ($1 = ANY (Value))", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            var value = 60;

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeTimeArray, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("NOT ($1 = ANY (Value))", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            var value = DateTime.Now;

            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", value, 1);

            // Assert
            Assert.NotNull(expression);
            Assert.Equal("NOT ($1 = ANY (Value))", expression.Condition);
            Assert.Single(expression.Args);
            Assert.Equal(value, expression.Args[0]);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsNumber()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumber, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsBoolean()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeBoolean, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDate()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeDate, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsTime()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeTime, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }

        [Fact]
        public void Build_ShouldReturnNull_WhenFieldTypeIsDateTime()
        {
            // Arrange
            // Act
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTime, "Value", null, 0);

            // Assert
            Assert.Null(expression);
        }
    }
}