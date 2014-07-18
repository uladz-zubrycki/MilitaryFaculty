using System;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data.Events
{
    public class ModifiedEntityEventArgs<T> : EventArgs
        where T : class, IUniqueEntity
    {
        public T ModifiedEntity { get; protected set; }

        public ModifiedEntityEventArgs(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            ModifiedEntity = entity;
        }
    }
}