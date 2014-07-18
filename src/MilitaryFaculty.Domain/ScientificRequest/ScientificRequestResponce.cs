using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ScientificRequest
{
    public enum ScientificRequestResponce
    {
        [EnumName("С положительным ответом")] Positive,

        [EnumName("С отрицательным ответом")] Negative
    }
}