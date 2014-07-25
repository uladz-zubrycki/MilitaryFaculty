using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum ApplicationStatus
    {
        Applied,
        Accepted
    }
}