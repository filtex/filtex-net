using System;
using System.Linq;
using FiltexNet.Builders.Postgres.Types;

namespace FiltexNet.Builders.Postgres.Logics
{
    public static class AndLogic
    {
        public static PostgresExpression Build(PostgresExpression[] expressions)
        {
            var conditions = Array.Empty<string>();
            var args = Array.Empty<object>();

            foreach (var exp in expressions)
            {
                conditions = conditions.Append($"({exp.Condition})").ToArray();

                foreach (var arg in exp.Args)
                {
                    args = args.Append(arg).ToArray();
                }
            }

            return new PostgresExpression
            {
                Condition = string.Join(" AND ", conditions),
                Args = args
            };
        }
    }
}