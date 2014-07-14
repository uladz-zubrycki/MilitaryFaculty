using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum ParticipationPlace
    {
        [EnumName("Экспертный совет ВАК Беларуси")] HigherAttestationCommission,

        [EnumName("Специализированный совет по защите диссертиций")] DefenceOfDissertationCounsil,

        [EnumName("Военно-научный или научно-технический совет вуза")] ResearchCounsil,

        [EnumName("Редакционная коллегия научных изданий")] EditorialBoardsOfScientificPublications
    }
}