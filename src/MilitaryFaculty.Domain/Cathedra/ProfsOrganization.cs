using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum ProfsOrganization
    {
        [EnumName("Подготовка научно-педагогических работников высшей квалификации не организована")] None,


        [EnumName(
            "Подготовка научно-педагогических работников высшей квалификации организована, но ведется с отдельными недостатками"
            )] Custom,

        [EnumName("Подготовка научно-педагогических работников высшей квалификации организована")] Full,
    }
}