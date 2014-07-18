using System;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class ScientificExpertise : UniqueEntity, IImitator<ScientificExpertise>
    {
        public virtual string Name { get; set; }
        public virtual Professor Author { get; set; }
        public virtual DateTime Date { get; set; }

        public ScientificExpertise()
        {
            Name = String.Empty;
            Date = DateTime.Now;
        }

        public ScientificExpertise(ScientificExpertise other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(ScientificExpertise other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
            Author = other.Author;
            Date = other.Date;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}
