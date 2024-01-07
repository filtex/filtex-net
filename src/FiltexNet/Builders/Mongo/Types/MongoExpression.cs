using MongoDB.Bson;

namespace FiltexNet.Builders.Mongo.Types
{
    public class MongoExpression
    {
        public BsonDocument Condition { get; set; }
    }
}