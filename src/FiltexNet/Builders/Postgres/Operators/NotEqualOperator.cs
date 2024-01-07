using FiltexNet.Builders.Postgres.Types;
using FiltexNet.Constants;

namespace FiltexNet.Builders.Postgres.Operators
{
    public static class NotEqualOperator
    {
        public static PostgresExpression Build(FieldType fieldType, string field, object value, int index)
        {
            if (fieldType.IsArray())
            {
                return null;
            }

            if (fieldType.Name == FieldType.FieldTypeString.Name)
            {
                return new PostgresExpression
                {
                    Condition = $"{field} NOT ILIKE ${index}",
                    Args = new[] { value }
                };
            }

            return new PostgresExpression
            {
                Condition = $"{field} <> ${index}",
                Args = new[] { value }
            };
        }
    }
}