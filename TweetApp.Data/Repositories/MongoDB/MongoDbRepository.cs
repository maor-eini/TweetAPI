using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TweetApp.Data.Repositories.Interfaces;
using TweetApp.Domain.Entities;

namespace TweetApp.Data.Repositories.MongoDB
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : RootEntity
    {
        protected readonly IMongoDatabase _entities;
        protected string _entityName;

        public MongoDbRepository(string connectionString, string collectionName, string entityName)
        {
            var mongoClient = new MongoClient(connectionString);
            _entities = mongoClient.GetDatabase(collectionName);
            _entityName = entityName;
        }

        public TEntity Get(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, id);
            return _entities.GetCollection<TEntity>(_entityName).Find(filter).FirstOrDefault();
        }

        public IEnumerable<TEntity> Get()
        {
            return _entities.GetCollection<TEntity>(_entityName).Find(t=>t.Active == true).SortByDescending(t=>t.CreatedOn).Limit(100).ToList();
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
            var update = Builders<TEntity>.Update.Set(x => x.Active, false);

            _entities.GetCollection<TEntity>(_entityName).UpdateOne(filter, update);
            //_entities.GetCollection<TEntity>(_entityName).DeleteOne(filter);
        }

        public void RemoveRange(Expression<Func<TEntity, bool>> predicate)
        {
            _entities.GetCollection<TEntity>(_entityName).DeleteMany(predicate);
        }
    }
}
