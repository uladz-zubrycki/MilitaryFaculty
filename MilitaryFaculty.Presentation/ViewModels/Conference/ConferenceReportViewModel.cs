using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

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
               SetModelProperty(m => m.ConferenceReport.ThemeActuality, value);
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
               SetModelProperty(m => m.ConferenceReport.OrganizationCorrectness, value);
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
               SetModelProperty(m => m.ConferenceReport.ReportMaterials, value);
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
                SetModelProperty(m => m.ConferenceReport.ResultsUsage, value);
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceReportViewModel(Conference model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            Title = "Отчёт о конференции";

            var editCommands = new EditableViewBehaviour<Conference>(Do.Conference.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}