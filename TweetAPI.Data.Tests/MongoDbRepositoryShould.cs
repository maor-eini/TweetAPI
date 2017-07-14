using System;
using TweetAPI.Data.Repositories;
using TweetAPI.Domain.Entities;
using Xunit;

namespace TweetAPI.Data.Tests
{
    public class MongoDbRepositoryShould
    {
        [Fact]
        public void Get_ReturnEntity_True()
        {
            var mongoDbRepository = new MongoDbRepository<Tweet>("mongodb://localhost:27017","Tweets", "Tweet");

            var tweet = new Tweet
            {
                Content = "This is a tweet"
            };

            mongoDbRepository.Add(tweet);

            Assert.True(mongoDbRepository.Get(tweet.Id).Id == tweet.Id);
        }
    }
}
