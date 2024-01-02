using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FiltexNet.Constants;
using FiltexNet.Models;
using FiltexNet.Utils;

namespace FiltexNet.Tokenizers
{
    internal class TokenPattern
    {
        public TokenPattern(string tokenPattern, TokenType tokenType)
        {
            Pattern = tokenPattern;
            Type = tokenType;
        }

        public string Pattern { get; set; }
        public TokenType Type { get; set; }
    }

    public class TokenMatch
    {
        public string RemainingText { get; set; }
        public TokenType TokenType { get; set; }
        public string Value { get; set; }
    }

    public class BaseQueryTokenizer
    {
        private readonly Metadata _metadata;
        private readonly TokenPattern[] _tokenPatterns;

        public BaseQueryTokenizer(Metadata metadata)
        {
            _metadata = metadata;

            var tokenPatterns = new List<TokenPattern>
            {
                new("(?i)^\\(", TokenType.TokenTypeOpenBracket),
                new("(?i)^\\)", TokenType.TokenTypeCloseBracket),

                new("(?i)^,", TokenType.TokenTypeComma),
                new("(?i)^/", TokenType.TokenTypeSlash),

                new("(?i)^and\\b", TokenType.TokenTypeAnd),
                new("(?i)^&&", TokenType.TokenTypeAnd),
                new("(?i)^or\\b", TokenType.TokenTypeOr),
                new("(?i)^\\|\\|", TokenType.TokenTypeOr),

                new("(?i)^=", TokenType.TokenTypeEqual),
                new("(?i)^equal\\b", TokenType.TokenTypeEqual),
                new("(?i)^!=", TokenType.TokenTypeNotEqual),
                new("(?i)^not equal\\b", TokenType.TokenTypeNotEqual),

                new("(?i)^>=", TokenType.TokenTypeGreaterThanOrEqual),
                new("(?i)^greater than or equal\\b", TokenType.TokenTypeGreaterThanOrEqual),
                new("(?i)^>", TokenType.TokenTypeGreaterThan),
                new("(?i)^greater than\\b", TokenType.TokenTypeGreaterThan),

                new("(?i)^<=", TokenType.TokenTypeLessThanOrEqual),
                new("(?i)^less than or equal\\b", TokenType.TokenTypeLessThanOrEqual),
                new("(?i)^<", TokenType.TokenTypeLessThan),
                new("(?i)^less than\\b", TokenType.TokenTypeLessThan),

                new("(?i)^\\[\\]", TokenType.TokenTypeBlank),
                new("(?i)^blank\\b", TokenType.TokenTypeBlank),
                new("(?i)^!\\[\\]", TokenType.TokenTypeNotBlank),
                new("(?i)^not blank\\b", TokenType.TokenTypeNotBlank),

                new("(?i)^~\\*", TokenType.TokenTypeStartWith),
                new("(?i)^start with\\b", TokenType.TokenTypeStartWith),
                new("(?i)^!~\\*", TokenType.TokenTypeNotStartWith),
                new("(?i)^not start with\\b", TokenType.TokenTypeNotStartWith),

                new("(?i)^\\*~", TokenType.TokenTypeEndWith),
                new("(?i)^end with\\b", TokenType.TokenTypeEndWith),
                new("(?i)^!\\*~", TokenType.TokenTypeNotEndWith),
                new("(?i)^not end with\\b", TokenType.TokenTypeNotEndWith),

                new("(?i)^~", TokenType.TokenTypeContain),
                new("(?i)^contain\\b", TokenType.TokenTypeContain),
                new("(?i)^!~", TokenType.TokenTypeNotContain),
                new("(?i)^not contain\\b", TokenType.TokenTypeNotContain),

                new("(?i)^in\\b", TokenType.TokenTypeIn),
                new("(?i)^not in\\b", TokenType.TokenTypeNotIn)
            };

            foreach (var field in metadata.Fields)
            {
                tokenPatterns.Add(new TokenPattern("(?i)^" + field.Label + "\\b", TokenType.TokenTypeField));
                tokenPatterns.Add(new TokenPattern("(?i)^" + field.Name + "\\b", TokenType.TokenTypeField));
            }

            tokenPatterns.AddRange(new[]
            {
                new TokenPattern("(?i)^\"[^\"]*\"", TokenType.TokenTypeStringValue),
                new TokenPattern("(?i)^\'[^\']*\'", TokenType.TokenTypeStringValue),
                new TokenPattern("(?i)^\\d\\d\\d\\d-\\d\\d-\\d\\d \\d\\d:\\d\\d(:\\d\\d)?",
                    TokenType.TokenTypeDateTimeValue),
                new TokenPattern("(?i)^\\d\\d\\d\\d-\\d\\d-\\d\\d", TokenType.TokenTypeDateValue),
                new TokenPattern("(?i)^\\d\\d:\\d\\d(:\\d\\d)?", TokenType.TokenTypeTimeValue),
                new TokenPattern("(?i)^(\\d+h)?( ?\\d+m)?( ?\\d+s)?", TokenType.TokenTypeTimeValue),
                new TokenPattern("(?i)^[0-9]+([.][0-9]+)?", TokenType.TokenTypeNumberValue),
                new TokenPattern("(?i)^(true|false)", TokenType.TokenTypeBooleanValue),
                new TokenPattern("(?i)^[a-zA-Z0-9-_]+", TokenType.TokenTypeLiteral)
            });

            _tokenPatterns = tokenPatterns.ToArray();
        }

