using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ScientificRequest
{
    public enum ScientificRequestType
    {
        [EnumName("Заявка на изобретение")] Invention,

        [EnumName("Заявка на полезную модель")] UtilityModel
    }
}