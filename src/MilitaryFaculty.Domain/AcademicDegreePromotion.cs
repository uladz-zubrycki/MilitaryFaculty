using System;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class AcademicDegreePromotion : UniqueEntity
    {
        public AcademicDegreePromotion()
        {
            PromotedAt = DateTime.Now;
        }

        public virtual Person Professor { get; set; }
        public virtual DateTime PromotedAt { get; set; }
        public virtual AcademicDegree TargetAcademicDegree { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}