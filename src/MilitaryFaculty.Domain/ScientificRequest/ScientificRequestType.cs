using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ScientificRequest
{
    public enum ScientificRequestType
    {
        [EnumName("Заявка на изобретение")] Invention,

        [EnumName("Заявка на полезную модель")] UtilityModel
    }
}