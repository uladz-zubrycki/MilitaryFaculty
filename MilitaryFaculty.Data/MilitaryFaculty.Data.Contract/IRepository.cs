using System;
using System.Linq;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Data.Contract
{
    public interface IRepository<T> 
        where T : class, IUniqueEntity
    {
        void Create(T entity);
        T Read(Guid id);
        void Update(T entity);
        void Delete(Guid id);

        IQueryable<T> Table { get; }
        int CountOf(Func<T, bool> predicate);
        double SumOf(Func<T, decimal?> evaluator);

        event EventHandler<ModifiedEntityEventArgs<T>> EntityCreated;
        event EventHandler<ModifiedEntityEventArgs<T>> EntityUpdated;
        event EventHandler<ModifiedEntityEventArgs<T>> EntityDeleted;
    }
}
