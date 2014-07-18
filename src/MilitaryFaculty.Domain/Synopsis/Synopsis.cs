using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.Synopsis
{
    public class Synopsis : UniqueEntity, IImitator<Synopsis>
    {
        private SynopsisType _synopsisType;
        private SynopsisDegree _synopsisDegree;

        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual SynopsisType SynopsisType
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

        public virtual SynopsisDegree SynopsisDegree
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
            Name = String.Empty;
            Date = DateTime.Now;
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