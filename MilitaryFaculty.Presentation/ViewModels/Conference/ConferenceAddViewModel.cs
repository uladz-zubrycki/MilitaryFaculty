using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceAddViewModel : ComplexViewModel<Conference>
    {
        #region Class Properties

        public ConferenceViewModel ConferenceViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceAddViewModel(Conference model)
            : base(model)
        {
            Title = "Добавить конференцию";

            ConferenceViewModel = new ConferenceViewModel(Model, EditableViewMode.Edit);
        }

        #endregion // Class Constructors
    }
}