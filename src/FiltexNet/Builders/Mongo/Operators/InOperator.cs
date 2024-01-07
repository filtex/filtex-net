using FiltexNet.Builders.Mongo.Types;
using FiltexNet.Constants;
using FiltexNet.Utils;
using MongoDB.Bson;

namespace FiltexNet.Builders.Mongo.Operators
{
    public static class InOperator
    {
        public static MongoExpression Build(FieldType fieldType, string field, object value)
        {
            if (fieldType.IsArray() || value == null)
            {
                return null;
            }

            if (CastUtils.IsArray(value))
            {
                return new MongoExpression
                {
                    Condition = new BsonDocument(field, new BsonDocument("$in", BsonValue.Create(value)))
                };
            }
            else
            {
                return new MongoExpression
                {
                    Condition = new BsonDocument(field, new BsonDocument("$in", BsonValue.Create(new[] { value })))
                };
            }
        }
    }
}