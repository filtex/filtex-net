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
    public class TextQueryParserTests
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

            var textQueryTokenizerMock = Substitute.For<ITextQueryTokenizer>();
            textQueryTokenizerMock
                .Tokenize(Arg.Any<string>())
                .Throws(TokenizeException.NewCouldNotBeTokenizedError());

            var textQueryParser = new TextQueryParser(metadata, textQueryTokenizerMock);

            Assert.Throws<TokenizeException>(() =>
            {
                // Act
                var expression = textQueryParser.Parse(query);

                // Assert
                Assert.Null(expression);
            });
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryTokenizerReturnedEmptyResult()
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

            var textQueryTokenizerMock = Substitute.For<ITextQueryTokenizer>();
            textQueryTokenizerMock
                .Tokenize(Arg.Any<string>())
                .Returns(new Token[] { });

            var textQueryParser = new TextQueryParser(metadata, textQueryTokenizerMock);

            Assert.Throws<ParseException>(() =>
            {
                // Act
                var expression = textQueryParser.Parse(query);

                // Assert
                Assert.Null(expression);
            });
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenQueryTokenLengthIsNotValid()
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
                    "Value Equal Test Test", new[]
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
                var textQueryTokenizerMock = Substitute.For<ITextQueryTokenizer>();
                textQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var textQueryParser = new TextQueryParser(metadata, textQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = textQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenLogicIsNotValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
                {
                    "Value Equal Test Xor Value Equal Test", new[]
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
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = "Xor"
                        },
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
                var textQueryTokenizerMock = Substitute.For<ITextQueryTokenizer>();
                textQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var textQueryParser = new TextQueryParser(metadata, textQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = textQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnError_WhenOperatorIsNotValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
                {
                    "Value Op Test", new[]
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
                var textQueryTokenizerMock = Substitute.For<ITextQueryTokenizer>();
                textQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var textQueryParser = new TextQueryParser(metadata, textQueryTokenizerMock);

                Assert.Throws<ParseException>(() =>
                {
                    // Act
                    var expression = textQueryParser.Parse(query);

                    // Assert
                    Assert.Null(expression);
                });
            }
        }

        [Fact]
        public void Parse_ShouldReturnLogicExpression_WhenQueryHasLogic()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
                {
                    "Value1 Equal Test1 And Value2 Equal Test2", new[]
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
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeAnd,
                            Value = "And"
                        },
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
                var textQueryTokenizerMock = Substitute.For<ITextQueryTokenizer>();
                textQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var textQueryParser = new TextQueryParser(metadata, textQueryTokenizerMock);

                // Act
                var expression = textQueryParser.Parse(query);

                // Assert

                Assert.NotNull(expression);
                Assert.IsType<LogicExpression>(expression);

                var logicExpression = expression as LogicExpression;

                Assert.NotNull(logicExpression);
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
        public void Parse_ShouldReturnOperatorExpression_WhenQueryDoesNotHaveLogic()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
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
                var textQueryTokenizerMock = Substitute.For<ITextQueryTokenizer>();
                textQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var textQueryParser = new TextQueryParser(metadata, textQueryTokenizerMock);

                // Act
                var expression = textQueryParser.Parse(query);

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