using MilitaryFaculty.Common;
using MilitaryFaculty.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum EventLevel : byte
    {
        University,
        Republican,
        International,
    }
}