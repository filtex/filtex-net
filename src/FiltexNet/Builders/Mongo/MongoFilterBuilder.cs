using FiltexNet.Builders.Mongo.Types;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;

namespace FiltexNet.Builders.Mongo
{
    public class MongoFilterBuilder
    {
        public MongoExpression Build(IExpression expression)
        {
            throw BuildException.NewCouldNotBeBuiltError();
        }
    }
}