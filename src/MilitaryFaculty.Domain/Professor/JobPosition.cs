using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum JobPosition : byte
    {
        [EnumName("Курсант/Студент")] Student,

        [EnumName("Аспирант")] Aspirant,

        [EnumName("Докторант")] Doctorant,

        [EnumName("Преподаватель")] Teacher,

        [EnumName("Старший преподаватель")] SeniorProfessor,

        [EnumName("Доцент")] Docent,

        [EnumName("Профессор")] Professor,

        [EnumName("Начальник цикла")] HeadOfCycle,

        [EnumName("Начальник кафедры")] HeadOfCathedra,

        [EnumName("Начальник факультета")] HeadOfFaculty,
    }
}