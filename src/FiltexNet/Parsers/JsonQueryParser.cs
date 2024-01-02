using System;
using System.Linq;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;
using FiltexNet.Models;
using FiltexNet.Tokenizers;

namespace FiltexNet.Parsers
{
    public class JsonQueryParser
    {
        private readonly Metadata _metadata;
        private readonly IJsonQueryTokenizer _queryTokenizer;

        public JsonQueryParser(Metadata metadata, IJsonQueryTokenizer queryTokenizer)
        {
            _metadata = metadata;
            _queryTokenizer = queryTokenizer;
        }

        public IExpression Parse(string query)
        {
            var tokens = _queryTokenizer.Tokenize(query);
            return ParseInternal(tokens);
        }

        private IExpression ParseInternal(object[] data)
        {
            if (data.Length == 3)
            {
                var fieldToken = data[0] as Token;
                if (fieldToken is null)
                {
                    throw ParseException.NewCouldNotBeParsedError();
                }

                var operatorToken = data[1] as Token;
                if (operatorToken is null)
                {
                    throw ParseException.NewCouldNotBeParsedError();
                }

                object value = null;

                if (data[2] is Token[] valueTokens)
                {
                    value = valueTokens.Select(x => x.Value).ToArray();
                }
                else if (data[2] is Token valueToken)
                {
                    value = valueToken.Value;
                }
                else
                {
                    throw ParseException.NewCouldNotBeParsedError();
                }

                var op = Operator.ParseOperator(operatorToken.Type.Name);
                if (op.Name == "")
                {
                    throw ParseException.NewOperatorCouldNotBeParsedError();
                }

                return new OperatorExpression
                {
                    Type = _metadata.GetFieldType(fieldToken.Value.ToString()),
                    Field = _metadata.GetFieldName(fieldToken.Value.ToString()),
                    Operator = op,
                    Value = value
                };
            }

            if (data.Length == 2)
            {
                var logicToken = data[0] as Token;
                if (logicToken is null)
                {
                    throw ParseException.NewCouldNotBeParsedError();
                }

                var logic = Logic.ParseLogic(logicToken.Value.ToString());
                if (logic.Name == "")
                {
                    throw ParseException.NewLogicCouldNotBeParsedError();
                }

                var expressionList = Array.Empty<IExpression>();
                var array = data[1] as object[];
                if (array is null)
                {
                    throw ParseException.NewCouldNotBeParsedError();
                }

                foreach (var item in array)
                {
                    var itemArray = item as object[];

                    var ex = ParseInternal(itemArray);
                    expressionList = expressionList.Append(ex).ToArray();
                }

                return new LogicExpression
                {
                    Logic = logic,
                    Expressions = expressionList
                };
            }

            throw ParseException.NewCouldNotBeParsedError();
        }
    }
}