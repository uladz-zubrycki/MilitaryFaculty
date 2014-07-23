using System.Collections.Generic;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    /// <summary>
    ///     University subdepartment.
    /// </summary>
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Cathedra : UniqueEntity
    {
        public const int NameMaxLength = 50;

        public virtual string Name { get; set; }
        public virtual ICollection<Professor> Professors { get; set; }

        public Cathedra()
        {
            Professors = new List<Professor>();
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}