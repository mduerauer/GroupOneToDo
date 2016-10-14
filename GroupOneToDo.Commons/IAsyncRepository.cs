﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupOneToDo.Commons
{
    public interface IAsyncRepository<TEntity, in TId> where TEntity : IAggregateRoot<TId>
    {
        Task<TEntity> GetById(TId id);

        Task<ICollection<TEntity>> FindAll();

        Task<TEntity> Save(TEntity entity);

        Task<TEntity> DeleteById(TId id);

    }
}
