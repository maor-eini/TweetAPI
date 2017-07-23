using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Domain.Entities;

namespace TweetApp.Services.Services.Interfaces
{
    public interface ITweetService
    {
        Task<Tweet> Get(string id);
        Task<IEnumerable<Tweet>> GetRecentlyAddedTweets();
        Task<IEnumerable<Tweet>> GetTweetsWithinCenterSphere(double x, double y, double radius);
        Task<IEnumerable<Tweet>> GetTweetsWithinPolygon(double[,] points);
        Task Add(Tweet tweet);
        Task Remove(string id);

    }
}
