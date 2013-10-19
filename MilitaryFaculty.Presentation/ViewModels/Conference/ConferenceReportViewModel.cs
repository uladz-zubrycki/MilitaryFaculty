using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Common.Extensions;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceReportViewModel : ViewModel<Conference>
    {
        #region Class Properties

        public IEnumerable<Tuple<AccordanceLevel, string>> AccordanceLevelList
        {
            get
            {
                return Enum.GetValues(typeof (AccordanceLevel))
                           .Cast<AccordanceLevel>()
                           .Select(val => new Tuple<AccordanceLevel, string>(val, val.GetName()));
            }
        }

        public string ThemeActualityString
        {
            get { return ThemeActuality.GetName(); }
        }

        /// <summary>
        /// Evaluates conference theme actuality.
        /// </summary>
        public AccordanceLevel ThemeActuality
        {
            get { return Model.ConferenceReport.ThemeActuality; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == ThemeActuality)
                {
                    return;
                }

                Model.ConferenceReport.ThemeActuality = value;
                OnPropertyChanged();
            }
        }

        public string OrganizationCorrectnessString
        {
            get { return OrganizationCorrectness.GetName(); }
        }

        /// <summary>
        /// Evaluates conference organization correctness.
        /// </summary>
        public AccordanceLevel OrganizationCorrectness
        {
            get { return Model.ConferenceReport.OrganizationCorrectness; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == OrganizationCorrectness)
                {
                    return;
                }

                Model.ConferenceReport.OrganizationCorrectness = value;
                OnPropertyChanged();
            }
        }

        public string ReportMaterialsString
        {
            get { return ReportMaterials.GetName(); }
        }

        /// <summary>
        /// Evaluates availability of conference report materials. 
        /// </summary>
        public AccordanceLevel ReportMaterials
        {
            get { return Model.ConferenceReport.ReportMaterials; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == ReportMaterials)
                {
                    return;
                }

                Model.ConferenceReport.ReportMaterials = value;
                OnPropertyChanged();
            }
        }

        public string ResultsUsageString
        {
            get { return ResultsUsage.GetName(); }
        }

        /// <summary>
        /// Evaluates level of conference results adoption.
        /// </summary>
        public AccordanceLevel ResultsUsage
        {
            get { return Model.ConferenceReport.ResultsUsage; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == ResultsUsage)
                {
                    return;
                }

                Model.ConferenceReport.ResultsUsage = value;
                OnPropertyChanged();
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceReportViewModel(Conference model, EditViewMode mode = EditViewMode.Display)
            : base(model)
        {
            const string title = "Отчёт о конференции";
           
            Title = title;

            var editCommands = new EditUICommandsPackage<Conference>(GlobalAppCommands.UpdateConference, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}