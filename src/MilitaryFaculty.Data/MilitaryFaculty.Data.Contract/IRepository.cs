using System;
using System.Linq;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Data.Contract
{
	public interface IRepository<T>
		where T : class, IUniqueEntity
	{
		IQueryable<T> Table { get; }
		void Create(T entity);
		T Read(Guid id);
		void Update(T entity);
		void Delete(Guid id);

		event EventHandler<ModifiedEntityEventArgs<T>> EntityCreated;
		event EventHandler<ModifiedEntityEventArgs<T>> EntityUpdated;
		event EventHandler<ModifiedEntityEventArgs<T>> EntityDeleted;
	}
}