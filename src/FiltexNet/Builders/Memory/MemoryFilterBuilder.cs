using System;
using System.Collections.Generic;
using FiltexNet.Builders.Memory.Logics;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Types;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;
using FiltexNet.Models;

namespace FiltexNet.Builders.Memory
{
    public class MemoryFilterBuilder
    {
        private readonly IDictionary<Logic, Func<MemoryExpression[], MemoryExpression>> _logicsMap = new Dictionary<Logic, Func<MemoryExpression[], MemoryExpression>>
        {
            { Logic.LogicAnd, AndLogic.Build },
            { Logic.LogicOr, OrLogic.Build }
        };
        
        private readonly IDictionary<Operator, Func<FieldType, string, object, MemoryExpression>> _operatorsMap = new Dictionary<Operator, Func<FieldType, string, object, MemoryExpression>>
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

        public MemoryExpression Build(IExpression expression)
        {
            if (expression is LogicExpression logicExpression)
            {
                var expressionList = new List<MemoryExpression>();

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