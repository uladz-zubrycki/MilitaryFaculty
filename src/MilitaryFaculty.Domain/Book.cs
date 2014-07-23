using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum BookType
    {
        Schoolbook,
        Tutorial
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Book : UniqueEntity
    {
        public virtual string Name { get; set; }
        public virtual Professor Author { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual BookType BookType { get; set; }
        public virtual int PagesCount { get; set; }

        public Book()
        {
            CreatedAt = DateTime.Now;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}