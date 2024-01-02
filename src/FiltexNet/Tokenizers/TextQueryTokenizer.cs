using System.Collections.Generic;
using System.Text.RegularExpressions;
using FiltexNet.Constants;
using FiltexNet.Models;

namespace FiltexNet.Tokenizers
{
    public class TextQueryTokenizer : BaseQueryTokenizer, ITextQueryTokenizer
    {
        public TextQueryTokenizer(Metadata metadata) : base(metadata)
        {
        }


        public Token[] Tokenize(string text)
        {
            var tokens = new List<Token>();
            var remainingText = text;

            while (remainingText.Length > 0)
            {
                var match = FindMatch(remainingText);
                if (match != null)
                {
                    var token = CreateToken(tokens.ToArray(), match.TokenType, match.Value);
                    if (token != null)
                    {
                        tokens.Add(token);
                    }
                    remainingText = match.RemainingText;
                }
                else
                {
                    var regex = new Regex("^\\s+").Match(remainingText);
                    if (regex.Success)
                    {
                        var token = CreateToken(tokens.ToArray(), TokenType.TokenTypeSpace, " ");
                        if (token != null)
                        {
                            tokens.Add(token);
                        }
                        remainingText = remainingText.Substring(1);
                    }
                    else
                    {
                        regex = new Regex("(^\\S+\\s)|^\\S+").Match(remainingText);
                        if (regex.Length == 0)
                        {
                            break;
                        }

                        var token = CreateToken(tokens.ToArray(), TokenType.TokenTypeNone, regex.Value);
                        if (token != null)
                        {
                            tokens.Add(token);
                        }
                        remainingText = remainingText.Substring(match.Value.Length);
                    }
                }
            }

            return tokens.ToArray();
        }
    }
}