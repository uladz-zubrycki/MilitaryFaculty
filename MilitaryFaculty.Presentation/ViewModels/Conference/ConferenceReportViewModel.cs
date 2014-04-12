using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.Attributes;
using MilitaryFaculty.Presentation.Core.ViewBehaviours;
using MilitaryFaculty.Presentation.Core.ViewModels.Entity;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceReportViewModel : EntityViewModel<Conference>
    {
        public override string Title
        {
            get { return "Отчёт о конференции"; }
        }

        /// <summary>
        ///     Evaluates conference theme actuality.
        /// </summary>
        [EnumProperty(Label = "Актуальность тематики:")]
        public AccordanceLevel ThemeActuality
        {
            get { return Model.ConferenceReport.ThemeActuality; }
            set { SetModelProperty(m => m.ConferenceReport.ThemeActuality, value); }
        }

        /// <summary>
        ///     Evaluates conference organization correctness.
        /// </summary>
        [EnumProperty(Label = "Правильность организации:")]
        public AccordanceLevel OrganizationCorrectness
        {
            get { return Model.ConferenceReport.OrganizationCorrectness; }
            set { SetModelProperty(m => m.ConferenceReport.OrganizationCorrectness, value); }
        }

        /// <summary>
        ///     Evaluates availability of conference report materials.
        /// </summary>
        [EnumProperty(Label = "Наличие отчётных материалов:")]
        public AccordanceLevel ReportMaterials
        {
            get { return Model.ConferenceReport.ReportMaterials; }
            set { SetModelProperty(m => m.ConferenceReport.ReportMaterials, value); }
        }

        /// <summary>
        ///     Evaluates level of conference results adoption.
        /// </summary>
        [EnumProperty(Label = "Внедрение результатов:")]
        public AccordanceLevel ResultsUsage
        {
            get { return Model.ConferenceReport.ResultsUsage; }
            set { SetModelProperty(m => m.ConferenceReport.ResultsUsage, value); }
        }

        public ConferenceReportViewModel(Conference model)
            : base(model)
        {
            // Empty
        }

        protected override IEnumerable<IViewBehaviour> GetBehaviours()
        {
            yield return new EditableViewBehaviour<Conference>(Do.Conference.Update);
        }
    }
}