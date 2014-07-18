using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class ConferenceReport : IImitator<ConferenceReport>
    {
        private AccordanceLevel _organizationCorrectness;
        private AccordanceLevel _reportMaterials;
        private AccordanceLevel _resultsUsage;
        private AccordanceLevel _themeActuality;

        /// <summary>
        ///     Evaluates conference theme actuality.
        /// </summary>
        public AccordanceLevel ThemeActuality
        {
            get { return _themeActuality; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _themeActuality = value;
            }
        }

        /// <summary>
        ///     Evaluates conference organization correctness.
        /// </summary>
        public AccordanceLevel OrganizationCorrectness
        {
            get { return _organizationCorrectness; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _organizationCorrectness = value;
            }
        }

        /// <summary>
        ///     Evaluates availability of conference report materials.
        /// </summary>
        public AccordanceLevel ReportMaterials
        {
            get { return _reportMaterials; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _reportMaterials = value;
            }
        }

        /// <summary>
        ///     Evaluates level of conference results adoption.
        /// </summary>
        public AccordanceLevel ResultsUsage
        {
            get { return _resultsUsage; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _resultsUsage = value;
            }
        }

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
}