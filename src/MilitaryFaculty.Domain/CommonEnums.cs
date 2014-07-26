using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum AcademicDegree
    {
        None,
        Docent,
        Professor
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum AcademicRank
    {
        None,
        CandidateOfScience,
        DoctorOfScience
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum ApplicationStatus
    {
        Applied,
        Accepted
    }
}
