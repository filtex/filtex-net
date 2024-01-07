using System;
using System.Collections.Generic;
using FiltexNet.Builders.Postgres.Logics;
using FiltexNet.Builders.Postgres.Operators;
using FiltexNet.Builders.Postgres.Types;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;
using FiltexNet.Models;

namespace FiltexNet.Builders.Postgres
{
    public class PostgresFilterBuilder
    {
        private readonly IDictionary<Logic, Func<PostgresExpression[], PostgresExpression>> _logicsMap = new Dictionary<Logic, Func<PostgresExpression[], PostgresExpression>>
        {
            { Logic.LogicAnd, AndLogic.Build },
            { Logic.LogicOr, OrLogic.Build }
        };
        
        private readonly IDictionary<Operator, Func<FieldType, string, object, int, PostgresExpression>> _operatorsMap = new Dictionary<Operator, Func<FieldType, string, object, int, PostgresExpression>>
        {
            { Operator.OperatorEqual, EqualOperator.Build },
            { Operator.OperatorNotEqual, NotEqualOperator.Build },
            { Operator.OperatorContain, ContainOperator.Build },
            { Operator.OperatorNotContain, NotContainOperator.Build },
            { Operator.OperatorStartWith, StartWithOperator.Build },
            { Operator.OperatorNotStartWith, NotStartWithOperator.Build },
            { Operator.OperatorEndWith, EndWithOperator.Build },
            { Operator.OperatorNotEndWith, NotEndWithOperator.Build },
            { Operator.OperatorBlank, BlankOperator.Build },
            { Operator.OperatorNotBlank, NotBlankOperator.Build },
            { Operator.OperatorGreaterThan, GreaterThanOperator.Build },
            { Operator.OperatorGreaterThanOrEqual, GreaterThanOrEqualOperator.Build },
            { Operator.OperatorLessThan, LessThanOperator.Build },
            { Operator.OperatorLessThanOrEqual, LessThanOrEqualOperator.Build },
            { Operator.OperatorIn, InOperator.Build },
            { Operator.OperatorNotIn, NotInOperator.Build }
        };

        public PostgresExpression Build(IExpression expression)
        {
            var index = 1;
            return BuildInternal(expression, ref index);
        }

        public PostgresExpression BuildInternal(IExpression expression, ref int index)
        {
            if (expression is LogicExpression logicExpression)
            {
                var expressionList = new List<PostgresExpression>();

                foreach (var ex in logicExpression.Expressions)
                {
                    var e = BuildInternal(ex, ref index);
                    expressionList.Add(e);
                }

                if (_logicsMap.TryGetValue(logicExpression.Logic, out var logicFn))
                {
                    return logicFn.Invoke(expressionList.ToArray());
                }

                throw BuildException.NewCouldNotBeBuiltError();
            }

            if (expression is OperatorExpression operatorExpression)
            {
                if (_operatorsMap.TryGetValue(operatorExpression.Operator, out var operatorFn))
                {
                    var result = operatorFn.Invoke(operatorExpression.Type, operatorExpression.Field, operatorExpression.Value, index);
                    index += result.Args.Length;
                    return result;
                }

                throw BuildException.NewCouldNotBeBuiltError();
            }

            throw BuildException.NewCouldNotBeBuiltError();
        }
    }
}