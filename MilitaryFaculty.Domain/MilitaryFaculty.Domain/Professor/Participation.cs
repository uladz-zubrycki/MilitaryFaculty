using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Participation : UniqueEntity, IImitator<Participation>
    {
        private ParticipationPlace _placeType;

        public Professor Participant { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ParticipationPlace PlaceType
        {
            get { return _placeType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _placeType = value;
            }
        }

        public Participation()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            StartDate = DateTime.Now;
            EndDate = DateTime.MaxValue;
            _placeType = ParticipationPlace.HigherAttestationCommission;
        }

        public Participation(Participation other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(Participation other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
            StartDate = other.StartDate;
            EndDate = other.EndDate;
            PlaceType = other.PlaceType;
        }
    }
}