using System;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class EfficiencyProposal: UniqueEntity
    {
        public EfficiencyProposal()
        {
            CreatedAt = DateTime.Now;
        }

        public virtual string Text { get; set; }
        public virtual Person Author { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual ApplicationStatus Status { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}