using FiltexNet.Builders.Memory.Types;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;

namespace FiltexNet.Builders.Memory.Operators
{
    public static class EqualOperator
    {
        public static MemoryExpression Build(FieldType fieldType, string field, object value)
        {
            if (fieldType.IsArray())
            {
                return new MemoryExpression
                {
                    Fn = data => false
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
                
                    return val != null && MemoryUtils.CheckEquality(fieldType, val, value);
                }
            };
        }
    }
}