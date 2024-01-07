using System;
using FiltexNet.Builders.Mongo;
using FiltexNet.Builders.Mongo.Types;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;
using FiltexNet.Models;
using Xunit;

namespace FiltexNet.Tests.Builders.Mongo
{
    public class MongoFilterBuilderTests
    {
        [Fact]
        public void Build_ShouldReturnError_WhenExpressionIsNull()
        {
            // Arrange
            var builder = new MongoFilterBuilder();

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
            var builder = new MongoFilterBuilder();
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
            var builder = new MongoFilterBuilder();
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
            Assert.IsType<MongoExpression>(expression);
        }

        [Fact]
        public void Build_ShouldReturnError_WhenExpressionIsOperatorExpressionAndNotValidOperator()
        {
            // Arrange
            var builder = new MongoFilterBuilder();
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
            var builder = new MongoFilterBuilder();
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
            Assert.IsType<MongoExpression>(expression);
        }
    }
}