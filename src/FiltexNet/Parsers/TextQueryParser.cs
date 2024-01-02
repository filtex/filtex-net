using System;
using System.Linq;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;
using FiltexNet.Models;
using FiltexNet.Tokenizers;

namespace FiltexNet.Parsers
{
    public class TextQueryParser
    {
        private readonly Metadata _metadata;
        private readonly ITextQueryTokenizer _queryTokenizer;

        public TextQueryParser(Metadata metadata, ITextQueryTokenizer queryTokenizer)
        {
            _metadata = metadata;
            _queryTokenizer = queryTokenizer;
        }

        public IExpression Parse(string query)
        {
            var tokens = _queryTokenizer.Tokenize(query);

            var result = Array.Empty<object>();
            var parsed = ParseTokens(ref tokens, result);
            return ParseExpression(parsed);
        }

        private object[] ParseTokens(ref Token[] queue, object[] result, bool isValueExpected = false)
        {
            while (queue.Length > 0)
            {
                var token = queue[0];
                queue = queue.Skip(1).ToArray();

                if (token.Type == TokenType.TokenTypeSpace)
                {
                    continue;
                }

                if (token.Type.IsFieldTokenType())
                {
                    result = result.Append(token).ToArray();
                }
                else if (token.Type.IsComparerTokenType())
                {
                    result = result.Append(token).ToArray();
                }
                else if (token.Type.IsNotComparerTokenType())
                {
                    result = result.Append(token).ToArray();
                    result = result.Append(new Token { Type = TokenType.TokenTypeValue, Value = "" }).ToArray();

                    if (isValueExpected)
                    {
                        return result;
                    }
                }
                else if (token.Type.IsValueTokenType())
                {
                    if (result.Length > 2 && result[2].GetType().IsArray)
                    {
                        var inner = result[2] as object[];
                        inner = inner.Append(token).ToArray();
                        result[2] = inner;
                    }
                    else
                    {
                        result = result.Append(token).ToArray();
                    }

                    if (isValueExpected)
                    {
                        return result;
                    }
                }
                else if (token.Type.IsLogicTokenType())
                {
                    var logicInner = Array.Empty<object>();
                    var logicResult = ParseTokens(ref queue, logicInner, true);
                    var newResult = Array.Empty<object>();
                    newResult = newResult.Append(token).ToArray();
                    newResult = newResult.Append(new object[]
                    {
                        result,
                        logicResult
                    }).ToArray();
                    result = newResult;
                }
                else if (token.Type.IsSeparatorTokenType())
                {
                    if (result.Length > 2 && result[2].GetType().IsArray)
                    {
                        var inner = result[2] as object[];
                        var newResult = Array.Empty<object>();
                        foreach (var item in inner)
                        {
                            newResult = newResult.Append(item).ToArray();
                        }
                        result[2] = newResult;
                    }
                    else
                    {
                        var inner = Array.Empty<object>();
                        inner = inner.Append(result[2]).ToArray();
                        result[2] = inner;
                    }
                }
                else if (token.Type.IsOpenGroupTokenType())
                {
                    var bracketInner = Array.Empty<object>();
                    result = ParseTokens(ref queue, bracketInner);
                }
                else if (token.Type.IsCloseGroupTokenType())
                {
                    return result;
                }
                else
                {
                    result = result.Append(token).ToArray();
                }
            }

            return result;
        }

        private IExpression ParseExpression(object[] data)
        {
            if (data.Length == 3)
            {
                var fieldToken = data[0] as Token;
                var operatorToken = data[1] as Token;

                object value = null;

                if (data[2] is object[] valueTokens)
                {
                    value = valueTokens.Cast<Token>().Select(x => x.Value).ToArray();
                }
                else if (data[2] is Token valueToken)
                {
                    value = valueToken.Value;
                }

                var op = Operator.ParseOperator(operatorToken?.Type.Name);
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

                var logic = Logic.ParseLogic(logicToken.Value.ToString());
                if (logic.Name == "")
                {
                    throw ParseException.NewLogicCouldNotBeParsedError();
                }

                var expressionList = Array.Empty<IExpression>();
                var array = data[1] as object[];

                foreach (var item in array)
                {
                    var itemArray = item as object[];

                    var ex = ParseExpression(itemArray);
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