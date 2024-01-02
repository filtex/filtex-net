using System;
using System.Collections.Generic;
using FiltexNet.Constants;
using FiltexNet.Models;
using FiltexNet.Tokenizers;
using Xunit;

namespace FiltexNet.Tests.Tokenizers
{
    public class TextQueryTokenizerTests
    {
        [Fact]
        public void Tokenize_ShouldReturnTokens()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
                { "", new Token[] { } },
                {
                    "Value", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        }
                    }
                },
                {
                    "Value Equal Test", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeSpace,
                            Value = " "
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeSpace,
                            Value = " "
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeValue,
                            Value = "Test"
                        }
                    }
                },
                {
                    "Value Equal Test1 Or Value Equal Test2", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeSpace,
                            Value = " "
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeSpace,
                            Value = " "
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeValue,
                            Value = "Test1"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeSpace,
                            Value = " "
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeOr,
                            Value = "Or"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeSpace,
                            Value = " "
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeSpace,
                            Value = " "
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeSpace,
                            Value = " "
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeValue,
                            Value = "Test2"
                        }
                    }
                }
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = "string",
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var textQueryTokenizer = new TextQueryTokenizer(metadata);

            foreach (var (query, tokens) in queryMap)
            {
                // Act
                var result = textQueryTokenizer.Tokenize(query);

                // Assert
                Assert.NotNull(result);

                Assert.Equal(tokens.Length, result.Length);

                for (var i = 0; i < result.Length; i++)
                {
                    var value = result[i];
                    Assert.Equal(tokens[i].Type, value.Type);
                    Assert.Equal(tokens[i].Value, value.Value);
                }
            }
        }
    }
}