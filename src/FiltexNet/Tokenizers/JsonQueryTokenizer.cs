using System;
using System.Linq;
using System.Text.Json;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;

namespace FiltexNet.Tokenizers
{
    public class JsonQueryTokenizer : BaseQueryTokenizer, IJsonQueryTokenizer
    {
        private readonly Metadata _metadata;

        public JsonQueryTokenizer(Metadata metadata) : base(metadata)
        {
            _metadata = metadata;
        }

        public object[] Tokenize(string query)
        {
            try
            {
                var data = JsonSerializer.Deserialize<object[]>(query);
                return TokenizeInternal(data);
            }
            catch (Exception)
            {
                throw TokenizeException.NewCouldNotBeTokenizedError();
            }
        }

        private object[] TokenizeInternal(object[] data)
        {
            if (data.Length == 3)
            {
                var fieldString = data[0].ToString();
                var fieldMatch = FindMatch(fieldString);
                var fieldToken = CreateToken(new Token[] { }, fieldMatch.TokenType, fieldMatch.Value);
                if (fieldToken == null || fieldToken.Type == TokenType.TokenTypeNone)
                {
                    fieldToken = new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = fieldString
                    };
                }

                var operatorString = data[1].ToString();
                var operatorMatch = FindMatch(operatorString);
                var operatorToken = CreateToken(new[] { fieldToken }, operatorMatch.TokenType, operatorMatch.Value);
                if (operatorToken == null || operatorToken.Type == TokenType.TokenTypeNone)
                {
                    operatorToken = new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = operatorString
                    };
                }

                if (data[2] is JsonElement { ValueKind: JsonValueKind.Array } array)
                {
                    var valueTokens = Array.Empty<Token>();

                    foreach (var v in array.EnumerateArray())
                    {
                        var valueString = v.ToString();
                        var valueMatch = FindMatch(valueString);


                        Token valueToken = null;

                        if (valueMatch != null)
                        {
                            if (valueString?.Length == valueMatch.Value?.Length)
                            {
                                valueToken = CreateToken(new[] { fieldToken, operatorToken }, valueMatch.TokenType, valueMatch.Value);
                            }
                            else if (_metadata.GetFieldType(fieldString) == FieldType.FieldTypeString)
                            {
                                valueToken = new Token
                                {
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = valueString
                                };
                            }
                        }
                        else if (operatorToken.Type.IsNotComparerTokenType() && valueString == "")
                        {
                            valueToken = new Token
                            {
                                Type = TokenType.TokenTypeValue,
                                Value = valueString
                            };
                        }

                        if (valueToken == null || valueToken.Type == TokenType.TokenTypeNone)
                        {
                            valueToken = new Token
                            {
                                Type = TokenType.TokenTypeNone,
                                Value = valueString
                            };
                        }

                        valueTokens = valueTokens.Append(valueToken).ToArray();
                    }

                    return new object[]
                    {
                        fieldToken,
                        operatorToken,
                        valueTokens
                    };
                }

                {
                    var valueString = Convert.ToString(data[2]);
                    var valueMatch = FindMatch(valueString);

                    Token valueToken = null;

                    if (valueMatch != null)
                    {
                        if (valueString?.Length == valueMatch.Value?.Length)
                        {
                            valueToken = CreateToken(new[] { fieldToken, operatorToken }, valueMatch.TokenType, valueMatch.Value);
                        }
                        else if (_metadata.GetFieldType(fieldString) == FieldType.FieldTypeString)
                        {
                            valueToken = new Token
                            {
                                Type = TokenType.TokenTypeStringValue,
                                Value = valueString
                            };
                        }
                    }
                    else if (operatorToken.Type.IsNotComparerTokenType() && valueString == "")
                    {
                        valueToken = new Token
                        {
                            Type = TokenType.TokenTypeValue,
                            Value = valueString
                        };
                    }

                    if (valueToken == null || valueToken.Type == TokenType.TokenTypeNone)
                    {
                        valueToken = new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = valueString
                        };
                    }

                    return new object[]
                    {
                        fieldToken,
                        operatorToken,
                        valueToken
                    };
                }
            }

            if (data.Length == 2)
            {
                var logicString = data[0].ToString();
                var logicMatch = FindMatch(logicString);

                var logicToken = new Token
                {
                    Type = TokenType.TokenTypeNone,
                    Value = logicString
                };

                var logic = Logic.ParseLogic(logicString);
                if (logic != Logic.LogicUnknown)
                {
                    logicToken = new Token
                    {
                        Type = logic.ToTokenType(),
                        Value = logicMatch.Value
                    };
                }

                if (data[1] is not JsonElement { ValueKind: JsonValueKind.Array } array)
                {
                    return new object[]
                    {
                        logicToken,
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = data[1]
                        }
                    };
                }

                var expressionList = Array.Empty<object>();

                foreach (var v in array.EnumerateArray())
                {
                    if (v is JsonElement { ValueKind: JsonValueKind.Array } itemArray)
                    {
                        var ex = TokenizeInternal(itemArray.EnumerateArray().Select(x => (object)x).ToArray());
                        expressionList = expressionList.Append(ex).ToArray();
                    }
                }

                return new object[]
                {
                    logicToken,
                    expressionList
                };
            }

            throw TokenizeException.NewCouldNotBeTokenizedError();
        }
    }
}