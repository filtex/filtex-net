using FiltexNet.Builders.Mongo.Types;
using FiltexNet.Constants;
using MongoDB.Bson;

namespace FiltexNet.Builders.Mongo.Operators
{
    public static class LessThanOrEqualOperator
    {
        public static MongoExpression Build(FieldType fieldType, string field, object value)
        {
            if (fieldType != FieldType.FieldTypeNumber &&
                fieldType != FieldType.FieldTypeDate &&
                fieldType != FieldType.FieldTypeTime &&
                fieldType != FieldType.FieldTypeDateTime)
            {
                return null;
            }

            return new MongoExpression
            {
                Condition = new BsonDocument(field, new BsonDocument("$lte", BsonValue.Create(value)))
            };
        }
    }
}