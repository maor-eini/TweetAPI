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
        public void Get_NewEntity_True()
        {
            var mongoDbRepository = new MongoDbRepository<Tweet>(_connectionString, _databaseName, _collectionName);

            var tweet = new Tweet
            {
                Content = "This is a tweet"
            };

            _database.GetCollection<Tweet>(_collectionName).InsertOne(tweet);

            var result = mongoDbRepository.Get(tweet.Id);

            Assert.True(result != null ? result.Id == tweet.Id : false);
        }

        [Fact]
        public void Remove_NewEntity_True()
        {
            var mongoDbRepository = new MongoDbRepository<Tweet>(_connectionString, _databaseName, _collectionName);

            var tweet = new Tweet
            {
                Content = "This is a testweet"
            };

            _database.GetCollection<Tweet>(_collectionName).InsertOne(tweet);

            mongoDbRepository.Remove(tweet.Id);

            var result = _database.GetCollection<Tweet>(_collectionName).Find(x => x.Id == tweet.Id).SingleOrDefault();

            Assert.True(result == null ? true : false);
        }

        [Fact]
        public void Add_NewEntity_True()
        {
            var mongoDbRepository = new MongoDbRepository<Tweet>(_connectionString, _databaseName, _collectionName);

            var tweet = new Tweet
            {
                Content = "This is a tweet"
            };

            mongoDbRepository.Add(tweet);

            var result = _database.GetCollection<Tweet>(_collectionName).Find(x => x.Id == tweet.Id).SingleOrDefault();

            Assert.True(result != null ? result.Id == tweet.Id : false);
        }

        [Fact]
        public void GetRecentlyAdded_NewEntites_True()
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

            _database.GetCollection<Tweet>(_collectionName).InsertMany(tweets);
            var all = _database.GetCollection<Tweet>(_collectionName).Find(FilterDefinition<Tweet>.Empty).ToList();

            var result = mongoDbRepository.Get();

            Assert.Equal<RootEntity>(all, result, new RootEntityComparer());
        }
    }
}
