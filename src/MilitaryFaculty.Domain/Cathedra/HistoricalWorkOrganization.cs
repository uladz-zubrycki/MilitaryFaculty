using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum HistoricalWorkOrganization
    {
        [EnumName("Военно-историческая работа не органозована")] None,

        [EnumName("Военно-историческая работа органозована, но ведется с отдельными недостатками")] Custom,

        [EnumName(
            "Военно-историческая работа организована и ведется в соответствии с требованиями нормативных правовых актов"
            )] Full,
    }
}