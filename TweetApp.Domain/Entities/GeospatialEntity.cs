using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TweetApp.Domain.Entities
{
    public class GeospatialEntity : RootEntity
    {
        [BsonElement("Position")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Position { get; set; }
    }
}
