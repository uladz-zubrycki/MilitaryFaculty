using System;
using System.Data.Entity;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Data
{
    public class Repository<T> : IRepository<T>
        where T : class, IUniqueEntity
    {
        #region Class Fields

        private readonly DbContext dbContext;

        #endregion // Class Fields

        #region Class Properties

        protected DbSet<T> DbSet
        {
            get { return dbContext.Set<T>(); }
        }

        public IQueryable<T> Table
        {
            get { return DbSet.AsQueryable(); }
        }

        #endregion // Class Properties

        #region Class Events

        public event EventHandler<ModifiedEntityEventArgs<T>> EntityCreated;
        public event EventHandler<ModifiedEntityEventArgs<T>> EntityUpdated;
        public event EventHandler<ModifiedEntityEventArgs<T>> EntityDeleted;

        #endregion // Class Events

        #region Class Constructors

        public Repository(EntityContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            dbContext = context;
        }

        #endregion // Class Constructors

        #region Public Class Methods

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("item");
            }

            DbSet.Add(entity);
            dbContext.SaveChanges();

            OnEntityCreated(entity);
        }

        public T Read(Guid id)
        {
            return DbSet.Single(x => x.Id == id);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // changes are auto-tracking by context, 
            // so just check if entity exist in database
            Read(entity.Id);
            dbContext.SaveChanges();

            OnEntityUpdated(entity);
        }

        public void Delete(Guid id)
        {
            var entity = Read(id);
            OnEntityDeleted(entity);

            DbSet.Remove(entity);
            dbContext.SaveChanges();
        }

        public int CountOf(Func<T, bool> predicate)
        {
            return Table.Count(predicate);
        }

        public double SumOf(Func<T, double> evaluator)
        {
            return Table.Sum(evaluator);
        }

        #endregion // Public Class Methods

        #region Class Protected Methods

        protected void OnEntityCreated(T entity)
        {
            var handler = EntityCreated;

            if (handler != null)
            {
                handler(null, new ModifiedEntityEventArgs<T>(entity));
            }
        }

        protected void OnEntityUpdated(T entity)
        {
            var handler = EntityUpdated;

            if (handler != null)
            {
                handler(null, new ModifiedEntityEventArgs<T>(entity));
            }
        }

        protected void OnEntityDeleted(T entity)
        {
            var handler = EntityDeleted;

            if (handler != null)
            {
                handler(null, new ModifiedEntityEventArgs<T>(entity));
            }
        }

        #endregion // Class Protected Methods
    }
}