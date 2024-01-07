using System;
using FiltexNet.Builders.Memory;
using FiltexNet.Builders.Memory.Types;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;
using FiltexNet.Models;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory
{
    public class MemoryFilterBuilderTests
    {
        [Fact]
        public void Build_ShouldReturnError_WhenExpressionIsNull()
        {
            // Arrange
            var builder = new MemoryFilterBuilder();

            // Assert
            Assert.Throws<BuildException>(() =>
            {
                // Act
                var expression = builder.Build(null);

                // Assert
                Assert.Null(expression);
            });
        }

        [Fact]
        public void Build_ShouldReturnError_WhenExpressionIsLogicExpressionAndNotValidLogic()
        {
            // Arrange
            var builder = new MemoryFilterBuilder();
            var logicExpression = new LogicExpression
            {
                Logic = Logic.LogicUnknown,
                Expressions = Array.Empty<IExpression>()
            };

            // Assert
            Assert.Throws<BuildException>(() =>
            {
                // Act
                var expression = builder.Build(logicExpression);

                // Assert
                Assert.Null(expression);
            });
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenExpressionIsLogicExpressionAndValid()
        {
            // Arrange
            var builder = new MemoryFilterBuilder();
            var logicExpression = new LogicExpression
            {
                Logic = Logic.LogicAnd,
                Expressions = new IExpression[]
                {
                    new OperatorExpression
                    {
                        Type = FieldType.FieldTypeString,
                        Field = "Value",
                        Operator = Operator.OperatorEqual,
                        Value = "Filtex"
                    }
                }
            };

            // Act
            var expression = builder.Build(logicExpression);

            // Assert
            Assert.NotNull(expression);
            Assert.IsType<MemoryExpression>(expression);
        }

        [Fact]
        public void Build_ShouldReturnError_WhenExpressionIsOperatorExpressionAndNotValidOperator()
        {
            // Arrange
            var builder = new MemoryFilterBuilder();
            var operatorExpression = new OperatorExpression
            {
                Type = FieldType.FieldTypeString,
                Field = "Value",
                Operator = Operator.OperatorUnknown,
                Value = "Filtex"
            };

            // Assert
            Assert.Throws<BuildException>(() =>
            {
                // Act
                var expression = builder.Build(operatorExpression);

                // Assert
                Assert.Null(expression);
            });
        }

        [Fact]
        public void Build_ShouldReturnExpression_WhenExpressionIsOperatorExpressionAndValid()
        {
            // Arrange
            var builder = new MemoryFilterBuilder();
            var operatorExpression = new OperatorExpression
            {
                Type = FieldType.FieldTypeString,
                Field = "Value",
                Operator = Operator.OperatorEqual,
                Value = "Filtex"
            };

            // Act
            var expression = builder.Build(operatorExpression);

            // Assert
            Assert.NotNull(expression);
            Assert.IsType<MemoryExpression>(expression);
        }
    }
}