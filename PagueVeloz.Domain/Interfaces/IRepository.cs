using PagueVeloz.Domain.Entities;
using System.Linq;

namespace PagueVeloz.Domain.Interfaces
{
    public interface IRepository
    {
        long SaveOrUpdate<TEntity>(TEntity entity) where TEntity : EntityBase;

        void Delete<TEntity>(TEntity entity) where TEntity : EntityBase;

        void Delete<TEntity>(long id) where TEntity : EntityBase;

        IQueryable<TEntity> Query<TEntity>() where TEntity : EntityBase;

        TEntity GetById<TEntity>(long id) where TEntity : EntityBase;

        IQueryable<TEntity> QueryAsNoTracking<TEntity>() where TEntity : EntityBase;
    }
}
