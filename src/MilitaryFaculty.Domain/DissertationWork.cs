using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum DissertationWorkDegree
    {
        Doctor,
        Candidate
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum DissertationWorkType
    {
        Dissertation,
        OpinionOnTheDissertation,
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