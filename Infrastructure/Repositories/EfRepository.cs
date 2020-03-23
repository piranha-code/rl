using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
    {
        private readonly NorthwindContext _dbContext;
        internal readonly DbSet<TEntity> _dbSet;

        public EfRepository(NorthwindContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<TEntity>() ?? throw new ArgumentNullException(nameof(_dbSet));
        }

        public virtual TEntity Get(TKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> Find()
        {
            return _dbSet.AsNoTracking().AsEnumerable();
        }

        public virtual void Add(params TEntity[] entity)
        {
            _dbSet.AddRange(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(params TEntity[] entity)
        {
            AttachDetached(entity);
            _dbSet.RemoveRange(entity);
        }

        public virtual void Delete(TKey id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        protected virtual void AttachDetached(params TEntity[] entity)
        {
            Array.ForEach<TEntity>(entity, e =>
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                    _dbContext.Attach(entity);
            });
        }
    }
}