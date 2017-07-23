using System.Collections.Generic;
using TweetApp.Domain.Entities;

namespace TweetApp.Services.Services.Interfaces
{
    public interface ITweetService
    {
        Tweet Get(string id);
        IEnumerable<Tweet> GetRecentlyAddedTweets();
        IEnumerable<Tweet> GetTweetsWithinCenterSphere(double x, double y, double radius);
        IEnumerable<Tweet> GetTweetsWithinPolygon(double[,] points);
        void Add(Tweet tweet);
        void Remove(string id);

    }
}
