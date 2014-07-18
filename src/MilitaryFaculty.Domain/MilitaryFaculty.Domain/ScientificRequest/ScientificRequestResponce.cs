using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum ScientificRequestResponce
    {
        [EnumName("С положительным ответом")] Positive,

        [EnumName("С отрицательным ответом")] Negative
    }
}