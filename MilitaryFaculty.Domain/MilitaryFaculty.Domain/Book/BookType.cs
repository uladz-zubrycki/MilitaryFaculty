using MilitaryFaculty.Common.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum BookType
    {
        [EnumName("Учебник")]
        Schoolbook,

        [EnumName("Учебное пособие")]
        Tutorial
    }
}