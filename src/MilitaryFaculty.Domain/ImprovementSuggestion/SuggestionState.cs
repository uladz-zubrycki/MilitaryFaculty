using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum SuggestionState
    {
        [EnumName("Не принята")] Denied,

        [EnumName("Принята")] Accepted,
    }
}
