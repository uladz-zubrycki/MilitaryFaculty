using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ImprovementSuggestion
{
    public class ImprovementSuggestion : UniqueEntity, IImitator<ImprovementSuggestion>
    {
        private SuggestionState _suggestionState;

        public virtual string Name { get; set; }
        public virtual Professor Author { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual SuggestionState SuggestionState
        {
            get { return _suggestionState; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _suggestionState = value;
            }
        }

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
            _suggestionState = other.SuggestionState;
        }
    }
}