﻿using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class ImprovementSuggestion : UniqueEntity, IImitator<ImprovementSuggestion>
    {
        private SuggestionState _suggestionState;

        public string Name { get; set; }
        public Professor Author { get; set; }
        public DateTime Date { get; set; }

        public SuggestionState SuggestionState
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
            Id = Guid.Empty;
            Name = String.Empty;
            Date = DateTime.Now;
            SuggestionState = SuggestionState.Accepted;
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