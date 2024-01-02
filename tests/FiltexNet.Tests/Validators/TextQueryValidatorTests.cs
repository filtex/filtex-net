using System;
using System.Collections.Generic;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;
using FiltexNet.Tokenizers;
using FiltexNet.Validators;
using NSubstitute;
using Xunit;

namespace FiltexNet.Tests.Validators
{
    public class TextQueryValidatorTests
    {
        [Fact]
        public void Validate_ShouldReturnNil_WhenThereIsNoToken()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
                {
                    "", new Token[] { }
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

                var textQueryValidator = new TextQueryValidator(metadata, textQueryTokenizerMock);

                // Act
                // Assert
                textQueryValidator.Validate(query);
            }
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenThereIsNoneToken()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
                {
                    "Value", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = "Value"
                        }
                    }
                },
                {
                    "Name Equals", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = "Equals"
                        }
                    }
                },
                {
                    "Name Equal 123", new[]
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
                            Type = TokenType.TokenTypeNone,
                            Value = "123"
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

                var textQueryValidator = new TextQueryValidator(metadata, textQueryTokenizerMock);

                // Assert
                Assert.Throws<ValidateException>(() =>
                {
                    // Act
                    textQueryValidator.Validate(query);
                });
            }
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenBracketCountIsMismatched()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
                {
                    "(Name",
                    new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeOpenBracket,
                            Value = "("
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        }
                    }
                },
                {
                    "(Name))", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeOpenBracket,
                            Value = "("
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeCloseBracket,
                            Value = ")"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeCloseBracket,
                            Value = ")"
                        }
                    }
                },
                {
                    "Name))", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeCloseBracket,
                            Value = ")"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeCloseBracket,
                            Value = ")"
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

                var textQueryValidator = new TextQueryValidator(metadata, textQueryTokenizerMock);

                // Assert
                Assert.Throws<ValidateException>(() =>
                {
                    // Act
                    textQueryValidator.Validate(query);
                });
            }
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenLastTokenIsNotValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
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
                    "Name Equal", new[]
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
                        }
                    }
                },
                {
                    "Name Equal Filtex And", new[]
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
                            Value = "Filtex"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeAnd,
                            Value = "And"
                        }
                    }
                },
                {
                    "Name Equal Filtex And (", new[]
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
                            Value = "Filtex"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeAnd,
                            Value = "And"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeOpenBracket,
                            Value = "("
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

                var textQueryValidator = new TextQueryValidator(metadata, textQueryTokenizerMock);

                // Assert
                Assert.Throws<ValidateException>(() =>
                {
                    // Act
                    textQueryValidator.Validate(query);
                });
            }
        }

        [Fact]
        public void Validate_ShouldReturnNil_WhenLastTokenIsValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, Token[]>
            {
                {
                    "Name Equal Filtex", new[]
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
                            Value = "Filtex"
                        }
                    }
                },
                {
                    "(Name Equal Filtex)", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeOpenBracket,
                            Value = "("
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
                            Type = TokenType.TokenTypeValue,
                            Value = "Filtex"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeCloseBracket,
                            Value = ")"
                        }
                    }
                },
                {
                    "Name Blank", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeBlank,
                            Value = "Blank"
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

                var textQueryValidator = new TextQueryValidator(metadata, textQueryTokenizerMock);

                // Act
                // Assert
                textQueryValidator.Validate(query);
            }
        }
    }
}