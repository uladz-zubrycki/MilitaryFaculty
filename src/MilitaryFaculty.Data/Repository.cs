using System;
using System.Data.Entity;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class Repository<T> : IRepository<T>
        where T : class, IUniqueEntity
    {
        private readonly DbContext _dbContext;

        protected DbSet<T> DbSet
        {
            get { return _dbContext.Set<T>(); }
        }

        public IQueryable<T> Table
        {
            get { return DbSet; }
        }

        public event EventHandler<ModifiedEntityEventArgs<T>> EntityCreated;
        public event EventHandler<ModifiedEntityEventArgs<T>> EntityUpdated;
        public event EventHandler<ModifiedEntityEventArgs<T>> EntityDeleted;

        public Repository(EntityContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _dbContext = context;
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbSet.Add(entity);
            _dbContext.SaveChanges();

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
            _dbContext.SaveChanges();

            OnEntityUpdated(entity);
        }

        public void Delete(Guid id)
        {
            var entity = Read(id);
            OnEntityDeleted(entity);

            DbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

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
    }
}