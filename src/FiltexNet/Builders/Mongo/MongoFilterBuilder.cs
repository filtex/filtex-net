using System;
using System.Collections.Generic;
using FiltexNet.Builders.Mongo.Logics;
using FiltexNet.Builders.Mongo.Operators;
using FiltexNet.Builders.Mongo.Types;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;
using FiltexNet.Models;

namespace FiltexNet.Builders.Mongo
{
    public class MongoFilterBuilder
    {
        private readonly IDictionary<Logic, Func<MongoExpression[], MongoExpression>> _logicsMap = new Dictionary<Logic, Func<MongoExpression[], MongoExpression>>
        {
            { Logic.LogicAnd, AndLogic.Build },
            { Logic.LogicOr, OrLogic.Build }
        };
        
        private readonly IDictionary<Operator, Func<FieldType, string, object, MongoExpression>> _operatorsMap = new Dictionary<Operator, Func<FieldType, string, object, MongoExpression>>
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

        public MongoExpression Build(IExpression expression)
        {
            if (expression is LogicExpression logicExpression)
            {
                var expressionList = new List<MongoExpression>();

                foreach (var ex in logicExpression.Expressions)
                {
                    var e = Build(ex);
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
                    return operatorFn.Invoke(operatorExpression.Type, operatorExpression.Field, operatorExpression.Value);
                }

                throw BuildException.NewCouldNotBeBuiltError();
            }

            throw BuildException.NewCouldNotBeBuiltError();
        }
    }
}