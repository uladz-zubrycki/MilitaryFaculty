using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    /// <summary>
    ///     Evaluates some characteristic accordance to standart.
    /// </summary>
    public enum AccordanceLevel : byte
    {
        [EnumName("Не соответствует")] None = 0,

        [EnumName("Частично соответствует")] Partly = 1,

        [EnumName("Полностью соответствует")] Fully = 2,
    }
}