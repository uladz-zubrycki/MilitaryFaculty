using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum ParticipationPlace
    {
        HigherAttestationCommission,
        DefenceOfDissertationCounsil,
        ResearchCounsil,
        EditorialBoardsOfScientificPublications
    }

    public class Participation : UniqueEntity, IImitator<Participation>
    {
        public Professor Participant { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ParticipationPlace PlaceType { get; set; }

        public Participation()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            StartDate = DateTime.Now;
            EndDate = DateTime.MaxValue;
            PlaceType = ParticipationPlace.HigherAttestationCommission;
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