using System.Collections.Generic;

namespace Bookstore.Models.Repos
{
    public interface IBookstoreRepository<TEntity>
    {
        IList<TEntity> List();

        TEntity GetEntity(int id);

        void Add(TEntity entity);

        void delete(int id);

        void Update(int id,TEntity entity);
    }
}