using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Data.Repositories.Interfaces;
using TweetApp.Domain.Entities;
using TweetApp.Services.Services.Interfaces;

namespace TweetApp.Services.Services
{
    public class TweetService : ITweetService
    {
        private IGeospatialRepository<Tweet> _repository;
        private IMemoryCache _cache;

        public TweetService(IMemoryCache cache, IGeospatialRepository<Tweet> repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task Add(Tweet tweet)
        {
            await _repository.Add(tweet);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for the time specified and reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromSeconds(3));

            _cache.Set($"{typeof(Tweet)}_{tweet.Id}", tweet, cacheEntryOptions);
        }

        public async Task<IEnumerable<Tweet>> GetTweetsWithinCenterSphere(double x, double y, double radius)
        {
            IEnumerable<Tweet> result = null; 

            int cacheEntry = $"{x}_{y}_{radius}".GetHashCode();
            if (_cache.TryGetValue(cacheEntry, out IEnumerable<string> idList))
            {
                result = await _repository.Find(t => idList.Contains(t.Id));
            }
            else
            {
                result = await _repository.FindWithinCenterSphere(x, y, radius);
                if (result != null && result.Count() > 0)
                {
                    _cache.Set(cacheEntry, result.Select(t => t.Id));
                }
            }
            return result;  
        }

        public async Task<IEnumerable<Tweet>> GetTweetsWithinPolygon(double[,] points)
        {
            IEnumerable<Tweet> result = null;

            StringBuilder sb = new StringBuilder();
            foreach (double point in points)
            {
                if (sb.Length > 0)
                {
                    sb.Append("_");
                }
                sb.Append($"{point}");
            }

            int cacheEntry = sb.ToString().GetHashCode();

            if (_cache.TryGetValue(cacheEntry, out IEnumerable<string> idList))
            {
                result = await _repository.Find(t => idList.Contains(t.Id));
            }
            else
            {
                result = await _repository.FindWithinPolygon(points);
                if (result != null && result.Count() > 0)
                {
                    _cache.Set(cacheEntry, result.Select(t => t.Id));
                }
            }
            return result;
        }

        public async Task<Tweet> Get(string id)
        {
            if (_cache.TryGetValue(id, out Tweet tweet))
            {
                return tweet;
            }
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Tweet>> GetRecentlyAddedTweets()
        {
            return await _repository.Get();
        }

        public async Task Remove(string id)
        {
            await _repository.Remove(id);
        }
    }
}
