using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    public enum MilitaryScientificSupportState
    {
        [EnumName("Военно-научное сопровождение не проведено")]
        NotCompleted,

        [EnumName("Военно-научное сопровождение проведено")]
        Completed,
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class ScientificResearch : UniqueEntity, IImitator<ScientificResearch>
    {
        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Professor Author { get; set; }
        public virtual int PagesCount { get; set; }
        public virtual MilitaryScientificSupportState State { get; set; }

        public ScientificResearch()
        {
            Name = String.Empty;
            Date = DateTime.Now;
        }

        public ScientificResearch(ScientificResearch other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(ScientificResearch other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
            PagesCount = other.PagesCount;
            Author = other.Author;
            Date = other.Date;
            State = other.State;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}
