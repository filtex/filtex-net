using System.Collections.Generic;
using FiltexNet.Builders.Mongo.Types;
using FiltexNet.Constants;
using MongoDB.Bson;

namespace FiltexNet.Builders.Mongo.Operators
{
    public static class EndWithOperator
    {
        public static MongoExpression Build(FieldType fieldType, string field, object value)
        {
            if (fieldType.Name != FieldType.FieldTypeString.Name)
            {
                return null;
            }

            return new MongoExpression
            {
                Condition = new BsonDocument(field, new BsonDocument(new List<BsonElement>
                {
                    new("$regex", BsonValue.Create($"{value}$")),
                    new("$options", BsonValue.Create("i"))
                }))
            };
        }
    }
}