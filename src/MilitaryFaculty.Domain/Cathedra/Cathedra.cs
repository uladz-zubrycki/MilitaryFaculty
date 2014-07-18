using System;
using System.Collections.Generic;

namespace MilitaryFaculty.Domain
{
    /// <summary>
    ///     University subdepartment.
    /// </summary>
    public class Cathedra : UniqueEntity, IImitator<Cathedra>
    {
        public const int NameMaxLength = 50;

        public virtual string Name { get; set; }
        public virtual ICollection<Professor> Professors { get; set; }

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

        public void Imitate(Cathedra other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
        }
    }
}