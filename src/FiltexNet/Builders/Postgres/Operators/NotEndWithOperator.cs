using FiltexNet.Builders.Postgres.Types;
using FiltexNet.Constants;

namespace FiltexNet.Builders.Postgres.Operators
{
    public static class NotEndWithOperator
    {
        public static PostgresExpression Build(FieldType fieldType, string field, object value, int index)
        {
            if (fieldType.Name != FieldType.FieldTypeString.Name)
            {
                return null;
            }

            return new PostgresExpression
            {
                Condition = $"{field} NOT ILIKE '%' || ${index}",
                Args = new[] { value }
            };
        }
    }
}