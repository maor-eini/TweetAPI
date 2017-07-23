using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<TEntity> Get(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, id);
            return await _entities.GetCollection<TEntity>(_entityName).Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _entities.GetCollection<TEntity>(_entityName).Find(t=>t.Active == true).SortByDescending(t=>t.CreatedOn).Limit(100).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.GetCollection<TEntity>(_entityName).Find(predicate).ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _entities.GetCollection<TEntity>(_entityName).InsertOneAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _entities.GetCollection<TEntity>(_entityName).InsertManyAsync(entities);
        }

        public async Task Remove(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, id);
            var update = Builders<TEntity>.Update.Set(x => x.Active, false);

            await _entities.GetCollection<TEntity>(_entityName).UpdateOneAsync(filter, update);
            //_entities.GetCollection<TEntity>(_entityName).DeleteOne(filter);
        }

        public async Task RemoveRange(Expression<Func<TEntity, bool>> predicate)
        {
            await _entities.GetCollection<TEntity>(_entityName).DeleteManyAsync(predicate);
        }
    }
}
