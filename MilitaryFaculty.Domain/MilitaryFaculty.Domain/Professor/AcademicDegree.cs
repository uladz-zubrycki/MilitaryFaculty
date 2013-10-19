using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum AcademicDegree : byte
    {
        [EnumName("Отсутствует")]
        None,
 
        [EnumName("Доцент")]
        Docent,
        
        [EnumName("Профессор")]
        Professor
    }
}