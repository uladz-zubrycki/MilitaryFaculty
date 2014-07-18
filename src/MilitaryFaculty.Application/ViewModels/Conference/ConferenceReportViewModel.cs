using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ConferenceReportViewModel : EntityViewModel<Conference>
    {
        public ConferenceReportViewModel(Conference model)
            : base(model)
        {
           this.Editable(Do.ConferenceSave);
        }

        public override string Title
        {
            get { return "Отчёт о конференции"; }
        }

        [EnumProperty(Label = "Актуальность тематики:")]
        public AccordanceLevel ThemeActuality
        {
            get { return Model.ConferenceReport.ThemeActuality; }
            set { SetModelProperty(m => m.ConferenceReport.ThemeActuality, value); }
        }

        [EnumProperty(Label = "Правильность организации:")]
        public AccordanceLevel OrganizationCorrectness
        {
            get { return Model.ConferenceReport.OrganizationCorrectness; }
            set { SetModelProperty(m => m.ConferenceReport.OrganizationCorrectness, value); }
        }

        [EnumProperty(Label = "Наличие отчётных материалов:")]
        public AccordanceLevel ReportMaterials
        {
            get { return Model.ConferenceReport.ReportMaterials; }
            set { SetModelProperty(m => m.ConferenceReport.ReportMaterials, value); }
        }

        [EnumProperty(Label = "Внедрение результатов:")]
        public AccordanceLevel ResultsUsage
        {
            get { return Model.ConferenceReport.ResultsUsage; }
            set { SetModelProperty(m => m.ConferenceReport.ResultsUsage, value); }
        }
    }
}