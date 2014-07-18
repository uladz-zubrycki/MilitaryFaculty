using System;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum ParticipationPlace
    {
        [EnumName("Экспертный совет ВАК Беларуси")]
        HigherAttestationCommission,

        [EnumName("Специализированный совет по защите диссертаций")]
        DefenceOfDissertationCounsil,

        [EnumName("Военно-научный или научно-технический совет вуза")]
        ResearchCounsil,

        [EnumName("Редакционная коллегия научных изданий")]
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