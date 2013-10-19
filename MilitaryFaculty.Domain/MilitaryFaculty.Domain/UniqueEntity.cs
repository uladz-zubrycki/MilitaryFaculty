using System;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Domain
{
    public abstract class UniqueEntity : IUniqueEntity
    {
        #region Class Properties

        public Guid Id { get; set; }

        #endregion // Class Properties

        #region Class Public Methods

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal 
        /// to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            var entity = obj as UniqueEntity;

            return Equals(entity);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IUniqueEntity other)
        {
            return other != null && Id == other.Id;
        }

        #endregion // Class Public Methods
    }
}