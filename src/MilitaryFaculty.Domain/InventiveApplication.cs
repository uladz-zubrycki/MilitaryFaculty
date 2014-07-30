using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum InventiveApplicationType
    {
        Invention,
        UtilityModel
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class InventiveApplication : UniqueEntity
    {
        public InventiveApplication()
        {
            CreatedAt = DateTime.Now;
        }

        public virtual string Name { get; set; }
        public virtual Person Author { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual InventiveApplicationType Type { get; set; }
        public virtual ApplicationStatus Status { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}