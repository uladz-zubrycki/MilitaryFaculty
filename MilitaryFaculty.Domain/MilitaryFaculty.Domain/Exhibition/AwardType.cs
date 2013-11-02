using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum AwardType
    {
        [EnumName("Диплом первой степени")]
        FirstDegree,

        [EnumName("Диплом второй степени (медаль)")]
        SecondDegree,
        
        [EnumName("Диплом третьей степени")]
        ThirdDegree,
        
        [EnumName("Диплом (грамота)")]
        NoneDegree
    }
}
