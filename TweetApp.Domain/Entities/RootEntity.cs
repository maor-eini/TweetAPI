using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TweetApp.Domain.Entities
{
    public class RootEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public bool Active { get; set; }

        [BsonElement]
        public DateTime CreatedOn { get; set; }

        [BsonElement]
        public DateTime LastModified { get; set; }

    }
}
