using System;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Domain
{
    public class ScientificExpertise : UniqueEntity, IImitator<ScientificExpertise>
    {
        public string Name { get; set; }
        public Professor Author { get; set; }
        public DateTime Date { get; set; }

        public ScientificExpertise()
        {
            Id = Guid.Empty;
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
