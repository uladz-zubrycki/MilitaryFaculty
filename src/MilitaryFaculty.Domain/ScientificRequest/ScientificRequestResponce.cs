using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ScientificRequest
{
    public enum ScientificRequestResponce
    {
        [EnumName("С отрицательным ответом")] Negative,

        [EnumName("С положительным ответом")] Positive,
    }
}