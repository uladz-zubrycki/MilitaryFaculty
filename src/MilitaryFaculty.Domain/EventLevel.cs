using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum EventLevel : byte
    {
        [EnumName("Вузовская")] University,
        [EnumName("Республиканская")] Republican,
        [EnumName("Международная")] International,
    }
}