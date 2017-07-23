using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TweetApp.Domain.Entities;

namespace TweetApp.Data.Repositories.Interfaces
{
    public interface IGeospatialRepository<TEntity> : IRepository<TEntity> where TEntity : GeospatialEntity
    {
        IEnumerable<TEntity> FindWithinCenterSphere(double x, double y, double radius);
        IEnumerable<TEntity> FindWithinCenterSphere(double x, double y, double radius, Expression<Func<TEntity,bool>> filter);
        IEnumerable<TEntity> FindWithinPolygon(double[,] points);
        IEnumerable<TEntity> FindWithinPolygon(double[,] points, Expression<Func<TEntity,bool>> filter);
    }
}
