using System.Linq;
using FiltexNet.Expressions;
using FiltexNet.Models;
using FiltexNet.Options;
using FiltexNet.Parsers;
using FiltexNet.Tokenizers;
using FiltexNet.Validators;

namespace FiltexNet
{
    public class Filtex
    {
        private readonly Metadata _metadata;

        private Filtex(Metadata metadata)
        {
            _metadata = metadata;
        }

        public static Filtex New(params IOption[] options)
        {
            var lookups = options
                .OfType<LookupOption>()
                .Select(x => x.Build())
                .ToArray();

            var metadata = new Metadata
            {
                Fields = options
                    .OfType<FieldOption>()
                    .Select(x => x.Build(lookups))
                    .ToArray()
            };

            return new Filtex(metadata);
        }

        public Metadata Metadata()
        {
            return _metadata;
        }

        public IExpression ExpressionFromJson(string jsonQuery)
        {
            return new JsonQueryParser(_metadata, new JsonQueryTokenizer(_metadata)).Parse(jsonQuery);
        }

        public IExpression ExpressionFromText(string textQuery)
        {
            return new TextQueryParser(_metadata, new TextQueryTokenizer(_metadata)).Parse(textQuery);
        }

        public void ValidateFromJson(string jsonQuery)
        {
            new JsonQueryValidator(_metadata, new JsonQueryTokenizer(_metadata)).Validate(jsonQuery);
        }

        public void ValidateFromText(string textQuery)
        {
            new TextQueryValidator(_metadata, new TextQueryTokenizer(_metadata)).Validate(textQuery);
        }
    }
}