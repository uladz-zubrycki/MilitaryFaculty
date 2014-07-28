using System;
using System.Linq;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Data
{
    public interface IRepository<T>
        where T : class, IUniqueEntity
    {
        IQueryable<T> Table { get; }
        void Create(T entity);
        T Read(Int32 id);
        void Update(T entity);
        void Delete(Int32 id);

        event EventHandler<ModifiedEntityEventArgs<T>> EntityCreated;
        event EventHandler<ModifiedEntityEventArgs<T>> EntityUpdated;
        event EventHandler<ModifiedEntityEventArgs<T>> EntityDeleted;
    }
}