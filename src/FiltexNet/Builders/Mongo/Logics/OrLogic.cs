using System;
using System.Linq;
using FiltexNet.Builders.Mongo.Types;
using MongoDB.Bson;

namespace FiltexNet.Builders.Mongo.Logics
{
    public static class OrLogic
    {
        public static MongoExpression Build(MongoExpression[] expressions)
        {
            var conditions = Array.Empty<BsonDocument>();

            foreach (var exp in expressions)
            {
                conditions = conditions.Append(exp.Condition).ToArray();
            }

            return new MongoExpression
            {
                Condition = new BsonDocument("$or", new BsonArray(conditions))
            };
        }
    }
}