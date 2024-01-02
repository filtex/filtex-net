using System;
using System.Collections.Generic;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;
using FiltexNet.Tokenizers;
using Xunit;

namespace FiltexNet.Tests.Tokenizers
{
    public class JsonQueryTokenizerTests
    {
        [Fact]
        public void Tokenize_ShouldReturnError_WhenQueryIsNotJson()
        {
            // Arrange
            var query = "some_text";

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

            var jsonQueryTokenizer = new JsonQueryTokenizer(metadata);

            Assert.Throws<TokenizeException>(() =>
            {
                // Act
                var result = jsonQueryTokenizer.Tokenize(query);

                // Assert
                Assert.Null(result);
            });
        }

        [Fact]
        public void Tokenize_ShouldReturnError_WhenQueryLengthIsNotValid()
        {
            // Arrange
            var queries = new[]
            {
                "[]",
                "[\"Value\"]",
                "[\"Value\", \"Equal\", \"Filtex\", \"Go\"]"
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

            var jsonQueryTokenizer = new JsonQueryTokenizer(metadata);

            foreach (var query in queries)
            {
                Assert.Throws<TokenizeException>(() =>
                {
                    // Act
                    var result = jsonQueryTokenizer.Tokenize(query);

                    // Assert
                    Assert.Null(result);
                });
            }
        }

        [Fact]
        public void Tokenize_ShouldReturnTokens_WhenQueryLengthIsThree()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Value\", \"Equal\", \"Test\"]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeValue,
                            Value = "Test"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", 100]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeNumberValue,
                            Value = "100"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", true]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeBooleanValue,
                            Value = "True"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", \"2020-01-01\"]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeDateValue,
                            Value = "2020-01-01"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", \"10:12:14\"]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeTimeValue,
                            Value = "10:12:14"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", \"2020-01-01 11:12:13\"]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeDateTimeValue,
                            Value = "2020-01-01 11:12:13"
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

            var jsonQueryTokenizer = new JsonQueryTokenizer(metadata);

            foreach (var (query, tokens) in queryMap)
            {
                // Act
                var result = jsonQueryTokenizer.Tokenize(query);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(((Token)tokens[0]).Type, ((Token)result[0]).Type);
                Assert.Equal(((Token)tokens[0]).Value, ((Token)result[0]).Value);
                Assert.Equal(((Token)tokens[1]).Type, ((Token)result[1]).Type);
                Assert.Equal(((Token)tokens[1]).Value, ((Token)result[1]).Value);
                Assert.Equal(((Token)tokens[2]).Type, ((Token)result[2]).Type);
                Assert.Equal(((Token)tokens[2]).Value, ((Token)result[2]).Value);
            }
        }

        [Fact]
        public void Tokenize_ShouldReturnTokens_WhenQueryLengthIsTwo()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Or\", [[\"Value\", \"Equal\", 100], [\"Value\", \"Equal\", 200]]]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeOr,
                            Value = "Or"
                        },
                        new[]
                        {
                            new[]
                            {
                                new Token
                                {
                                    Type = TokenType.TokenTypeField,
                                    Value = "Value"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeEqual,
                                    Value = "Equal"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeNumberValue,
                                    Value = "100"
                                }
                            },
                            new[]
                            {
                                new Token
                                {
                                    Type = TokenType.TokenTypeField,
                                    Value = "Value"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeEqual,
                                    Value = "Equal"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeNumberValue,
                                    Value = "200"
                                }
                            }
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

            var jsonQueryTokenizer = new JsonQueryTokenizer(metadata);

            foreach (var (query, tokens) in queryMap)
            {
                // Act
                var result = jsonQueryTokenizer.Tokenize(query);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(((Token)tokens[0]).Type, ((Token)result[0]).Type);
                Assert.Equal(((Token)tokens[0]).Value, ((Token)result[0]).Value);

                var tokenExpressions = tokens[1] as object[];
                var resultExpressions = result[1] as object[];

                Assert.NotNull(tokenExpressions);
                Assert.NotNull(resultExpressions);

                for (var i = 0; i < 2; i++)
                for (var j = 0; j < 3; j++)
                {
                    Assert.Equal(((Token)((object[])tokenExpressions[i])[j]).Type.Name,
                        ((Token)((object[])resultExpressions[i])[j]).Type.Name);
                    Assert.Equal(((Token)((object[])tokenExpressions[i])[j]).Value,
                        ((Token)((object[])resultExpressions[i])[j]).Value);
                }
            }
        }
    }
}