using MilitaryFaculty.Common.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum ConferenceType : byte
    {
        [EnumName("Вузовская")]
        University,

        [EnumName("Республиканская")]
        Republican,

        [EnumName("Международная")]
        International,
    }
}
