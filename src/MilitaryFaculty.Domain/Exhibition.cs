using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum ExhibitionAward
    {
        Certificate,
        FirstDegree,
        SecondDegree,
        ThirdDegree,
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Exhibition : UniqueEntity
    {
        public static int NameMaxLength = 50;

        public Exhibition()
        {
            Date = DateTime.Now;
        }

        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Professor Participant { get; set; }
        public virtual ExhibitionAward Award { get; set; }
        public virtual EventLevel EventLevel { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}