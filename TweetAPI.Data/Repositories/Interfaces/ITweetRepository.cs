using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TweetAPI.Domain.Entities;

namespace TweetAPI.Data.Repositories.Interfaces
{
    public interface ITweetRepository : IRepository<Tweet>
    {
        IEnumerable<Tweet> GetTweetsWithinCenterSphere(double x, double y, double radius);

        IEnumerable<Tweet> GetTweetsWithinPolygon(double[,] points);
    }
}
