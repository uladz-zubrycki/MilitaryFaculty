using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.DataAccess.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.DataAccess.Stubs
{
    public class StubRepository<T> : IRepository<T>
        where T: class, IUniqueEntity
    {
        #region Class Fields
        private readonly IList<T> items;
        #endregion // Class Fields

        #region Class Events
        public event EventHandler<ModifiedEntityEventArgs<T>> EntityCreated;
        public event EventHandler<ModifiedEntityEventArgs<T>> EntityUpdated;
        public event EventHandler<ModifiedEntityEventArgs<T>> EntityDeleted;
        #endregion // Class Events

        #region Class Constructors
        public StubRepository(params T[] items)
        {
            
            if (items == null)
            {
                throw new ArgumentNullException("cathedras");
            }
            

            this.items = items.ToList();
        }
        #endregion // Class Constructors

        #region Implementation of IRepository<Cathedra>
        public void Create(T entity)
        {
            
            if (entity == null)
            {
                throw new ArgumentNullException("cathedra");
            }
            if (items.Contains(entity))
            {
                throw new ArgumentException();
            }
            

            items.Add(entity);
        }

        public T Read(Guid id)
        {
            return items.First(item => item.Id == id);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Cathedra cathedra)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> AsQueryable()
        {
            return items.AsQueryable();
        }
        #endregion // Implementation of IRepository<Cathedra>
    }
}