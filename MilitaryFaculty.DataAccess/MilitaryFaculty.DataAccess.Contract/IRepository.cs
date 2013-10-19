using System;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.DataAccess.Contract
{
    public interface IRepository<T> 
        where T : class, IUniqueEntity
    {
        void Create(T entity);
        T Read(Guid id);
        void Update(T entity);
        void Delete(Guid id);

        event EventHandler<ModifiedEntityEventArgs<T>> EntityCreated;
        event EventHandler<ModifiedEntityEventArgs<T>> EntityUpdated;
        event EventHandler<ModifiedEntityEventArgs<T>> EntityDeleted;
    }
}
