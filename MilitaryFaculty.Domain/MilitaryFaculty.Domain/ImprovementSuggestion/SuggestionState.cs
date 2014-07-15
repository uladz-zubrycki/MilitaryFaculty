using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum SuggestionState
    {
        [EnumName("Принята")]
        Accepted,

        [EnumName("Не принята")]
        Denied
    }
}
