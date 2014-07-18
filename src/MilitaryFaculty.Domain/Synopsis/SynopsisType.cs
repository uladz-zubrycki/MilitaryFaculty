using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.Synopsis
{
    public enum SynopsisType
    {
        [EnumName("Диссертация")] Dissertation,

        [EnumName("Экспертное заключение по диссертации")] OpinionOnTheDissertation,

        [EnumName("Отзыв на автореферат")] ReviewedOnSynopsis
    }
}