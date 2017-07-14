using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TweetAPI.Domain.Entities
{
    public class BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public DateTime CreatedOn { get; set; }

        [BsonElement]
        public DateTime LastModifiedOn { get; set; }
    }
}
