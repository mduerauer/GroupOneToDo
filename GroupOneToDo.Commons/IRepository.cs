using System.Collections.Generic;

namespace GroupOneToDo.Commons
{
    public interface IRepository<TEntity, in TId> where TEntity : IAggregateRoot<TId>
    {
        TEntity GetById(TId id);

        ICollection<TEntity> FindAll();

        void Save(TEntity entity);

        TEntity DeleteById(TId id);

    }
}
