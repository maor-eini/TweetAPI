using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void Add(Tweet tweet)
        {
            _repository.Add(tweet);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for the time specified and reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromSeconds(3));

            _cache.Set($"{typeof(Tweet)}_{tweet.Id}", tweet, cacheEntryOptions);
        }

        public IEnumerable<Tweet> GetTweetsWithinCenterSphere(double x, double y, double radius)
        {
            IEnumerable<Tweet> result = null; 

            int cacheEntry = $"{x}_{y}_{radius}".GetHashCode();
            if (_cache.TryGetValue(cacheEntry, out IEnumerable<string> idList))
            {
                result = _repository.Find(t => idList.Contains(t.Id));
            }
            else
            {
                result = _repository.FindWithinCenterSphere(x, y, radius);
                if (result != null && result.Count() > 0)
                {
                    _cache.Set(cacheEntry, result.Select(t => t.Id));
                }
            }
            return result;  
        }

        public IEnumerable<Tweet> GetTweetsWithinPolygon(double[,] points)
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
                result = _repository.Find(t => idList.Contains(t.Id));
            }
            else
            {
                result = _repository.FindWithinPolygon(points);
                if (result != null && result.Count() > 0)
                {
                    _cache.Set(cacheEntry, result.Select(t => t.Id));
                }
            }
            return result;
        }

        public Tweet Get(string id)
        {
            if (_cache.TryGetValue(id, out Tweet tweet))
            {
                return tweet;
            }
            return _repository.Get(id);
        }

        public IEnumerable<Tweet> GetRecentlyAddedTweets()
        {
            return _repository.Get();
        }

        public void Remove(string id)
        {
            _repository.Remove(id);
        }
    }
}
