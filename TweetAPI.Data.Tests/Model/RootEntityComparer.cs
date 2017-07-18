using System;
using System.Collections.Generic;
using System.Text;
using TweetAPI.Domain.Entities;

namespace TweetAPI.Data.Tests.Model
{
    public class RootEntityComparer : IEqualityComparer<RootEntity>
    {
        public bool Equals(RootEntity x, RootEntity y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(RootEntity obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
