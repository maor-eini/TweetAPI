using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TweetAPI.Domain.Entities
{
    public class Tweet : BaseEntity
    {
        [BsonElement("Content")]
        public string Content { get; set; }

        [BsonElement("Position")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Position { get; set; }

    }
}
