using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Synopsis : UniqueEntity, IImitator<Synopsis>
    {
        private SynopsisType _synopsisType;
        private SynopsisDegree _synopsisDegree;

        public Professor Author { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public SynopsisType SynopsisType
        {
            get { return _synopsisType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _synopsisType = value;
            }
        }

        public SynopsisDegree SynopsisDegree
        {
            get { return _synopsisDegree; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _synopsisDegree = value;
            }
        }

        public Synopsis()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            Date = DateTime.Now;
            SynopsisType = SynopsisType.Dissertation;
            SynopsisDegree = SynopsisDegree.Candidate;
        }

        public Synopsis(Synopsis other)
        {
            Imitate(other);
        }

        public void Imitate(Synopsis other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Date = other.Date;
            Name = other.Name;
            Author = other.Author;
            _synopsisType = other.SynopsisType;
            _synopsisDegree = other.SynopsisDegree;
        }
    }
}