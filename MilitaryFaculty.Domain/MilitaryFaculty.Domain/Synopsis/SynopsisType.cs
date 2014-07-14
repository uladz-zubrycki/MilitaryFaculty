using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum SynopsisType
    {
        [EnumName("Диссертация")] Dissertation,

        [EnumName("Экспертное заключение по диссертации")] OpinionOnTheDissertation,

        [EnumName("Отзыв на автореферат")] ReviewedOnSynopsis
    }
}