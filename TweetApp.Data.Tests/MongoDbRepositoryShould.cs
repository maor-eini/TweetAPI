using MongoDB.Driver;
using System.Collections.Generic;
using TweetApp.Data.Repositories.MongoDB;
using TweetApp.Data.Tests.Model;
using TweetApp.Domain.Entities;
using Xunit;

namespace TweetApp.Data.Tests
{
    public class MongoDbRepositoryShould
    {
        private static readonly string _connectionString = "mongodb://localhost:27017";
        private static readonly string _databaseName = "test";
        private static readonly string _collectionName = "tweets";

        private IMongoDatabase _database { get; set; }

        public MongoDbRepositoryShould()
        {
            var mongoClient = new MongoClient(_connectionString);
            _database = mongoClient.GetDatabase(_databaseName);
        }

        [Fact]
        public async void Get_NewEntity_True()
        {
            var mongoDbRepository = new MongoDbRepository<Tweet>(_connectionString, _databaseName, _collectionName);

            var tweet = new Tweet
            {
                Content = "This is a tweet"
            };

            await _database.GetCollection<Tweet>(_collectionName).InsertOneAsync(tweet);

            var result = await mongoDbRepository.Get(tweet.Id);

            Assert.True(result != null ? result.Id == tweet.Id : false);
        }

        [Fact]
        public async void Remove_NewEntity_True()
        {
            var mongoDbRepository = new MongoDbRepository<Tweet>(_connectionString, _databaseName, _collectionName);

            var tweet = new Tweet
            {
                Content = "This is a testweet"
            };

            await _database.GetCollection<Tweet>(_collectionName).InsertOneAsync(tweet);

            mongoDbRepository.Remove(tweet.Id);

            var result = await _database.GetCollection<Tweet>(_collectionName).Find(x => x.Id == tweet.Id).SingleOrDefaultAsync();

            Assert.True(result == null ? true : false);
        }

        [Fact]
        public async void Add_NewEntity_True()
        {
            var mongoDbRepository = new MongoDbRepository<Tweet>(_connectionString, _databaseName, _collectionName);

            var tweet = new Tweet
            {
                Content = "This is a tweet"
            };

            mongoDbRepository.Add(tweet);

            var result = await _database.GetCollection<Tweet>(_collectionName).Find(x => x.Id == tweet.Id).SingleOrDefaultAsync();

            Assert.True(result != null ? result.Id == tweet.Id : false);
        }

        [Fact]
        public async void GetRecentlyAdded_NewEntites_True()
        {
            var mongoDbRepository = new MongoDbRepository<Tweet>(_connectionString, _databaseName, _collectionName);

            var tweets = new List<Tweet>() {
                new Tweet
                {
                    Content = "This is a tweet"
                },
                new Tweet
                {
                    Content = "This is another tweet"
                }
            };

            await _database.GetCollection<Tweet>(_collectionName).InsertManyAsync(tweets);
            var all = await _database.GetCollection<Tweet>(_collectionName).Find(FilterDefinition<Tweet>.Empty).ToListAsync();

            var result = await mongoDbRepository.Get();

            Assert.Equal<RootEntity>(all, result, new RootEntityComparer());
        }
    }
}
