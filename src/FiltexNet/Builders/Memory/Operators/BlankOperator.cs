using System;
using FiltexNet.Builders.Memory.Types;
using FiltexNet.Constants;

namespace FiltexNet.Builders.Memory.Operators
{
    public static class BlankOperator
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

                        return val == null || ((Array)val).Length == 0;
                    }
                };
            }

            if (fieldType == FieldType.FieldTypeString)
            {
                return new MemoryExpression
                {
                    Fn = data =>
                    {
                        if (!data.TryGetValue(field, out var val))
                        {
                            return false;
                        }

                        return val == null || val.ToString()?.Length == 0;
                    }
                };
            }

            return new MemoryExpression
            {
                Fn = data => false
            };
        }
    }
}