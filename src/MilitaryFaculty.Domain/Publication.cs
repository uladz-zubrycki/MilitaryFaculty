using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum PublicationType
    {
        Monograph,
        ReviewedArticle,
        Article,
        Thesis
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Publication : UniqueEntity
    {
        public virtual string Name { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual Person Author { get; set; }
        public virtual PublicationType PublicationType { get; set; }
        public virtual int PagesCount { get; set; }

        public Publication()
        {
            CreatedAt = DateTime.Now;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}