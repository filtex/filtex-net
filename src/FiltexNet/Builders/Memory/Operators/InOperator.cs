using FiltexNet.Builders.Memory.Types;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using FiltexNet.Utils;

namespace FiltexNet.Builders.Memory.Operators
{
    public static class InOperator
    {
        public static MemoryExpression Build(FieldType fieldType, string field, object value)
        {
            if (fieldType.IsArray() || value == null)
            {
                return new MemoryExpression
                {
                    Fn = _ => false
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

                    if (!CastUtils.IsArray(value))
                    {
                        return MemoryUtils.CheckEquality(fieldType, val, value);
                    }

                    foreach (var v in CastUtils.Array(value))
                    {
                        if (MemoryUtils.CheckEquality(fieldType, val, v))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            };
        }
    }
}