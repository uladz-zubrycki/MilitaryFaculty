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

        public Cathedra()
        {
            Professors = new List<Person>();
            ScienceRanks = new List<ScienceRank>();
        }

        public virtual string Name { get; set; }
        public virtual ICollection<Person> Professors { get; set; }
        public virtual ICollection<ScienceRank> ScienceRanks { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}