using FiltexNet.Builders.Postgres.Types;
using FiltexNet.Constants;

namespace FiltexNet.Builders.Postgres.Operators
{
    public static class NotBlankOperator
    {
        public static PostgresExpression Build(FieldType fieldType, string field, object value, int index)
        {
            if (fieldType.IsArray())
            {
                return new PostgresExpression
                {
                    Condition = $"ARRAY_LENGTH({field}, 1) <> 0",
                    Args = new object[] { }
                };
            }

            if (fieldType.Name == FieldType.FieldTypeString.Name)
            {
                return new PostgresExpression
                {
                    Condition = $"{field} IS NOT NULL AND {field} <> ''",
                    Args = new object[] { }
                };
            }

            return null;
        }
    }
}