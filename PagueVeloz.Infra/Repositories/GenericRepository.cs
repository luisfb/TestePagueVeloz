using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PagueVeloz.Domain;
using PagueVeloz.Domain.Entities;
using PagueVeloz.Domain.Interfaces;

namespace PagueVeloz.Infra.Repositories
{
    public class GenericRepository : IRepository
    {
        private readonly DbContext _context;
        
        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public long SaveOrUpdate<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (!entity.Validate())
            {
                //_context.Database.RollbackTransaction(); //sem suporte a transações no in-memory database
                throw new EntityValidationException(entity.ValidationErrors);
            }

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                if (entity.Id == 0)
                    _context.Set<TEntity>().Add(entity);
                else
                {
                    _context.Set<TEntity>().Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
            _context.SaveChanges();
            return entity.Id;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Set<TEntity>().Attach(entity);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete<TEntity>(long id) where TEntity : EntityBase
        {
            var entity = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : EntityBase
        {
            return _context.Set<TEntity>();
        }

        public TEntity GetById<TEntity>(long id) where TEntity : EntityBase
        {
            return _context.Find<TEntity>(id);
        }

        public IQueryable<TEntity> QueryAsNoTracking<TEntity>() where TEntity : EntityBase
        {
            return _context.Set<TEntity>().AsNoTracking();
        }
    }
}
