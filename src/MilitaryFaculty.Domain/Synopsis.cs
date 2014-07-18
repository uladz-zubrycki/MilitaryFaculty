using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum SynopsisDegree
    {
        [EnumName("Докторская")]
        Doctor,

        [EnumName("Кандидатская")]
        Candidate
    }

    public enum SynopsisType
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
    public class Synopsis : UniqueEntity, IImitator<Synopsis>
    {
        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual SynopsisType SynopsisType { get; set; }
        public virtual SynopsisDegree SynopsisDegree { get; set; }

        public Synopsis()
        {
            Name = String.Empty;
            Date = DateTime.Now;
        }

        public Synopsis(Synopsis other)
        {
            Imitate(other);
        }

        public void Imitate(Synopsis other)
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