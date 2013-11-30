﻿using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceReportViewModel : ViewModel<Conference>
    {
        #region Class Properties

        public override string Title
        {
            get
            {
                return "Отчёт о конференции";
            }
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
            var editCommands = new EditableViewBehaviour<Conference>(Do.Conference.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}