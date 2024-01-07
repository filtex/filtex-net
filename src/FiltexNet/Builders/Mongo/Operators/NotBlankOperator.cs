using System.Collections.Generic;
using FiltexNet.Builders.Mongo.Types;
using FiltexNet.Constants;
using MongoDB.Bson;

namespace FiltexNet.Builders.Mongo.Operators
{
    public static class NotBlankOperator
    {
        public static MongoExpression Build(FieldType fieldType, string field, object value)
        {
            if (fieldType.IsArray())
            {
                return new MongoExpression
                {
                    Condition = new BsonDocument($"{field}.0", new BsonDocument("$exists", BsonValue.Create(true)))
                };
            }

            if (fieldType == FieldType.FieldTypeString)
            {
                return new MongoExpression
                {
                    Condition = new BsonDocument(field, new BsonDocument(new List<BsonElement>
                    {
                        new("$exists", BsonValue.Create(true)),
                        new("$ne", BsonValue.Create(""))
                    }))
                };
            }

            return null;
        }
    }
}