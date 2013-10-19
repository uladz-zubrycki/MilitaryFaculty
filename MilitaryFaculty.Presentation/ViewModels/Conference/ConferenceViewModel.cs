using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceViewModel : ComplexViewModel<Conference>
    {
        #region Class Properties

        public ConferenceInfoViewModel InfoViewModel { get; private set; }
        public ConferenceReportViewModel ReportViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceViewModel(Conference model)
            : this(model, EditViewMode.Display)
        {
            // Empty
        }

        public ConferenceViewModel(Conference model, EditViewMode mode)
            : base(model)
        {
            Title = "Информация о конференции";
            InitViewModels(mode);
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected void InitViewModels(EditViewMode mode)
        {
            InfoViewModel = new ConferenceInfoViewModel(Model, mode);
            ReportViewModel = new ConferenceReportViewModel(Model, mode);

            ViewModels.Add(InfoViewModel);
            ViewModels.Add(ReportViewModel);
        }

        #endregion // Class Protected Methods
    }
}