using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Domain.Entities;

namespace TweetApp.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : RootEntity
    {
        Task<TEntity> Get(string id);
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);

        Task Remove(string id);
        Task RemoveRange(Expression<Func<TEntity, bool>> predicate);
    }
}
