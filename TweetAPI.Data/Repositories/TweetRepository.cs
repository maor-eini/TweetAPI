using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TweetAPI.Data.Repositories.Interfaces;
using TweetAPI.Domain.Entities;

namespace TweetAPI.Data.Repositories
{
    public class TweetRepository : MongoDbRepository<Tweet>, ITweetRepository
    {

        public TweetRepository(string connectionString, string collectionName, string entityName) :
            base(connectionString, collectionName, entityName)
        { }

        public IEnumerable<Tweet> GetTweetsWithinCenterSphere(double x, double y, double radius)
        {
            var filter = Builders<Tweet>.Filter.GeoWithinCenterSphere(t => t.Position, x, y, radius);
            return _entities.GetCollection<Tweet>(_entityName).Find(filter).ToList();
        }

        public IEnumerable<Tweet> GetTweetsWithinPolygon(double [,] points)
        {
            var filter = Builders<Tweet>.Filter.GeoWithinPolygon(t => t.Position, points);
            return _entities.GetCollection<Tweet>(_entityName).Find(filter).ToList();
        }
    }
}
