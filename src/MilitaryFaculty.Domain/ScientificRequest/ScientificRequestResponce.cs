using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum ScientificRequestResponce
    {
        [EnumName("С отрицательным ответом")] Negative,

        [EnumName("С положительным ответом")] Positive,
    }
}