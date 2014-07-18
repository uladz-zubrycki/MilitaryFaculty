using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ImprovementSuggestion
{
    public enum SuggestionState
    {
        [EnumName("Не принята")] Denied,

        [EnumName("Принята")] Accepted,
    }
}
