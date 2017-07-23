using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TweetApp.Data.Repositories.Interfaces;
using TweetApp.Domain.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TweetApp.Data.Repositories.MongoDB
{
    public class GeospatialMongoDbRepository<TEntity> : MongoDbRepository<TEntity>, IGeospatialRepository<TEntity> where TEntity : GeospatialEntity
    {
        public GeospatialMongoDbRepository(string connectionString, string collectionName, string entityName)
            : base(connectionString, collectionName, entityName)
        {
        }

        public async Task<IEnumerable<TEntity>> FindWithinCenterSphere(double x, double y, double radius)
        {
            var filter = Builders<TEntity>.Filter.GeoWithinCenterSphere(t => t.Position, x, y, radius);
            return await _entities.GetCollection<TEntity>(_entityName).Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindWithinCenterSphere(double x, double y, double radius, Expression<Func<TEntity, bool>> filter)
        {
            var builder = Builders<TEntity>.Filter;
            var filters = builder.And(builder.Where(filter), builder.GeoWithinCenterSphere(t => t.Position, x, y, radius));

            return await _entities.GetCollection<TEntity>(_entityName).Find(filters).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindWithinPolygon(double[,] points)
        {
            var filter = Builders<TEntity>.Filter.GeoWithinPolygon(t => t.Position, points);
            return await _entities.GetCollection<TEntity>(_entityName).Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindWithinPolygon(double[,] points, Expression<Func<TEntity, bool>> filter)
        {
            var builder = Builders<TEntity>.Filter;
            var filters = builder.And(builder.Where(filter), builder.GeoWithinPolygon(t => t.Position, points));

            return await _entities.GetCollection<TEntity>(_entityName).Find(filters).ToListAsync();
        }
    }
}
