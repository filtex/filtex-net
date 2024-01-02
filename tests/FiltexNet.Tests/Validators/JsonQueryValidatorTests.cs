using System;
using System.Collections.Generic;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;
using FiltexNet.Tokenizers;
using FiltexNet.Validators;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace FiltexNet.Tests.Validators
{
    public class JsonQueryValidatorTests
    {
        [Fact]
        public void Validate_ShouldReturnError_WhenQueryTokenizerReturnedError()
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
                .Throws(TokenizeException.NewCouldNotBeTokenizedError());

            var jsonQueryValidator = new JsonQueryValidator(metadata, jsonQueryTokenizerMock);

            // Assert
            Assert.Throws<TokenizeException>(() =>
            {
                // Act
                jsonQueryValidator.Validate(query);
            });
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenQueryTokenLengthIsNotValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[]", new Token[] { }
                },
                {
                    "[\"Value\"]", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", \"Test\", \"Test\"]", new[]
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
                var jsonQueryTokenizerMock = Substitute.For<IJsonQueryTokenizer>();

                jsonQueryTokenizerMock
                    .Tokenize(Arg.Is(query))
                    .Returns(tokens);

                var jsonQueryValidator = new JsonQueryValidator(metadata, jsonQueryTokenizerMock);

                // Assert
                Assert.Throws<ValidateException>(() =>
                {
                    // Act
                    jsonQueryValidator.Validate(query);
                });
            }
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenQueryTokenLengthIsThreeAndThereIsNoneToken()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Value1\", \"Equal\", \"Test\"]", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
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
                            Value = "Test"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", \"Test\"]", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = "Equal"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeStringValue,
                            Value = "Test"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal1\", \"Test\"]", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = "Equal1"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeStringValue,
                            Value = "Test"
                        }
                    }
                },
                {
                    "[\"Value\", \"Equal\", \"Test1\"]", new[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = "Value"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeEqual,
                            Value = "Equal1"
                        },
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = "Test1"
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

                var jsonQueryValidator = new JsonQueryValidator(metadata, jsonQueryTokenizerMock);

                // Assert
                Assert.Throws<ValidateException>(() =>
                {
                    // Act
                    jsonQueryValidator.Validate(query);
                });
            }
        }

        [Fact]
        public void Validate_ShouldReturnNil_WhenQueryTokenLengthIsThreeAndValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Value\", \"Equal\", \"Test\"]", new[]
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

                var jsonQueryValidator = new JsonQueryValidator(metadata, jsonQueryTokenizerMock);

                // Assert
                // Act
                jsonQueryValidator.Validate(query);
            }
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenQueryTokenLengthIsTwoAndThereIsNoneToken()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"O\", [[\"Value\", \"Equal\", \"Test1\"], [\"Value\", \"Equal\", \"Test2\"]]]", new object[]
                    {
                        new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = "O"
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
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test1"
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
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test2"
                                }
                            }
                        }
                    }
                },
                {
                    "[\"Or\", [[\"Value1\", \"Equal\", \"Test1\"], [\"Value\", \"Equal\", \"Test2\"]]]", new object[]
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
                                    Type = TokenType.TokenTypeNone,
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
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test2"
                                }
                            }
                        }
                    }
                },
                {
                    "[\"Or\", [[\"Value\", \"Equals\", \"Test1\"], [\"Value\", \"Equal\", \"Test2\"]]]", new object[]
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
                                    Type = TokenType.TokenTypeNone,
                                    Value = "Equals"
                                },
                                new Token
                                {
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test1"
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
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test2"
                                }
                            }
                        }
                    }
                },
                {
                    "[\"Or\", [[\"Value\", \"Equal\", \"Test1\"], [\"Value\", \"Equal\", \"Test3\"]]]", new object[]
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
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test1"
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
                                    Type = TokenType.TokenTypeNone,
                                    Value = "Test3"
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

                var jsonQueryValidator = new JsonQueryValidator(metadata, jsonQueryTokenizerMock);


                // Assert
                Assert.Throws<ValidateException>(() =>
                {
                    // Act
                    jsonQueryValidator.Validate(query);
                });
            }
        }

        [Fact]
        public void Validate_ShouldReturnNil_WhenQueryTokenLengthIsTwoAndValid()
        {
            // Arrange
            var queryMap = new Dictionary<string, object[]>
            {
                {
                    "[\"Or\", [[\"Value\", \"Equal\", \"Test1\"], [\"Value\", \"Equal\", \"Test2\"]]]", new object[]
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
                                    Type = TokenType.TokenTypeStringValue,
                                    Value = "Test1"
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

                var jsonQueryValidator = new JsonQueryValidator(metadata, jsonQueryTokenizerMock);

                // Act
                // Assert
                jsonQueryValidator.Validate(query);
            }
        }
    }
}