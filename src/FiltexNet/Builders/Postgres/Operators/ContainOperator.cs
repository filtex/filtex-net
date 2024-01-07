using FiltexNet.Builders.Postgres.Types;
using FiltexNet.Constants;

namespace FiltexNet.Builders.Postgres.Operators
{
    public static class ContainOperator
    {
        public static PostgresExpression Build(FieldType fieldType, string field, object value, int index)
        {
            if (fieldType.IsArray())
            {
                if (fieldType.Name == FieldType.FieldTypeStringArray.Name)
                {
                    return new PostgresExpression
                    {
                        Condition = $"LOWER(${index}) = ANY (LOWER({field}::TEXT)::TEXT[])",
                        Args = new[] { value }
                    };
                }

                return new PostgresExpression
                {
                    Condition = $"${index} = ANY ({field})",
                    Args = new[] { value }
                };
            }

            if (fieldType.Name == FieldType.FieldTypeString.Name)
            {
                return new PostgresExpression
                {
                    Condition = $"{field} ILIKE '%' || ${index} || '%'",
                    Args = new[] { value }
                };
            }

            return null;
        }
    }
}