using MilitaryFaculty.Common.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum JobPosition : byte
    {
        [EnumName("Преподаватель")]
        Teacher,

        [EnumName("Старший преподаватель")]
        SeniorProfessor,

        [EnumName("Доцент")]
        Docent,

        [EnumName("Профессор")]
        Professor,
        
        [EnumName("Начальник цикла")]
        HeadOfCycle,
        
        [EnumName("Начальник кафедры")]
        HeadOfCathedra
    }
}
