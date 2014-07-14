using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum ScientificRequestResponce
    {
        [EnumName("Без ответа")] Unanswered,

        [EnumName("С положительным ответом")] PositiveResponse,

        [EnumName("С отрицательным ответом")] NegativeResponse
    }
}