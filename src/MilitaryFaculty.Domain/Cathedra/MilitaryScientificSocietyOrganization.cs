using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum MilitaryScientificSocietyOrganization
    {
        [EnumName("Работа научного кружка курсантов (стедентов) не организована")] None,

        [EnumName("Работа научного кружка курсантов (стедентов) организована, но ведется с отдельными недостатками")] Custom,


        [EnumName(
            "Работа научного кружка курсантов (студентов) организована и ведется в соответствии с требованиями нормативных правовых актов"
            )] Full,
    }
}