using System;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    public enum InventiveApplicationType
    {
        Invention,
        UtilityModel
    }

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
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}