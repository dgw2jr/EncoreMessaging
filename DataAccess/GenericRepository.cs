using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain.Interfaces;

namespace DataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DomainModel _context;
        private readonly DbSet<TEntity> DbSet;

        public GenericRepository(DomainModel context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.AsQueryable();
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> result = DbSet.Where(predicate);
            return result;
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var result = DbSet.FirstOrDefault(predicate);
            return result;
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate) => DbSet.Single(predicate);

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate) => DbSet.SingleOrDefault(predicate);

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                DbSet.Attach(entityToDelete);
            DbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            DbSet.RemoveRange(entitiesToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IQueryable<TEntity> Get(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return includeExpressions.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (DbSet, (current, expression) => current.Include(expression));
        }
    }
}