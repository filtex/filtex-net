using FiltexNet.Builders.Postgres.Types;
using FiltexNet.Constants;

namespace FiltexNet.Builders.Postgres.Operators
{
    public static class GreaterThanOperator
    {
        public static PostgresExpression Build(FieldType fieldType, string field, object value, int index)
        {
            if (fieldType.Name != FieldType.FieldTypeNumber.Name &&
                fieldType.Name != FieldType.FieldTypeDate.Name &&
                fieldType.Name != FieldType.FieldTypeTime.Name &&
                fieldType.Name != FieldType.FieldTypeDateTime.Name)
            {
                return null;
            }

            return new PostgresExpression
            {
                Condition = $"{field} > ${index}",
                Args = new[] { value }
            };
        }
    }
}