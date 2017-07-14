using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TweetAPI.Data.Repositories.Interfaces;
using TweetAPI.Domain.Entities;

namespace TweetAPI.Data.Repositories
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IMongoDatabase _entities;
        protected string _entityName { get; set; }

        public MongoDbRepository(string connectionString, string database, string entityName)
        {
            var mongoClient = new MongoClient(connectionString);
            _entities = mongoClient.GetDatabase(database);
            _entityName = entityName;
        }

        public TEntity Get(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, id);
            return _entities.GetCollection<TEntity>(_entityName).Find(filter).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.GetCollection<TEntity>(_entityName).Find(FilterDefinition<TEntity>.Empty).ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.GetCollection<TEntity>(_entityName).Find(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            _entities.GetCollection<TEntity>(_entityName).InsertOne(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.GetCollection<TEntity>(_entityName).InsertMany(entities);
        }

        public void Remove(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, id);
            _entities.GetCollection<TEntity>(_entityName).DeleteOne(filter);
        }

        public void RemoveRange(Expression<Func<TEntity, bool>> predicate)
        {
            _entities.GetCollection<TEntity>(_entityName).DeleteMany(predicate);
        }

    }
}
