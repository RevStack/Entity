using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using RevStack.Pattern;
using System.Linq.Expressions;

namespace RevStack.Entity
{
    public class EntityRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private DbContext _context;
        public EntityRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> Get()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public TEntity Add(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State= EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

    }
}