        protected internal TokenMatch FindMatch(string text)
        {
            foreach (var tokenPattern in _tokenPatterns)
            {
                var match = new Regex(tokenPattern.Pattern).Match(text);
                if (match.Success && match.Length > 0)
                {
                    var remainingText = "";
                    if (match.Length != text.Length)
                    {
                        remainingText = text.Substring(match.Length);
                    }

                    return new TokenMatch
                    {
                        RemainingText = remainingText,
                        TokenType = tokenPattern.Type,
                        Value = match.Value
                    };
                }
            }

            return null;
        }

        protected internal Token CreateToken(Token[] tokens, TokenType tokenType, string value)
        {
            if (tokenType == TokenType.TokenTypeSpace)
            {
                if (tokens.Length > 0 && tokens[^1].Type == TokenType.TokenTypeSpace)
                {
                    return null;
                }
                return new Token
                {
                    Type = tokenType,
                    Value = value
                };
            }

            var allTokens = new List<Token>();
            var lastToken = null as Token;
            var lastTokenType = TokenType.TokenTypeNone;
            var lastFieldToken = null as Token;
            var lastOperatorToken = null as Token;

            foreach (var token in tokens)
            {
                if (token.Type == TokenType.TokenTypeSpace)
                {
                    continue;
                }

                allTokens.Add(token);

                if (token.Type.IsFieldTokenType())
                {
                    lastFieldToken = token;
                }
                else if (token.Type.IsOperatorTokenType())
                {
                    lastOperatorToken = token;
                }

                lastToken = token;
                lastTokenType = token.Type;
            }

            if (allTokens.Count == 0)
            {
                if (tokenType == TokenType.TokenTypeField || tokenType == TokenType.TokenTypeLiteral)
                {
                    if (ValidateField(value))
                    {
                        return new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = value
                        };
                    }
                    return new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = value
                    };
                }

