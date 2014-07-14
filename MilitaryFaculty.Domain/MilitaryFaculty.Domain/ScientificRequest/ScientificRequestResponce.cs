using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ScientificRequest
{
    public enum ScientificRequestResponce
    {
        [EnumName("Без ответа")] Unanswered,

        [EnumName("С положительным ответом")] PositiveResponse,

        [EnumName("С отрицательным ответом")] NegativeResponse
    }
}