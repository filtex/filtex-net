using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;
using FiltexNet.Tokenizers;

namespace FiltexNet.Validators
{
    public class JsonQueryValidator
    {
        private readonly Metadata _metadata;
        private readonly IJsonQueryTokenizer _queryTokenizer;

        public JsonQueryValidator(Metadata metadata, IJsonQueryTokenizer queryTokenizer)
        {
            _metadata = metadata;
            _queryTokenizer = queryTokenizer;
        }

        public void Validate(string query)
        {
            ValidateInternal(_queryTokenizer.Tokenize(query));
        }

        private void ValidateInternal(object[] data)
        {
            if (data.Length == 3)
            {
                var fieldToken = data[0] as Token;
                var operatorToken = data[1] as Token;

                if (data[2] is Token[] valueTokens)
                {
                    foreach (var valueToken in valueTokens)
                    {
                        if (valueToken.Type == TokenType.TokenTypeNone)
                        {
                            throw ValidateException.NewInvalidValueError();
                        }
                    }
                }
                else if (data[2] is Token valueToken)
                {
                    if (valueToken.Type == TokenType.TokenTypeNone)
                    {
                        throw ValidateException.NewInvalidValueError();
                    }
                }

                if (fieldToken == null || fieldToken.Type == TokenType.TokenTypeNone)
                {
                    throw ValidateException.NewInvalidFieldError();
                }

                if (operatorToken == null || operatorToken.Type == TokenType.TokenTypeNone)
                {
                    throw ValidateException.NewInvalidOperatorError();
                }
            }
            else if (data.Length == 2)
            {
                var logicToken = data[0] as Token;

                if (logicToken == null || logicToken.Type == TokenType.TokenTypeNone)
                {
                    throw ValidateException.NewInvalidLogicError();
                }

                var array = data[1] as object[];

                foreach (var item in array)
                {
                    ValidateInternal(item as object[]);
                }
            }
            else
            {
                throw ValidateException.NewCouldNotBeValidatedError();
            }
        }
    }
}