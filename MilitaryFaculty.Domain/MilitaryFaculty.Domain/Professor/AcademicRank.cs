using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum AcademicRank : byte
    {
        [EnumName("Отсутствует")] None,

        [EnumName("Кандидат наук")] CandidateOfScience,

        [EnumName("Доктор наук")] DoctorOfScience
    }
}