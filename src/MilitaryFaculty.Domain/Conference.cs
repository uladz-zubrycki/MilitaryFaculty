using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    /// <summary>
    ///     Evaluates some characteristic accordance to standart.
    /// </summary>
    public enum AccordanceLevel : byte
    {
        [EnumName("Не соответствует")]
        None = 0,

        [EnumName("Частично соответствует")]
        Partly = 1,

        [EnumName("Полностью соответствует")]
        Fully = 2,
    }

    public class ConferenceReport : IImitator<ConferenceReport>
    {
        /// <summary>
        ///     Evaluates conference theme actuality.
        /// </summary>
        public AccordanceLevel ThemeActuality { get; set; }
    
        /// <summary>
        ///     Evaluates conference organization correctness.
        /// </summary>
        public AccordanceLevel OrganizationCorrectness { get; set; }

        /// <summary>
        ///     Evaluates availability of conference report materials.
        /// </summary>
        public AccordanceLevel ReportMaterials { get; set; }

        /// <summary>
        ///     Evaluates level of conference results adoption.
        /// </summary>
        public AccordanceLevel ResultsUsage { get; set; }

        public ConferenceReport()
        {
            ThemeActuality = AccordanceLevel.None;
            OrganizationCorrectness = AccordanceLevel.None;
            ReportMaterials = AccordanceLevel.None;
            ResultsUsage = AccordanceLevel.None;
        }

        public ConferenceReport(ConferenceReport other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(ConferenceReport other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            ThemeActuality = other.ThemeActuality;
            OrganizationCorrectness = other.OrganizationCorrectness;
            ReportMaterials = other.ReportMaterials;
            ResultsUsage = other.ResultsUsage;
        }
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Conference : UniqueEntity, IImitator<Conference>
    {
        public const int NameMaxLength = 200;

        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Professor Curator { get; set; }
        public virtual ConferenceReport ConferenceReport { get; set; }
        public virtual EventLevel EventLevel { get; set; }

        public Conference()
        {
            Name = String.Empty;
            ConferenceReport = new ConferenceReport();
            Date = DateTime.Now;
        }

        public Conference(Conference other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(Conference other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
            Date = other.Date;
            Curator = other.Curator;
            EventLevel = other.EventLevel;
            ConferenceReport.Imitate(other.ConferenceReport);
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}