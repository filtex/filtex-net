using FiltexNet.Builders.Postgres.Types;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;

namespace FiltexNet.Builders.Postgres
{
    public class PostgresFilterBuilder
    {
        public PostgresExpression Build(IExpression expression)
        {
            throw BuildException.NewCouldNotBeBuiltError();
        }
    }
}