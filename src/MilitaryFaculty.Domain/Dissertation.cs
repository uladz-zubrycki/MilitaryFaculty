using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum DissertationState
    {
        Defenced,
        Development
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Dissertation : UniqueEntity
    {
        public Dissertation()
        {
            CreatedAt = DateTime.Now;
        }

        public virtual string Name { get; set; }
        public virtual Person Author { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual AcademicRank TargetAcademicRank { get; set; }
        public virtual DissertationState State { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}