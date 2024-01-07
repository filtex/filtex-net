using FiltexNet.Builders.Memory.Types;
using FiltexNet.Constants;

namespace FiltexNet.Builders.Memory.Operators
{
    public static class StartWithOperator
    {
        public static MemoryExpression Build(FieldType fieldType, string field, object value)
        {
            return new MemoryExpression
            {
                Fn = data =>
                {
                    if (!data.TryGetValue(field, out var val))
                    {
                        return false;
                    }

                    if (fieldType.Name == FieldType.FieldTypeString.Name)
                    {
                        return val?.ToString()?.ToLowerInvariant().StartsWith(value?.ToString()?.ToLowerInvariant() ?? string.Empty) == true;
                    }

                    return false;
                }
            };
        }
    }
}