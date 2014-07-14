using System;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Domain
{
    public class ImprovementSuggestion : UniqueEntity, IImitator<ImprovementSuggestion>
    {
        public string Name { get; set; }
        public Professor Author { get; set; }
        public DateTime Date { get; set; }

        public ImprovementSuggestion()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            Date = DateTime.Now;
        }

        public ImprovementSuggestion(ImprovementSuggestion other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(ImprovementSuggestion other)
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