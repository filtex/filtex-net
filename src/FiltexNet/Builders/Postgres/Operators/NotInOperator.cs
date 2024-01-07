using System;
using System.Collections.Generic;
using System.Linq;
using FiltexNet.Builders.Postgres.Types;
using FiltexNet.Constants;
using FiltexNet.Utils;

namespace FiltexNet.Builders.Postgres.Operators
{
    public static class NotInOperator
    {
        public static PostgresExpression Build(FieldType fieldType, string field, object value, int index)
        {
            if (fieldType.IsArray())
            {
                return null;
            }

            if (!CastUtils.IsArray(value))
            {
                if (fieldType.Name == FieldType.FieldTypeString.Name)
                {
                    return new PostgresExpression
                    {
                        Condition = $"LOWER({field}) NOT IN (LOWER(${index}))",
                        Args = new[] { value }
                    };
                }
                else
                {
                    return new PostgresExpression
                    {
                        Condition = $"{field} NOT IN (${index})",
                        Args = new[] { value }
                    };
                }
            }

            if (fieldType.Name == FieldType.FieldTypeString.Name)
            {
                var indexes = Array.Empty<string>().AsEnumerable();

                for (var i = index; i < index + ((IEnumerable<object>)value).Count(); i++)
                {
                    indexes = indexes.Append($"LOWER(${i})");
                }

                return new PostgresExpression
                {
                    Condition = $"LOWER({field}) NOT IN ({string.Join(",", indexes)})",
                    Args = ((IEnumerable<object>)value).ToArray()
                };
            }
            else
            {
                var indexes = Array.Empty<string>().AsEnumerable();

                for (var i = index; i < index + ((IEnumerable<object>)value).Count(); i++)
                {
                    indexes = indexes.Append($"${i}");
                }

                return new PostgresExpression
                {
                    Condition = $"{field} NOT IN ({string.Join(",", indexes)})",
                    Args = ((IEnumerable<object>)value).ToArray()
                };
            }
        }
    }
}