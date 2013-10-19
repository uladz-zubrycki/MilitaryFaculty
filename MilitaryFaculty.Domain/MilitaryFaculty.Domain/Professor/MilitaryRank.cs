using MilitaryFaculty.Common.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum MilitaryRank : short
    {
        [EnumName("Младший лейтенант")]
        JuniorLieutenant,        

        [EnumName("Лейтенант")]
        Lieutenant,              

        [EnumName("Старший лейтенант")]
        SeniorLieutenant,        

        [EnumName("Капитан")]
        Captain,                  

        [EnumName("Майор")]
        Major,                   

        [EnumName("Подполковник")]
        LieutenantColonel,       

        [EnumName("Полковник ")]
        Colonel,                 

        //[EnumName("Генерал-майор")]
        //MajorGeneral,            

        //[EnumName("Генерал-лейтенант")]
        //LieutenantGeneral,       

        //[EnumName("Генерал-полковник")]
        //ColonelGeneral,          
    }
}
