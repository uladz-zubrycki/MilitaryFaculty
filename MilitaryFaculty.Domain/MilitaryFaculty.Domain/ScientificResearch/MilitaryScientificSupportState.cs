using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain.ScientificResearch
{
    public enum MilitaryScientificSupportState
    {
        [EnumName("Военно-научное сопровождение не проведено")] NotCompleted,

        [EnumName("Военно-научное сопровождение проведено")] Completed
    }
}