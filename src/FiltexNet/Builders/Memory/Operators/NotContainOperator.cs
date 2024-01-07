using FiltexNet.Builders.Memory.Types;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using FiltexNet.Utils;

namespace FiltexNet.Builders.Memory.Operators
{
    public static class NotContainOperator
    {
        public static MemoryExpression Build(FieldType fieldType, string field, object value)
        {
            if (fieldType.IsArray())
            {
                return new MemoryExpression
                {
                    Fn = data =>
                    {
                        if (!data.TryGetValue(field, out var val))
                        {
                            return false;
                        }

                        foreach (var v in CastUtils.Array(val))
                        {
                            if (MemoryUtils.CheckEquality(fieldType, v, value))
                            {
                                return false;
                            }
                        }

                        return true;
                    }
                };
            }

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
                        return val?.ToString()?.ToLowerInvariant().Contains(value?.ToString()?.ToLowerInvariant() ?? string.Empty) != true;
                    }

                    return false;
                }
            };
        }
    }
}