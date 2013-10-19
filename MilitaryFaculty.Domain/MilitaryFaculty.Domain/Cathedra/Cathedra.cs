using System;
using System.Collections;
using System.Collections.Generic;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Domain
{
    /// <summary>
    /// University subdepartment.
    /// </summary>
    public class Cathedra : UniqueEntity, IImitator<Cathedra>
    {
        #region Class Constants

        public const int NameMaxLength = 50;

        #endregion // Class Constants

        #region Class Fields

        #endregion // Class Fields

        #region Class Properties

        public virtual string Name { get; set; }
        public virtual ICollection<Professor> Professors { get; set; }

        #endregion //Class Properties

        #region Class Constructors

        public Cathedra()
        {
            Id = Guid.Empty;
            Name = String.Empty;
        }

        public Cathedra(string name)
            : this()
        {
            Name = name;
        }

        public Cathedra(Cathedra other)
            : this()
        {
            Imitate(other);
        }

        #endregion // Class Constructors

        #region Implementation of IImitator

        public void Imitate(Cathedra other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
        }

        #endregion // Implementation of IImitator
    }
}