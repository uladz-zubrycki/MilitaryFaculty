using System;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum DissertationWorkDegree
    {
        [EnumName("Докторская")]
        Doctor,

        [EnumName("Кандидатская")]
        Candidate
    }

    public enum DissertationWorkType
    {
        [EnumName("Диссертация")]
        Dissertation,

        [EnumName("Экспертное заключение по диссертации")]
        OpinionOnTheDissertation,

        [EnumName("Отзыв на автореферат")]
        ReviewedOnSynopsis
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class DissertationWork : UniqueEntity, IImitator<DissertationWork>
    {
        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual DissertationWorkType SynopsisType { get; set; }
        public virtual DissertationWorkDegree SynopsisDegree { get; set; }

        public DissertationWork()
        {
            Name = String.Empty;
            Date = DateTime.Now;
        }

        public DissertationWork(DissertationWork other)
        {
            Imitate(other);
        }

        public void Imitate(DissertationWork other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Date = other.Date;
            Name = other.Name;
            Author = other.Author;
            SynopsisType = other.SynopsisType;
            SynopsisDegree = other.SynopsisDegree;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}