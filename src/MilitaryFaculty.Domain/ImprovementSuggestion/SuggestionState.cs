using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ImprovementSuggestion
{
    public enum SuggestionState
    {
        [EnumName("Принята")]
        Accepted,

        [EnumName("Не принята")]
        Denied
    }
}
