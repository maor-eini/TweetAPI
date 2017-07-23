using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TweetApp.Domain.Entities;

namespace TweetApp.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : RootEntity
    {
        TEntity Get(string id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(string id);
        void RemoveRange(Expression<Func<TEntity, bool>> predicate);
    }
}