                if (tokenType.IsOpenGroupTokenType())
                {
                    return new Token
                    {
                        Type = tokenType,
                        Value = value
                    };
                }
            }
            else if (tokenType == TokenType.TokenTypeField)
            {
                if (lastTokenType.IsPreFieldTokenType())
                {
                    if (ValidateField(value))
                    {
                        return new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = value
                        };
                    }
                    return new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = value
                    };
                }

                if (lastTokenType.IsComparerTokenType() || lastTokenType.IsSeparatorTokenType())
                {
                    var lookupValue = value;

                    foreach (var v in _metadata.GetFieldValues(lastFieldToken?.Value?.ToString()))
                    {
                        if (v.Name.ToLowerInvariant() == value.ToLowerInvariant())
                        {
                            lookupValue = v.Value.ToString();
                            break;
                        }
                    }

                    if (ValidateValue(lastFieldToken.Value, lookupValue))
                    {
                        if (lastOperatorToken != null && lastOperatorToken.Type.IsComparerTokenType())
                        {
                            return new Token
                            {
                                Type = TokenType.TokenTypeValue,
                                Value = CastValue(lastFieldToken.Value, lookupValue)
                            };
                        }
                        return new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = value
                        };
                    }

                    return new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = value
                    };
                }
            }
            else if (tokenType == TokenType.TokenTypeLiteral)
            {
                if (lastTokenType.IsComparerTokenType() || lastTokenType.IsSeparatorTokenType())
                {
                    var lookupValue = value;

                    foreach (var v in _metadata.GetFieldValues(lastFieldToken?.Value?.ToString()))
                    {
                        if (v.Name.ToLowerInvariant() == value.ToLowerInvariant())
                        {
                            lookupValue = v.Value.ToString();
                            break;
                        }
                    }

                    if (lastFieldToken != null && ValidateValue(lastFieldToken.Value, lookupValue))
                    {
                        if (lastOperatorToken != null && lastOperatorToken.Type.IsComparerTokenType())
                        {
                            return new Token
                            {
                                Type = TokenType.TokenTypeValue,
                                Value = CastValue(lastFieldToken.Value, lookupValue)
                            };
                        }
                        return new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = value
                        };
                    }

                    return new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = value
                    };
                }

                if (lastTokenType.IsPreFieldTokenType())
                {
                    if (ValidateField(value))
                    {
                        return new Token
                        {
                            Type = TokenType.TokenTypeField,
                            Value = value
                        };
                    }
                    return new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = value
                    };
                }
            }
            else if (tokenType.IsValueTokenType())
            {
                if (lastTokenType.IsComparerTokenType() || lastTokenType.IsSeparatorTokenType())
                {
                    if (lastFieldToken != null && ValidateValue(lastFieldToken.Value, value))
                    {
                        if (lastOperatorToken != null && lastOperatorToken.Type.IsComparerTokenType())
                        {
                            return new Token
                            {
                                Type = tokenType,
                                Value = CastValue(lastFieldToken.Value, value)
                            };
                        }
                        return new Token
                        {
                            Type = TokenType.TokenTypeNone,
                            Value = value
                        };
                    }

                    return new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = value
                    };
                }
            }
            else if (tokenType.IsOperatorTokenType())
            {
                if (lastTokenType == TokenType.TokenTypeField)
                {
                    var op = tokenType.ToOperator();

                    if (ValidateOperator(lastToken.Value, op.Name))
                    {
                        return new Token
                        {
                            Type = tokenType,
                            Value = value
                        };
                    }
                    return new Token
                    {
                        Type = TokenType.TokenTypeNone,
                        Value = value
                    };
                }
            }
            else if (tokenType.IsLogicTokenType())
            {
                if (lastTokenType.IsValueTokenType() || lastTokenType.IsCloseGroupTokenType() ||
                    lastTokenType.IsNotComparerTokenType())
                {
                    return new Token
                    {
                        Type = tokenType,
                        Value = value
                    };
                }
            }
            else if (tokenType.IsOpenGroupTokenType())
            {
                if (lastTokenType.IsLogicTokenType() || lastTokenType.IsOpenGroupTokenType())
                {
                    return new Token
                    {
                        Type = tokenType,
                        Value = value
                    };
                }
            }
            else if (tokenType.IsCloseGroupTokenType())
            {
                var openGroupTokenCount = 0;
                var closeGroupTokenCount = 0;

                foreach (var token in allTokens)
                {
                    if (token.Type.IsOpenGroupTokenType())
                    {
                        openGroupTokenCount++;
                    }
                    else if (token.Type.IsCloseGroupTokenType())
                    {
                        closeGroupTokenCount++;
                    }
                }

                if (openGroupTokenCount > closeGroupTokenCount &&
                    (lastTokenType.IsValueTokenType() || lastTokenType.IsCloseGroupTokenType() ||
                     lastTokenType.IsNotComparerTokenType()))
                {
                    return new Token
                    {
                        Type = tokenType,
                        Value = value
                    };
                }
            }
            else if (tokenType.IsSeparatorTokenType())
            {
                if (lastOperatorToken != null &&
                    lastOperatorToken.Type.IsComparerTokenType() &&
                    lastOperatorToken.Type.IsMultiAllowedTokenType())
                {
                    if (lastTokenType.IsValueTokenType())
                    {
                        return new Token
                        {
                            Type = tokenType,
                            Value = value
                        };
                    }
                }
            }

            return new Token
            {
                Type = TokenType.TokenTypeNone,
                Value = value
            };
        }

        internal bool ValidateField(string field)
        {
            foreach (var item in _metadata.Fields)
            {
                if (string.Equals(item.Label, field, StringComparison.InvariantCultureIgnoreCase) ||
                    string.Equals(item.Name, field, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        internal bool ValidateOperator(object field, object value)
        {
            var fieldValue = null as Field;

            foreach (var item in _metadata.Fields)
            {
                if (string.Equals(item.Label, field.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                    string.Equals(item.Name, field.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    fieldValue = new Field
                    {
                        Operators = item.Operators
                    };
                }
            }

            if (fieldValue == null)
            {
                return false;
            }

            foreach (var item in fieldValue.Operators)
            {
                if (string.Equals(item, value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        internal bool ValidateValue(object field, object value)
        {
            var fieldValue = null as Field;

            foreach (var item in _metadata.Fields)
            {
                if (string.Equals(item.Label, field.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                    string.Equals(item.Name, field.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    fieldValue = new Field
                    {
                        Type = item.Type,
                        Values = item.Values
                    };
                }
            }

            if (fieldValue == null)
            {
                return false;
            }

            if (fieldValue.Values.Length == 0)
            {
                if (fieldValue.Type == FieldType.FieldTypeString.Name ||
                    fieldValue.Type == FieldType.FieldTypeStringArray.Name)
                {
                    return CastUtils.IsString(value);
                }

                if (fieldValue.Type == FieldType.FieldTypeNumber.Name ||
                    fieldValue.Type == FieldType.FieldTypeNumberArray.Name)
                {
                    return CastUtils.IsNumber(value);
                }

                if (fieldValue.Type == FieldType.FieldTypeBoolean.Name ||
                    fieldValue.Type == FieldType.FieldTypeBooleanArray.Name)
                {
                    return CastUtils.IsBoolean(value);
                }

                if (fieldValue.Type == FieldType.FieldTypeDate.Name ||
                    fieldValue.Type == FieldType.FieldTypeDateArray.Name)
                {
                    return CastUtils.IsDate(value);
                }

                if (fieldValue.Type == FieldType.FieldTypeTime.Name ||
                    fieldValue.Type == FieldType.FieldTypeTimeArray.Name)
                {
                    return CastUtils.IsTime(value);
                }

                if (fieldValue.Type == FieldType.FieldTypeDateTime.Name ||
                    fieldValue.Type == FieldType.FieldTypeDateTimeArray.Name)
                {
                    return CastUtils.IsDateTime(value);
                }

                return false;
            }

            foreach (var item in fieldValue.Values)
            {
                var vVal = Convert.ToString(item.Value);
                var vName = Convert.ToString(item.Name);
                var val = Convert.ToString(value);

                if (string.Equals(vVal, val, StringComparison.InvariantCulture) ||
                    string.Equals(vName, val, StringComparison.InvariantCulture))
                {
                    return true;
                }
            }

            return false;
        }

        internal object CastValue(object field, object value)
        {
            var fieldType = _metadata.GetFieldType(field.ToString());

            if (fieldType.Name == FieldType.FieldTypeUnknown.Name)
            {
                return value;
            }

            if (fieldType.Name == FieldType.FieldTypeString.Name ||
                fieldType.Name == FieldType.FieldTypeStringArray.Name)
            {
                return CastUtils.String(value).Trim('\'', '"');
            }

            if (fieldType.Name == FieldType.FieldTypeNumber.Name ||
                fieldType.Name == FieldType.FieldTypeNumberArray.Name)
            {
                return CastUtils.Number(value);
            }

            if (fieldType.Name == FieldType.FieldTypeBoolean.Name ||
                fieldType.Name == FieldType.FieldTypeBooleanArray.Name)
            {
                return CastUtils.Boolean(value);
            }

            if (fieldType.Name == FieldType.FieldTypeDate.Name ||
                fieldType.Name == FieldType.FieldTypeDateArray.Name)
            {
                return CastUtils.Date(value);
            }

            if (fieldType.Name == FieldType.FieldTypeTime.Name ||
                fieldType.Name == FieldType.FieldTypeTimeArray.Name)
            {
                return CastUtils.Time(value);
            }

            if (fieldType.Name == FieldType.FieldTypeDateTime.Name ||
                fieldType.Name == FieldType.FieldTypeDateTimeArray.Name)
            {
                return CastUtils.DateTime(value);
            }

            return value;
        }
    }
}