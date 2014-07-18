using System;

namespace MilitaryFaculty.Domain
{
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
}
