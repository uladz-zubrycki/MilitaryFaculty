using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceViewModel : ComplexViewModel<Conference>
    {
        #region Class Properties

        public override string Title
        {
            get
            {
                return "Информация о конференции";
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceViewModel(Conference model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            ViewModels.AddRange(new ViewModel<Conference>[]
                                {
                                    new ConferenceInfoViewModel(Model, mode),
                                    new ConferenceReportViewModel(Model, mode)
                                });
        }

        #endregion // Class Constructors
    }
}