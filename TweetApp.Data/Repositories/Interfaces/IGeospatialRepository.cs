using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TweetApp.Domain.Entities;

namespace TweetApp.Data.Repositories.Interfaces
{
    public interface IGeospatialRepository<TEntity> : IRepository<TEntity> where TEntity : GeospatialEntity
    {
        Task<IEnumerable<TEntity>> FindWithinCenterSphere(double x, double y, double radius);
        Task<IEnumerable<TEntity>> FindWithinCenterSphere(double x, double y, double radius, Expression<Func<TEntity,bool>> filter);
        Task<IEnumerable<TEntity>> FindWithinPolygon(double[,] points);
        Task<IEnumerable<TEntity>> FindWithinPolygon(double[,] points, Expression<Func<TEntity,bool>> filter);
    }
}
