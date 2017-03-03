using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(params Expression<Func<TEntity, object>>[] includeExpressions);
        TEntity GetById(object id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void DeleteRange(IEnumerable<TEntity> entitiesToDelete);
        void Update(TEntity entityToUpdate);
    }
}