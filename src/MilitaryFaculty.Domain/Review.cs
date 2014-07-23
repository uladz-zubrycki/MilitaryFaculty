using System;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    public enum ReviewType
    {
        ExpertAdvice,
        SummaryFeedback
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Review: UniqueEntity
    {
        public string Name { get; set; }
        public Professor Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReviewType Type { get; set; }
        public AcademicRank TargetAcademicRank { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}
