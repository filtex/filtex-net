using System;
using System.Collections.Generic;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;
using FiltexNet.Parsers;
using FiltexNet.Tokenizers;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace FiltexNet.Tests.Parsers
{
    public class JsonQueryParserTests
    {
        [Fact]
        public void Parse_ShouldReturnError_WhenQueryTokenizerReturnedError()
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
            var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
            jsonQueryTokenizerMock
                .Tokenize(Arg.Any<string>())
                .Throws(ParseException.NewCouldNotBeParsedError());

            var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

            Assert.Throws<ParseException>(() =>
            {
                // Act
                var expression = jsonQueryParser.Parse(query);

                // Assert
                Assert.Null(expression);
            });
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryTokenLengthIsNotValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                { "[]", new object[] { } },
                {
                    "[\"Value\"]", new object[]
                    {
                        new Token
                        {
                            Type =
                                TokenType.TokenTypeField,
                            Value =
                                "Value"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", \"Test\", \"Test\"]", new object[]
                    {
                        new Token
                        {
                            Type =
                                TokenType.TokenTypeField,
                            Value =
                                "Value"
                        },
                        new Token
                        {
                            Type =
                                TokenType.TokenTypeEqual,
                            Value =
                                "Equal"
                        },
                        new Token
                        {
                            Type =
                                TokenType.TokenTypeStringValue,
                            Value =
                                "Test"
                        },
                        new Token
                        {
                            Type =
                                TokenType.TokenTypeStringValue,
                            Value =
                                "Test"
                        }
                    }
                }
            };

            var metadata = new Metadata
            {
                Fields =
                    new[]
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = jsonQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryLengthIsTwoAndLogicIsNotToken()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Test\", [[\"Value\", \"Equal\", \"Test\"], [\"Value\", \"Equal\", \"Test\"]]]", new object[]
                    {
                        "Test",
                        new object[]
                        {
                            new object[]
                            {
                                new Token
                                {
                                    Type =
                                        TokenType.TokenTypeField,
                                    Value =
                                        "Value"
                                },
                                new Token
                                {
                                    Type =
                                        TokenType.TokenTypeEqual,
                                    Value =
                                        "Equal"
                                },
                                new Token
                                {
                                    Type =
                                        TokenType.TokenTypeStringValue,
                                    Value =
                                        "Test"
                                }
                            },
                            new object[]
                            {
                                new Token
                                {
                                    Type =
                                        TokenType.TokenTypeField,
                                    Value =
                                        "Value"
                                },
                                new Token
                                {
                                    Type =
                                        TokenType.TokenTypeEqual,
                                    Value =
                                        "Equal"
                                },
                                new Token
                                {
                                    Type =
                                        TokenType.TokenTypeStringValue,
                                    Value =
                                        "Test"
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = jsonQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryLengthIsTwoAndLogicIsNotValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Test\", [[\"Value\", \"Equal\", \"Test\"], [\"Value\", \"Equal\", \"Test\"]]]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeLiteral,
                            Value = "Test"
                        },
                        new object[]
                        {
                            new object[]
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
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test"
                                }
                            },
                            new object[]
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
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test"
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = jsonQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryLengthIsTwoAndSecondItemIsNotArray()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Test\", \"Test\"]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeAnd,
                            Value = "And"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeLiteral,
                            Value = "Test"
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = jsonQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnLogicExpression_WhenQueryLengthIsTwo()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"And\", [[\"Value1\", \"Equal\", \"Test1\"], [\"Value2\", \"Equal\", \"Test2\"]]]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeAnd,
                            Value = "And"
                        },
                        new object[]
                        {
                            new object[]
                            {
                                new Token
                                {
                                    Type = TokenType.TokenTypeField,
                                    Value = "Value1"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeEqual,
                                    Value = "Equal"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test1"
                                }
                            },
                            new object[]
                            {
                                new Token
                                {
                                    Type = TokenType.TokenTypeField,
                                    Value = "Value2"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeNotEqual,
                                    Value = "Not Equal"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test2"
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
                        Name = "Value1",
                        Type = "string",
                        Label = "Value1",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "Value2",
                        Type = "string",
                        Label = "Value2",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                // Act
                var expression = jsonQueryParser.Parse(query);

                // Assert
                Assert.NotNull(expression);
                Assert.IsType<LogicExpression>(expression);

                var logicExpression = (LogicExpression)expression;

                Assert.Equal(Logic.LogicAnd, logicExpression.Logic);
                Assert.NotNull(logicExpression.Expressions);
                Assert.Equal(2, logicExpression.Expressions.Length);

                var firstExpression = logicExpression.Expressions[0] as OperatorExpression;

                Assert.NotNull(firstExpression);
                Assert.Equal("Value1", firstExpression.Field);
                Assert.Equal(Operator.OperatorEqual, firstExpression.Operator);
                Assert.Equal("Test1", firstExpression.Value);

                var secondExpression = logicExpression.Expressions[1] as OperatorExpression;

                Assert.NotNull(secondExpression);
                Assert.Equal("Value2", secondExpression.Field);
                Assert.Equal(Operator.OperatorNotEqual, secondExpression.Operator);
                Assert.Equal("Test2", secondExpression.Value);
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryLengthIsThreeAndFieldIsNotToken()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Value\", \"Equal\", \"Test\"]", new object[]
                    {
                        "Value",
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeStringValue,
                            Value = "Test"
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = jsonQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryLengthIsThreeAndOperatorIsNotToken()
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
                        "Equal",
                        new Token
                        {
                            Type = TokenType.TokenTypeStringValue,
                            Value = "Test"
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = jsonQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryLengthIsThreeAndValueIsNotToken()
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
                        "Test"
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = jsonQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryLengthIsThreeAndOperatorIsNotValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Value\", \"Op\", \"Test\"]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = "Op"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeStringValue,
                            Value = "Test"
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = jsonQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnOperatorExpression_WhenQueryLengthIsThree()
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
                            Type = TokenType.TokenTypeStringValue,
                            Value = "Test"
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

            foreach (var (query, tokens) in queryMap)
            {
                // Arrange
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();
                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryParser = new JsonQueryParser(metadata, jsonQueryTokenizerMock);

                // Act
                var expression = jsonQueryParser.Parse(query);

                // Assert
                Assert.NotNull(expression);
                Assert.IsType<OperatorExpression>(expression);

                var operatorExpression = expression as OperatorExpression;

                Assert.NotNull(operatorExpression);
                Assert.Equal("Value", operatorExpression.Field);
                Assert.Equal(Operator.OperatorEqual, operatorExpression.Operator);
                Assert.Equal("Test", operatorExpression.Value);
            }
        }
    }
}