using FiltexNet.Exceptions;
using FiltexNet.Models;

namespace FiltexNet.Options
{
    public class LookupOption : IOption
    {
        internal string _key;
        internal Lookup[] _values;

        public static LookupOption New()
        {
            return new LookupOption();
        }

        public LookupOption Key(string key)
        {
            _key = key;
            return this;
        }

        public LookupOption Values(Lookup[] values)
        {
            _values = values;
            return this;
        }

        public (string, Lookup[]) Build()
        {
            if (string.IsNullOrEmpty(_key))
            {
                throw LookupException.NewInvalidLookupKeyError();
            }

            if (_values == null || _values.Length == 0)
            {
                throw LookupException.NewInvalidLookupValuesError();
            }

            return (_key, _values);
        }
    }
}