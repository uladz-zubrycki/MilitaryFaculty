using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum InventiveApplicationType
    {
        Invention,
        UtilityModel
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum InventiveApplicationStatus
    {
        Applied,
        Accepted
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class InventiveApplication : UniqueEntity
    {
        public virtual string Name { get; set; }
        public virtual Professor Author { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual InventiveApplicationType Type { get; set; }
        public virtual InventiveApplicationStatus Status { get; set; }

        public InventiveApplication()
        {
            CreatedAt = DateTime.Now;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}