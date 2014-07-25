using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class EfficiencyProposal: UniqueEntity
    {
        public virtual string Text { get; set; }
        public virtual Professor Author { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual ApplicationStatus Status { get; set; }

        public EfficiencyProposal()
        {
            CreatedAt = DateTime.Now;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}