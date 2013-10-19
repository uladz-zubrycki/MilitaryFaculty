using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class ConferenceReport : IImitator<ConferenceReport>
    {
        #region Class Fields

        private AccordanceLevel themeActuality;
        private AccordanceLevel organizationCorrectness;
        private AccordanceLevel reportMaterials;
        private AccordanceLevel resultsUsage;

        #endregion // Class Fields

        #region Class Properties

        /// <summary>
        /// Evaluates conference theme actuality.
        /// </summary>
        public AccordanceLevel ThemeActuality
        {
            get { return themeActuality; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                themeActuality = value;
            }
        }

        /// <summary>
        /// Evaluates conference organization correctness.
        /// </summary>
        public AccordanceLevel OrganizationCorrectness
        {
            get { return organizationCorrectness; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                organizationCorrectness = value;
            }
        }

        /// <summary>
        /// Evaluates availability of conference report materials. 
        /// </summary>
        public AccordanceLevel ReportMaterials
        {
            get { return reportMaterials; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                reportMaterials = value;
            }
        }

        /// <summary>
        /// Evaluates level of conference results adoption.
        /// </summary>
        public AccordanceLevel ResultsUsage
        {
            get { return resultsUsage; }
            set
            {
                 if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                resultsUsage = value;
            }
        }

        #endregion // Class Properties

        #region Class Constructors

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

        #endregion // Class Constructors

        #region Implementation of IImitator<ConferenceReport>

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

        #endregion // Implementation of IImitator<ConferenceReport>
    }
}