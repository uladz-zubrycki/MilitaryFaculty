using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ScientificResearch
{
    public class ScientificResearch : UniqueEntity, IImitator<ScientificResearch>
    {
        private MilitaryScientificSupportState _state;

        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Professor Author { get; set; }
        public virtual int PagesCount { get; set; }

        public virtual MilitaryScientificSupportState State
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
}
