using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    public enum PublicationType
    {
        [EnumName("Монография")]
        Monograph,

        [EnumName("Рецензируемая научная статья")]
        ReviewedArticle,

        [EnumName("Нерецензируемая научная статья")]
        Article,

        [EnumName("Тезис докладов")]
        Thesis
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Publication : UniqueEntity, IImitator<Publication>
    {
        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual int PagesCount { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual PublicationType PublicationType { get; set; }

        public Publication()
        {
            Name = String.Empty;
            Date = DateTime.Now;
            PagesCount = 0;
        }

        public Publication(Publication other)
        {
            Imitate(other);
        }

        public void Imitate(Publication other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Date = other.Date;
            Name = other.Name;
            PagesCount = other.PagesCount;
            Author = other.Author;
            PublicationType = other.PublicationType;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}