using System;
using System.Collections.Generic;
using FiltexNet.Constants;
using FiltexNet.Models;
using FiltexNet.Tokenizers;
using Xunit;

namespace FiltexNet.Tests.Tokenizers
{
    public class BaseQueryTokenizerTests
    {
        [Fact]
        public void CreateToken_ShouldReturnNil_WhenLastAndNewTokenIsSpace()
        {
            // Arrange
            var tokens = new[]
            {
                new Token
                {
                    Type = TokenType.TokenTypeSpace,
                    Value = " "
                }
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            // Act
            var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeSpace, " ");

            // Assert
            Assert.Null(token);
        }

        [Fact]
        public void CreateToken_ShouldReturnSpaceToken_WhenNewTokenIsSpaceAndLastTokenIsNot()
        {
            // Arrange
            var tokens = new[]
            {
                new Token
                {
                    Type = TokenType.TokenTypeField,
                    Value = "Value"
                }
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            // Act
            var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeSpace, " ");

            // Assert
            Assert.NotNull(token);
            Assert.Equal(TokenType.TokenTypeSpace, token.Type);
            Assert.Equal(" ", token.Value);
        }

        [Fact]
        public void CreateToken_ShouldReturnFieldToken_WhenThereIsNoTokensAndTokenIsValidLiteral()
        {
            // Arrange
            var tokens = Array.Empty<Token>();

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            // Act
            var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Value");

            // Assert
            Assert.NotNull(token);
            Assert.Equal(TokenType.TokenTypeField, token.Type);
            Assert.Equal("Value", token.Value);
        }

        [Fact]
        public void CreateToken_ShouldReturnFieldToken_WhenThereIsNoTokensAndTokenIsValidField()
        {
            // Arrange
            var tokens = Array.Empty<Token>();

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            // Act
            var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Value");

            // Assert
            Assert.NotNull(token);
            Assert.Equal(TokenType.TokenTypeField, token.Type);
            Assert.Equal("Value", token.Value);
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenThereIsNoTokensAndTokenIsInvalidField()
        {
            // Arrange
            var tokens = Array.Empty<Token>();

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            // Act
            var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Value1");

            // Assert
            Assert.NotNull(token);
            Assert.Equal(TokenType.TokenTypeNone, token.Type);
            Assert.Equal("Value1", token.Value);
        }

        [Fact]
        public void CreateToken_ShouldReturnFieldToken_WhenThereIsNoTokensAndTokenIsInOpenGroupTokens()
        {
            // Arrange
            var tokens = Array.Empty<Token>();

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            // Act
            var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeOpenBracket, "(");

            // Assert
            Assert.NotNull(token);
            Assert.Equal(TokenType.TokenTypeOpenBracket, token.Type);
            Assert.Equal("(", token.Value);
        }

