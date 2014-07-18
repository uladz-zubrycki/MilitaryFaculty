using System;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum SuggestionState
    {
        [EnumName("Не принята")]
        Denied,

        [EnumName("Принята")]
        Accepted,
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class ImprovementSuggestion : UniqueEntity, IImitator<ImprovementSuggestion>
    {
        public virtual string Name { get; set; }
        public virtual Professor Author { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual SuggestionState SuggestionState { get; set; }

        public ImprovementSuggestion()
        {
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
            SuggestionState = other.SuggestionState;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}