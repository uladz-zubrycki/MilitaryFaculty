using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ScientificResearch
{
    public class ScientificResearch : UniqueEntity, IImitator<ScientificResearch>
    {
        private MilitaryScientificSupportState _state;

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Professor Author { get; set; }
        public int PagesCount { get; set; }

        public MilitaryScientificSupportState State
        {
            get { return _state; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _state = value;
            }
        }

        public ScientificResearch()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            Date = DateTime.Now;
            PagesCount = 0;
            _state = MilitaryScientificSupportState.NotCompleted;
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
}