        [Fact]
        public void CreateToken_ShouldReturnValueToken_WhenTokenIsFieldAndLastTokenIsNotInPreFieldAndComparerAndSeparatorTokensAndValidForValue()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Value");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeValue, token.Type);
                Assert.Equal("Value", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsFieldAndLastTokenIsNotInPreFieldAndComparerAndSeparatorTokensAndNotValidForValue()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = ""
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeCloseBracket,
                        Value = ")"
                    }
                },
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
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
                        Value = "Test"
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
                        Value = 100
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
                        Type = TokenType.TokenTypeBooleanValue,
                        Value = false
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
                        Type = TokenType.TokenTypeDateValue,
                        Value = DateTime.Now.Date
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
                        Type = TokenType.TokenTypeTimeValue,
                        Value = 60
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
                        Type = TokenType.TokenTypeDateTimeValue,
                        Value = DateTime.Now
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Value");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Value", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsFieldAndLastTokenIsInPreFieldTokensAndNotValidField()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "And"
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "Or"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Value1");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Value1", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnFieldToken_WhenTokenIsFieldAndLastTokenIsInPreFieldTokensAndValidField()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "And"
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "Or"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Value");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeField, token.Type);
                Assert.Equal("Value", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsFieldAndLastTokenIsInComparerOrSeparatorTokensAndInvalidValue()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeNumber.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Test");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Test", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenLastTokenIsInComparerOrSeparatorTokensAndValidValueAndOperatorIsNotComparer()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Value");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Value", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnValueToken_WhenTokenIsFieldAndLastTokenIsInComparerOrSeparatorTokensAndValidValueAndOperatorIsComparer()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeField, "Test");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeValue, token.Type);
                Assert.Equal("Test", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnValueToken_WhenTokenIsLiteralAndLastTokenIsNotInComparerAndSeparatorAndPreFieldTokensAndValidForValue()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Value");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeValue, token.Type);
                Assert.Equal("Value", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsLiteralAndLastTokenIsNotInComparerAndSeparatorAndPreFieldTokensAndNotValidForValue()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = ""
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeCloseBracket,
                        Value = ")"
                    }
                },
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
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
                        Value = "Test"
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
                        Value = 100
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
                        Type = TokenType.TokenTypeBooleanValue,
                        Value = false
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
                        Type = TokenType.TokenTypeDateValue,
                        Value = DateTime.Now
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
                        Type = TokenType.TokenTypeTimeValue,
                        Value = 60
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
                        Type = TokenType.TokenTypeDateTimeValue,
                        Value = DateTime.Now
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Value");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Value", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsLiteralAndLastTokenIsInComparerOrSeparatorTokensAndInvalidValue()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeDate.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "100");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("100", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsLiteralAndLastTokenIsInComparerOrSeparatorTokensAndValidValueAndLastOperatorIsNotComparer()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Test");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Test", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnValueToken_WhenTokenIsLiteralAndLastTokenIsInComparerOrSeparatorTokensAndValidValue()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Test");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeValue, token.Type);
                Assert.Equal("Test", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsLiteralAndLastTokenIsNotInComparerAndSeparatorAndPreFieldTokensAndInvalidForLookupValue()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeBoolean.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = new Lookup[]
                        {
                            new("Enabled", true),
                            new("Disabled", false)
                        }
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Value");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Value", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnValueToken_WhenTokenIsLiteralAndLastTokenIsNotInComparerAndSeparatorAndPreFieldTokensAndValidForLookupValue()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeBoolean.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = new Lookup[]
                        {
                            new("Enabled", true),
                            new("Disabled", false)
                        }
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Enabled");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeValue, token.Type);
                Assert.Equal(true, token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsLiteralAndLastTokenIsInPreFieldTokensAndInvalidField()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "And"
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "Or"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Value1");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Value1", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnFieldToken_WhenTokenIsLiteralAndLastTokenIsInPreFieldTokensAndValidField()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "And"
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "Or"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeLiteral, "Value");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeField, token.Type);
                Assert.Equal("Value", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsValueAndLastTokenIsNotInComparerAndSeparatorTokens()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = ""
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeCloseBracket,
                        Value = ")"
                    }
                },
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Value = "Test"
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
                        Value = 100
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
                        Type = TokenType.TokenTypeBooleanValue,
                        Value = false
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
                        Type = TokenType.TokenTypeDateValue,
                        Value = DateTime.Now
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
                        Type = TokenType.TokenTypeTimeValue,
                        Value = 60
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
                        Type = TokenType.TokenTypeDateTimeValue,
                        Value = DateTime.Now
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
                        Type = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    }
                }
            };

            var valueTokenTypes = new[]
            {
                TokenType.TokenTypeValue,
                TokenType.TokenTypeStringValue,
                TokenType.TokenTypeNumberValue,
                TokenType.TokenTypeBooleanValue,
                TokenType.TokenTypeDateValue,
                TokenType.TokenTypeTimeValue,
                TokenType.TokenTypeDateTimeValue
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var valueTokenType in valueTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, valueTokenType, "Test");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Test", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsValueAndLastTokenIsInComparerOrSeparatorTokensAndInvalidValue()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
                    }
                }
            };

            var valueTokenTypes = new[]
            {
                TokenType.TokenTypeValue,
                TokenType.TokenTypeStringValue,
                TokenType.TokenTypeNumberValue,
                TokenType.TokenTypeBooleanValue,
                TokenType.TokenTypeDateValue,
                TokenType.TokenTypeTimeValue,
                TokenType.TokenTypeDateTimeValue
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeTime.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var valueTokenType in valueTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, valueTokenType, "Test");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("Test", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsValueAndLastTokenIsInComparerOrSeparatorTokensAndValidValueAndOperatorIsNotComparer()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
                    }
                }
            };

            var valueTokenTypes = new[]
            {
                TokenType.TokenTypeValue,
                TokenType.TokenTypeStringValue,
                TokenType.TokenTypeNumberValue,
                TokenType.TokenTypeBooleanValue,
                TokenType.TokenTypeDateValue,
                TokenType.TokenTypeTimeValue,
                TokenType.TokenTypeDateTimeValue
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var valueTokenType in valueTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, valueTokenType, "100");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("100", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnValueToken_WhenTokenIsValueAndLastTokenIsInComparerOrSeparatorTokensAndValidValueAndOperatorIsComparer()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
                    }
                }
            };

            var valueTokenTypes = new[]
            {
                TokenType.TokenTypeValue,
                TokenType.TokenTypeStringValue,
                TokenType.TokenTypeNumberValue,
                TokenType.TokenTypeBooleanValue,
                TokenType.TokenTypeDateValue,
                TokenType.TokenTypeTimeValue,
                TokenType.TokenTypeDateTimeValue
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var valueTokenType in valueTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, valueTokenType, "Test");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(valueTokenType, token.Type);
                Assert.Equal("Test", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsOperatorAndLastTokenIsNotFieldToken()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
                    }
                }
            };

            var operatorTokenTypes = new[]
            {
                TokenType.TokenTypeEqual,
                TokenType.TokenTypeNotEqual,
                TokenType.TokenTypeGreaterThan,
                TokenType.TokenTypeGreaterThanOrEqual,
                TokenType.TokenTypeLessThan,
                TokenType.TokenTypeLessThanOrEqual,
                TokenType.TokenTypeBlank,
                TokenType.TokenTypeNotBlank,
                TokenType.TokenTypeContain,
                TokenType.TokenTypeNotContain,
                TokenType.TokenTypeStartWith,
                TokenType.TokenTypeNotStartWith,
                TokenType.TokenTypeEndWith,
                TokenType.TokenTypeNotEndWith,
                TokenType.TokenTypeIn,
                TokenType.TokenTypeNotIn
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var operatorTokenType in operatorTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, operatorTokenType, operatorTokenType.ToString());

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal(operatorTokenType.ToString(), token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsOperatorAndLastTokenIsFieldTokenAndInvalidOperator()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
                    }
                }
            };

            var invalidOperators = new[]
            {
                "",
                "op",
                TokenType.TokenTypeGreaterThan.Name,
                TokenType.TokenTypeGreaterThanOrEqual.Name,
                TokenType.TokenTypeLessThan.Name,
                TokenType.TokenTypeLessThanOrEqual.Name,
                TokenType.TokenTypeBlank.Name,
                TokenType.TokenTypeNotBlank.Name,
                TokenType.TokenTypeContain.Name,
                TokenType.TokenTypeNotContain.Name,
                TokenType.TokenTypeStartWith.Name,
                TokenType.TokenTypeNotStartWith.Name,
                TokenType.TokenTypeEndWith.Name,
                TokenType.TokenTypeNotEndWith.Name,
                TokenType.TokenTypeIn.Name,
                TokenType.TokenTypeNotIn.Name
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var invalidOperator in invalidOperators)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, new TokenType(invalidOperator),
                    invalidOperator);

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal(invalidOperator, token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnOperatorToken_WhenTokenIsOperatorAndLastTokenIsFieldTokenAndValidOperator()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
                    }
                }
            };

            var operatorTokenTypes = new[]
            {
                TokenType.TokenTypeEqual,
                TokenType.TokenTypeNotEqual,
                TokenType.TokenTypeGreaterThan,
                TokenType.TokenTypeGreaterThanOrEqual,
                TokenType.TokenTypeLessThan,
                TokenType.TokenTypeLessThanOrEqual,
                TokenType.TokenTypeBlank,
                TokenType.TokenTypeNotBlank,
                TokenType.TokenTypeContain,
                TokenType.TokenTypeNotContain,
                TokenType.TokenTypeStartWith,
                TokenType.TokenTypeNotStartWith,
                TokenType.TokenTypeEndWith,
                TokenType.TokenTypeNotEndWith,
                TokenType.TokenTypeIn,
                TokenType.TokenTypeNotIn
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var operatorTokenType in operatorTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, operatorTokenType, operatorTokenType.ToString());

                // Assert
                Assert.NotNull(token);
                Assert.Equal(operatorTokenType, token.Type);
                Assert.Equal(operatorTokenType.ToString(), token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsLogicAndLastTokenIsNotInValueAndCloseGroupAndNotComparerTokens()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
                    }
                },
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "And"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeOr,
                        Value = "Or"
                    }
                }
            };

            var logicTokenTypes = new[]
            {
                TokenType.TokenTypeAnd,
                TokenType.TokenTypeOr
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var logicTokenType in logicTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, logicTokenType, logicTokenType.ToString());

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal(logicTokenType.ToString(), token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnLogicToken_WhenTokenIsLogicAndLastTokenIsInValueOrCloseGroupOrNotComparerTokens()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
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
                        Type = TokenType.TokenTypeValue,
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
                        Type = TokenType.TokenTypeNumberValue,
                        Value = 100
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
                        Type = TokenType.TokenTypeBooleanValue,
                        Value = true
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
                        Type = TokenType.TokenTypeDateValue,
                        Value = DateTime.Now
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
                        Type = TokenType.TokenTypeTimeValue,
                        Value = 60
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
                        Type = TokenType.TokenTypeDateTimeValue,
                        Value = DateTime.Now
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeEqual,
                        Value = "Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeCloseBracket,
                        Value = ")"
                    }
                }
            };

            var logicTokenTypes = new[]
            {
                TokenType.TokenTypeAnd,
                TokenType.TokenTypeOr
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var logicTokenType in logicTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, logicTokenType, logicTokenType.ToString());

                // Assert
                Assert.NotNull(token);
                Assert.Equal(logicTokenType, token.Type);
                Assert.Equal(logicTokenType.ToString(), token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsInOpenGroupTokenTypesAndLastTokenIsNotInLogicAndOpenGroupTokens()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeCloseBracket,
                        Value = ")"
                    }
                },
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
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
                        Type = TokenType.TokenTypeValue,
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeOpenBracket, "(");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal("(", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnOpenGroupToken_WhenTokenIsInOpenGroupTokensAndLastTokenIsInLogicOrOpenGroupTokens()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "And"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeOr,
                        Value = "Or"
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
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeOpenBracket, "(");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeOpenBracket, token.Type);
                Assert.Equal("(", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsInCloseGroupTokenTypesAndLastTokenIsNotInValueAndCloseGroupAndNotComparerTokens()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
                    }
                },
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
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
                        Type = TokenType.TokenTypeValue,
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeSlash,
                        Value = "/"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "And"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test1"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeOr,
                        Value = "Or"
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
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeCloseBracket, ")");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal(")", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsInOpenGroupTokensAndLastTokenIsInValueOrCloseGroupAndNotComparerTokensAndOpenGroupCountIsInvalid()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeCloseBracket,
                        Value = ")"
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Value = "Test"
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
                        Value = 100
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
                        Type = TokenType.TokenTypeBooleanValue,
                        Value = false
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
                        Type = TokenType.TokenTypeDateValue,
                        Value = DateTime.Now
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
                        Type = TokenType.TokenTypeTimeValue,
                        Value = 60
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
                        Type = TokenType.TokenTypeDateTimeValue,
                        Value = DateTime.Now
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
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeCloseBracket, ")");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal(")", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnCloseGroupToken_WhenTokenIsInOpenGroupTokensAndLastTokenIsInValueOrCloseGroupAndNotComparerTokens()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeOpenBracket,
                        Value = "("
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeCloseBracket,
                        Value = ")"
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
                    }
                },
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
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeEqual,
                        Value = "Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeNumberValue,
                        Value = 100
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeEqual,
                        Value = "Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeBooleanValue,
                        Value = false
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeEqual,
                        Value = "Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeDateValue,
                        Value = DateTime.Now
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeEqual,
                        Value = "Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeTimeValue,
                        Value = 60
                    }
                },
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
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeEqual,
                        Value = "Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeDateTimeValue,
                        Value = DateTime.Now
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
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, TokenType.TokenTypeCloseBracket, ")");

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeCloseBracket, token.Type);
                Assert.Equal(")", token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsSeparatorAndOperatorIsNotInComparerAndMultiAllowedTokens()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
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
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeNotEqual,
                        Value = "Not Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeGreaterThan,
                        Value = "Greater Than"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "Greater Than Or Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeLessThan,
                        Value = "Less Than"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeLessThanOrEqual,
                        Value = "Less Than Or Equal"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeBlank,
                        Value = "Blank"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeNotBlank,
                        Value = "Not Blank"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeContain,
                        Value = "Contain"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeNotContain,
                        Value = "Not Contain"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeStartWith,
                        Value = "Start With"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeEndWith,
                        Value = "End With"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeNotEndWith,
                        Value = "Not End With"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeValue,
                        Value = "Test"
                    }
                }
            };

            var separatorTokenTypes = new[]
            {
                TokenType.TokenTypeComma,
                TokenType.TokenTypeSlash
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var separatorTokenType in separatorTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, separatorTokenType, separatorTokenType.ToString());

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal(separatorTokenType.ToString(), token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNoneToken_WhenTokenIsSeparatorAndOperatorIsValidAndLastTokenIsNotInValueTokens()
        {
            // Arrange
            var tokensList = new[]
            {
                new[]
                {
                    new Token
                    {
                        Type = TokenType.TokenTypeField,
                        Value = "Value"
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = 100
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeNumberValue,
                        Value = 100
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeComma,
                        Value = ","
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeNumberValue,
                        Value = 100
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeAnd,
                        Value = "And"
                    }
                }
            };

            var separatorTokenTypes = new[]
            {
                TokenType.TokenTypeComma,
                TokenType.TokenTypeSlash
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var separatorTokenType in separatorTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, separatorTokenType, separatorTokenType.ToString());

                // Assert
                Assert.NotNull(token);
                Assert.Equal(TokenType.TokenTypeNone, token.Type);
                Assert.Equal(separatorTokenType.ToString(), token.Value);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnSeparatorToken_WhenTokenIsSeparatorAndOperatorIsValidAndLastTokenIsInValueTokens()
        {
            // Arrange
            var tokensList = new[]
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
                        Type = TokenType.TokenTypeIn,
                        Value = "In"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeStringValue,
                        Value = "Test"
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
                        Type = TokenType.TokenTypeNotIn,
                        Value = "Not In"
                    },
                    new Token
                    {
                        Type = TokenType.TokenTypeStringValue,
                        Value = "Test"
                    }
                }
            };

            var separatorTokenTypes = new[]
            {
                TokenType.TokenTypeComma,
                TokenType.TokenTypeSlash
            };

            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name,
                            Operator.OperatorGreaterThan.Name,
                            Operator.OperatorGreaterThanOrEqual.Name,
                            Operator.OperatorLessThan.Name,
                            Operator.OperatorLessThanOrEqual.Name,
                            Operator.OperatorBlank.Name,
                            Operator.OperatorNotBlank.Name,
                            Operator.OperatorContain.Name,
                            Operator.OperatorNotContain.Name,
                            Operator.OperatorStartWith.Name,
                            Operator.OperatorNotStartWith.Name,
                            Operator.OperatorEndWith.Name,
                            Operator.OperatorNotEndWith.Name,
                            Operator.OperatorIn.Name,
                            Operator.OperatorNotIn.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var tokens in tokensList)
            foreach (var separatorTokenType in separatorTokenTypes)
            {
                // Act
                var token = baseQueryTokenizer.CreateToken(tokens, separatorTokenType, separatorTokenType.ToString());

                // Assert
                Assert.NotNull(token);
                Assert.Equal(separatorTokenType, token.Type);
                Assert.Equal(separatorTokenType.ToString(), token.Value);
            }
        }

        [Fact]
        public void FindMatch_ShouldReturnNil_WhenNotMatched()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            // Act
            var match = baseQueryTokenizer.FindMatch("%");

            // Assert
            Assert.Null(match);
        }

        [Fact]
        public void FindMatch_ShouldReturnTokenMatch_WhenMatchedExactly()
        {
            // Arrange
            var matchMap = new Dictionary<string, TokenMatch>
            {
                {
                    "(", new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeOpenBracket,
                        Value = "("
                    }
                },
                {
                    ")",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeCloseBracket,
                        Value = ")"
                    }
                },
                {
                    ",",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeComma,
                        Value = ","
                    }
                },
                {
                    "/",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeSlash,
                        Value = "/"
                    }
                },
                {
                    "and",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeAnd,
                        Value = "and"
                    }
                },
                {
                    "&&",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeAnd,
                        Value = "&&"
                    }
                },
                {
                    "or",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeOr,
                        Value = "or"
                    }
                },
                {
                    "||",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeOr,
                        Value = "||"
                    }
                },
                {
                    "=",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeEqual,
                        Value = "="
                    }
                },
                {
                    "equal",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeEqual,
                        Value = "equal"
                    }
                },
                {
                    "!=",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotEqual,
                        Value = "!="
                    }
                },
                {
                    "not equal",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotEqual,
                        Value = "not equal"
                    }
                },
                {
                    "greater than or equal",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "greater than or equal"
                    }
                },
                {
                    ">=",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = ">="
                    }
                },
                {
                    "greater than",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeGreaterThan,
                        Value = "greater than"
                    }
                },
                {
                    ">",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeGreaterThan,
                        Value = ">"
                    }
                },
                {
                    "less than or equal",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeLessThanOrEqual,
                        Value = "less than or equal"
                    }
                },
                {
                    "<=",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeLessThanOrEqual,
                        Value = "<="
                    }
                },
                {
                    "less than",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeLessThan,
                        Value = "less than"
                    }
                },
                {
                    "<",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeLessThan,
                        Value = "<"
                    }
                },
                {
                    "[]",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeBlank,
                        Value = "[]"
                    }
                },
                {
                    "blank",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeBlank,
                        Value = "blank"
                    }
                },
                {
                    "![]",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotBlank,
                        Value = "![]"
                    }
                },
                {
                    "not blank",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotBlank,
                        Value = "not blank"
                    }
                },
                {
                    "~",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeContain,
                        Value = "~"
                    }
                },
                {
                    "contain",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeContain,
                        Value = "contain"
                    }
                },
                {
                    "!~",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotContain,
                        Value = "!~"
                    }
                },
                {
                    "not contain",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotContain,
                        Value = "not contain"
                    }
                },
                {
                    "~*",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeStartWith,
                        Value = "~*"
                    }
                },
                {
                    "start with",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeStartWith,
                        Value = "start with"
                    }
                },
                {
                    "!~*",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotStartWith,
                        Value = "!~*"
                    }
                },
                {
                    "not start with",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotStartWith,
                        Value = "not start with"
                    }
                },
                {
                    "*~",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeEndWith,
                        Value = "*~"
                    }
                },
                {
                    "end with",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeEndWith,
                        Value = "end with"
                    }
                },
                {
                    "!*~",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotEndWith,
                        Value = "!*~"
                    }
                },
                {
                    "not end with",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotEndWith,
                        Value = "not end with"
                    }
                },
                {
                    "in",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeIn,
                        Value = "in"
                    }
                },
                {
                    "not in",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotIn,
                        Value = "not in"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var (query, expected) in matchMap)
            {
                // Act
                var match = baseQueryTokenizer.FindMatch(query);

                // Assert
                Assert.Equal(expected.TokenType, match.TokenType);
                Assert.Equal(expected.Value, match.Value);
                Assert.Equal(expected.RemainingText, match.RemainingText);
            }
        }

        [Fact]
        public void FindMatch_ShouldReturnTokenMatch_WhenMatchedWithCaseInsensitively()
        {
            // Arrange
            var matchMap = new Dictionary<string, TokenMatch>
            {
                {
                    "AnD",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeAnd,
                        Value = "AnD"
                    }
                },
                {
                    "OR",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeOr,
                        Value = "OR"
                    }
                },
                {
                    "EquAL",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeEqual,
                        Value = "EquAL"
                    }
                },
                {
                    "not EQUAL",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotEqual,
                        Value = "not EQUAL"
                    }
                },
                {
                    "greater than or EQUAL",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeGreaterThanOrEqual,
                        Value = "greater than or EQUAL"
                    }
                },
                {
                    "GREATER than",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeGreaterThan,
                        Value = "GREATER than"
                    }
                },
                {
                    "less THAN OR equal",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeLessThanOrEqual,
                        Value = "less THAN OR equal"
                    }
                },
                {
                    "less Than",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeLessThan,
                        Value = "less Than"
                    }
                },
                {
                    "BlAnk",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeBlank,
                        Value = "BlAnk"
                    }
                },
                {
                    "not BlAnk",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotBlank,
                        Value = "not BlAnk"
                    }
                },
                {
                    "Contain",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeContain,
                        Value = "Contain"
                    }
                },
                {
                    "not Contain",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotContain,
                        Value = "not Contain"
                    }
                },
                {
                    "Start with",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeStartWith,
                        Value = "Start with"
                    }
                },
                {
                    "Not Start With",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotStartWith,
                        Value = "Not Start With"
                    }
                },
                {
                    "end WiTh",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeEndWith,
                        Value = "end WiTh"
                    }
                },
                {
                    "not END with",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotEndWith,
                        Value = "not END with"
                    }
                },
                {
                    "IN",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeIn,
                        Value = "IN"
                    }
                },
                {
                    "nOt iN",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNotIn,
                        Value = "nOt iN"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var (query, expected) in matchMap)
            {
                // Act
                var match = baseQueryTokenizer.FindMatch(query);

                // Assert
                Assert.Equal(expected.TokenType, match.TokenType);
                Assert.Equal(expected.Value, match.Value);
                Assert.Equal(expected.RemainingText, match.RemainingText);
            }
        }

        [Fact]
        public void FindMatch_ShouldReturnTokenMatch_WhenMatchedAndThereIsRemainingText()
        {
            // Arrange
            var matchMap = new Dictionary<string, TokenMatch>
            {
                {
                    "Equal Test",
                    new TokenMatch
                    {
                        RemainingText = " Test",
                        TokenType = TokenType.TokenTypeEqual,
                        Value = "Equal"
                    }
                },
                {
                    "Equal Test AND",
                    new TokenMatch
                    {
                        RemainingText = " Test AND",
                        TokenType = TokenType.TokenTypeEqual,
                        Value = "Equal"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var (query, expected) in matchMap)
            {
                // Act
                var match = baseQueryTokenizer.FindMatch(query);

                // Assert
                Assert.Equal(expected.TokenType, match.TokenType);
                Assert.Equal(expected.Value, match.Value);
                Assert.Equal(expected.RemainingText, match.RemainingText);
            }
        }

        [Fact]
        public void FindMatch_ShouldReturnTokenMatch_WhenMatchedDefinedFields()
        {
            // Arrange
            var matchMap = new Dictionary<string, TokenMatch>
            {
                {
                    "Value",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeField,
                        Value = "Value"
                    }
                },
                {
                    "VaLue",
                    new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeField,
                        Value = "VaLue"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var (query, expected) in matchMap)
            {
                // Act
                var match = baseQueryTokenizer.FindMatch(query);

                // Assert
                Assert.Equal(expected.TokenType, match.TokenType);
                Assert.Equal(expected.Value, match.Value);
                Assert.Equal(expected.RemainingText, match.RemainingText);
            }
        }

        [Fact]
        public void FindMatch_ShouldReturnTokenMatch_WhenMatchedValue()
        {
            // Arrange
            var matchMap = new Dictionary<string, TokenMatch>
            {
                {
                    "Test", new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeLiteral,
                        Value = "Test"
                    }
                },
                {
                    "'Test'", new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeStringValue,
                        Value = "'Test'"
                    }
                },
                {
                    "123", new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeNumberValue,
                        Value = "123"
                    }
                },
                {
                    "True", new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeBooleanValue,
                        Value = "True"
                    }
                },
                {
                    "2020-01-01", new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeDateValue,
                        Value = "2020-01-01"
                    }
                },
                {
                    "14:12:10", new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeTimeValue,
                        Value = "14:12:10"
                    }
                },
                {
                    "2020-01-01 00:00:00", new TokenMatch
                    {
                        RemainingText = "",
                        TokenType = TokenType.TokenTypeDateTimeValue,
                        Value = "2020-01-01 00:00:00"
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
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            foreach (var (query, expected) in matchMap)
            {
                // Act
                var match = baseQueryTokenizer.FindMatch(query);

                // Assert
                Assert.Equal(expected.TokenType, match.TokenType);
                Assert.Equal(expected.Value, match.Value);
                Assert.Equal(expected.RemainingText, match.RemainingText);
            }
        }

        [Fact]
        public void ValidateField_ShouldReturnFalse_WhenThereIsNoDefinedField()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new Field[] { }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "",
                "Value",
                "Value1",
                "Value2"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateField(sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateField_ShouldReturnFalse_WhenFieldIsNotValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "",
                "Value1",
                "Value2"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateField(sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateField_ShouldReturnTrue_WhenFieldIsValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "Value",
                "value",
                "VaLuE"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateField(sample);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void ValidateOperator_ShouldReturnFalse_WhenThereIsNoDefinedField()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new Field[] { }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "equal",
                "Equal",
                "not-equal",
                "Not-Equal"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateOperator("Value", sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateOperator_ShouldReturnFalse_WhenFieldIsNotValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "equal",
                "Equal",
                "not-equal",
                "Not-Equal"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateOperator("Value1", sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateOperator_ShouldReturnFalse_WhenFieldIsValidAndThereIsNoDefinedOperator()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = "string",
                        Label = "Value",
                        Operators = new string[] { },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "equal",
                "Equal",
                "not-equal",
                "Not-Equal"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateOperator("Value", sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateOperator_ShouldReturnFalse_WhenFieldIsValidAndOperatorIsNotValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "In",
                "Not-In"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateOperator("Value", sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateOperator_ShouldReturnTrue_WhenFieldIsValidAndOperatorIsValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "equal",
                "Equal",
                "not-equal",
                "Not-Equal"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateOperator("Value", sample);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void ValidateValue_ShouldReturnFalse_WhenThereIsNoDefinedFields()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new Field[] { }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "Test1",
                "Test2"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateValue("Value", sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateValue_ShouldReturnFalse_WhenFieldIsNotValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "Test1",
                "Test2"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateValue("Value1", sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateValue_ShouldReturnFalse_WhenFieldIsValidAndHasValuesAndNotMatched()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = new[]
                        {
                            new Lookup("Enabled", true),
                            new Lookup("Disabled", false)
                        }
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "Active",
                "Passive"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateValue("Value", sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateValue_ShouldReturnTrue_WhenFieldIsValidAndHasValuesAndMatched()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "Value",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = new[]
                        {
                            new Lookup("Enabled", true),
                            new Lookup("Disabled", false)
                        }
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "Enabled",
                "Disabled"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateValue("Value", sample);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void ValidateValue_ShouldReturnFalse_WhenFieldIsValidAndDoesNotHaveValuesAndTypeIsNotValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeTime.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "True",
                "2020-01-01 11:12:13"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateValue("Value", sample);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ValidateValue_ShouldReturnTrue_WhenFieldIsValidAndDoesNotHaveValuesAndTypeIsValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "Test1",
                "Test2"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.ValidateValue("Value", sample);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void CastValue_ShouldReturnSame_WhenThereIsNoDefinedField()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new Field[] { }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "100",
                "200"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.CastValue("Value", sample);

                // Assert
                Assert.Equal(sample, result);
            }
        }

        [Fact]
        public void CastValue_ShouldReturnSame_WhenFieldIsNotValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "Value",
                        Type = FieldType.FieldTypeString.Name,
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

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new[]
            {
                "100",
                "200"
            };

            foreach (var sample in samples)
            {
                // Act
                var result = baseQueryTokenizer.CastValue("Value1", sample);

                // Assert
                Assert.Equal(sample, result);
            }
        }

        [Fact]
        public void CastValue_ShouldReturnCasted_WhenFieldIsValid()
        {
            // Arrange
            var metadata = new Metadata
            {
                Fields = new[]
                {
                    new Field
                    {
                        Name = "StringValue",
                        Type = FieldType.FieldTypeString.Name,
                        Label = "StringValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "StringArrayValue",
                        Type = FieldType.FieldTypeStringArray.Name,
                        Label = "StringArrayValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "NumberValue",
                        Type = FieldType.FieldTypeNumber.Name,
                        Label = "NumberValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "NumberArrayValue",
                        Type = FieldType.FieldTypeNumberArray.Name,
                        Label = "NumberArrayValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "BooleanValue",
                        Type = FieldType.FieldTypeBoolean.Name,
                        Label = "BooleanValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "BooleanArrayValue",
                        Type = FieldType.FieldTypeBooleanArray.Name,
                        Label = "BooleanArrayValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "DateValue",
                        Type = FieldType.FieldTypeDate.Name,
                        Label = "DateValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "DateArrayValue",
                        Type = FieldType.FieldTypeDateArray.Name,
                        Label = "DateArrayValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "TimeValue",
                        Type = FieldType.FieldTypeTime.Name,
                        Label = "TimeValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "TimeArrayValue",
                        Type = FieldType.FieldTypeTimeArray.Name,
                        Label = "TimeArrayValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "DateTimeValue",
                        Type = FieldType.FieldTypeDateTime.Name,
                        Label = "DateTimeValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    },
                    new Field
                    {
                        Name = "DateTimeArrayValue",
                        Type = FieldType.FieldTypeDateTimeArray.Name,
                        Label = "DateTimeArrayValue",
                        Operators = new[]
                        {
                            Operator.OperatorEqual.Name,
                            Operator.OperatorNotEqual.Name
                        },
                        Values = Array.Empty<Lookup>()
                    }
                }
            };

            var baseQueryTokenizer = new BaseQueryTokenizer(metadata);

            var samples = new Dictionary<string, object[]>
            {
                {
                    "StringValue",
                    new object[]
                    {
                        100, "100"
                    }
                },
                {
                    "StringArrayValue",
                    new object[]
                    {
                        100, "100"
                    }
                },
                {
                    "NumberValue",
                    new object[]
                    {
                        "100", 100.0
                    }
                },
                {
                    "NumberArrayValue",
                    new object[]
                    {
                        "100", 100.0
                    }
                },
                {
                    "BooleanValue",
                    new object[]
                    {
                        "True", true
                    }
                },
                {
                    "BooleanArrayValue",
                    new object[]
                    {
                        "False", false
                    }
                },
                {
                    "DateValue",
                    new object[]
                    {
                        "2020-01-01", new DateTime(2020, 01, 01)
                    }
                },
                {
                    "DateArrayValue",
                    new object[]
                    {
                        "2020-01-01", new DateTime(2020, 01, 01)
                    }
                },
                {
                    "TimeValue",
                    new object[]
                    {
                        "1h15m", 60 * 60 + 15 * 60
                    }
                },
                {
                    "TimeArrayValue",
                    new object[]
                    {
                        "1h15m", 60 * 60 + 15 * 60
                    }
                },
                {
                    "DateTimeValue",
                    new object[]
                    {
                        "2020-01-01 11:12:13", new DateTime(2020, 01, 01, 11, 12, 13)
                    }
                },
                {
                    "DateTimeArrayValue",
                    new object[]
                    {
                        "2020-01-01 11:12:13", new DateTime(2020, 01, 01, 11, 12, 13)
                    }
                }
            };

            foreach (var (field, sample) in samples)
            {
                // Act
                var result = baseQueryTokenizer.CastValue(field, sample[0]);

                // Assert
                Assert.Equal(sample[1], result);
            }
        }
    }
}