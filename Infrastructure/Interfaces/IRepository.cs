using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Find();
    }

    public interface IRepository<TEntity, TKey> : IRepository<TEntity>
    {        
        void Add(params TEntity[] entity);
        TEntity Get(TKey id);
        void Update(TEntity entity);
        void Delete(params TEntity[] entity);
        void Delete(TKey id);
    }
}