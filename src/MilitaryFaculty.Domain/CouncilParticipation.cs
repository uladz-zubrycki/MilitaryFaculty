using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof (EnumStrings))]
    public enum CouncilType
    {
        SupremeCertificationCommissionCouncil,
        DefenceOfDissertationsCouncil,
        ResearchCounsil,
        EditorialBoardCouncil
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class CouncilParticipation : UniqueEntity
    {
        public virtual string Name { get; set; }
        public virtual Professor Participant { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual CouncilType Type { get; set; }

        public CouncilParticipation()
        {
            Start = DateTime.Now;
        }
    }

    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}