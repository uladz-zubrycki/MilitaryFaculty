using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    /// <summary>
    ///     Evaluates some characteristic accordance to standart.
    /// </summary>
    [LocalizedEnum(typeof(EnumStrings))]
    public enum ConferenceAccordance
    {
        None,
        Partly,
        Fully,
    }

    public class ConferenceReport 
    {
        /// <summary>
        ///     Evaluates conference theme actuality.
        /// </summary>
        public ConferenceAccordance ThemeActuality { get; set; }
    
        /// <summary>
        ///     Evaluates conference organization correctness.
        /// </summary>
        public ConferenceAccordance OrganizationCorrectness { get; set; }

        /// <summary>
        ///     Evaluates availability of conference report materials.
        /// </summary>
        public ConferenceAccordance ReportMaterials { get; set; }

        /// <summary>
        ///     Evaluates level of conference results adoption.
        /// </summary>
        public ConferenceAccordance ResultsUsage { get; set; }
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Conference : UniqueEntity
    {
        public const int NameMaxLength = 200;

        public Conference()
        {
            Date = DateTime.Now;
            ConferenceReport = new ConferenceReport();
        }

        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Person Curator { get; set; }
        public virtual ConferenceReport ConferenceReport { get; set; }
        public virtual EventLevel EventLevel { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}