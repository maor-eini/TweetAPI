using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TweetApp.Domain.Entities
{
    public class Tweet : GeospatialEntity
    {
        [BsonElement("Content")]
        public string Content { get; set; }

    }
}
