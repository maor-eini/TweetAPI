using System;
using System.Collections.Generic;
using System.Text;
using TweetApp.Domain.Entities;

namespace TweetApp.Data.Tests.Model
{
    public class RootEntityComparer : IEqualityComparer<RootEntity>
    {
        public bool Equals(RootEntity x, RootEntity y)
        {
            return x.Id == y.Id && x.GetType() == y.GetType();
        }

        public int GetHashCode(RootEntity obj)
        {
            return $"{obj.GetType()}_{obj.Id}".GetHashCode();
        }
    }
}
