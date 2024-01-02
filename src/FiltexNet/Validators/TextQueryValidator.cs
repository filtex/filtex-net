using System;
using System.Linq;
using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;
using FiltexNet.Tokenizers;

namespace FiltexNet.Validators
{
    public class TextQueryValidator
    {
        private readonly Metadata _metadata;
        private readonly ITextQueryTokenizer _queryTokenizer;

        public TextQueryValidator(Metadata metadata, ITextQueryTokenizer queryTokenizer)
        {
            _metadata = metadata;
            _queryTokenizer = queryTokenizer;
        }

        public void Validate(string query)
        {
            ValidateInternal(_queryTokenizer.Tokenize(query));
        }

        private void ValidateInternal(Token[] tokens)
        {
            var tokensExceptSpace = Array.Empty<Token>();
            var openGroupTokenCount = 0;
            var closeGroupTokenCount = 0;

            foreach (var token in tokens)
            {
                if (token.Type == TokenType.TokenTypeNone)
                {
                    throw ValidateException.NewInvalidTokenError();
                }

                if (token.Type != TokenType.TokenTypeSpace)
                {
                    tokensExceptSpace = tokensExceptSpace.Append(token).ToArray();
                }

                if (token.Type.IsOpenGroupTokenType())
                {
                    openGroupTokenCount += 1;
                }

                if (token.Type.IsCloseGroupTokenType())
                {
                    closeGroupTokenCount += 1;
                }
            }

            if (tokensExceptSpace.Length == 0)
            {
                return;
            }

            var lastTokenType = tokensExceptSpace[^1].Type;

            if (lastTokenType.IsFieldTokenType() ||
                lastTokenType.IsComparerTokenType() ||
                lastTokenType.IsSeparatorTokenType() ||
                lastTokenType.IsLogicTokenType() ||
                lastTokenType.IsOpenGroupTokenType())
            {
                throw ValidateException.NewInvalidLastTokenError();
            }

            if (openGroupTokenCount != closeGroupTokenCount)
            {
                throw ValidateException.NewMismatchedBracketsError();
            }
        }
    }
}