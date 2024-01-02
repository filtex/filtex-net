using System.Linq;
using FiltexNet.Models;
using FiltexNet.Options;

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
    }
}