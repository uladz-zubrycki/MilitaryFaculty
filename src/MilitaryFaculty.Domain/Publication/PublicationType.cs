using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum PublicationType
    {
        [EnumName("Монография")] Monograph,

        [EnumName("Рецензируемая научная статья")] ReviewedArticle,

        [EnumName("Нерецензируемая научная статья")] Article,

        [EnumName("Тезис докладов")] Thesis
    }
}