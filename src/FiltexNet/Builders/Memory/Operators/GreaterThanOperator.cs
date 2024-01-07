using FiltexNet.Builders.Memory.Types;
using FiltexNet.Constants;
using FiltexNet.Utils;

namespace FiltexNet.Builders.Memory.Operators
{
    public static class GreaterThanOperator
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

                    if (val == null)
                    {
                        return false;
                    }

                    if (fieldType.Name == FieldType.FieldTypeNumber.Name)
                    {
                        return CastUtils.Number(val) > CastUtils.Number(value);
                    }

                    if (fieldType.Name == FieldType.FieldTypeDate.Name)
                    {
                        return CastUtils.Date(val) > CastUtils.Date(value);
                    }

                    if (fieldType.Name == FieldType.FieldTypeTime.Name)
                    {
                        return CastUtils.Time(val) > CastUtils.Time(value);
                    }

                    if (fieldType.Name == FieldType.FieldTypeDateTime.Name)
                    {
                        return CastUtils.DateTime(val) > CastUtils.DateTime(value);
                    }

                    return false;
                }
            };
        }
    }
}