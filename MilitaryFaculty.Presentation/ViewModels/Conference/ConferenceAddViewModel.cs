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
            const string title = "Добавить конференцию";
            Title = title;

            ConferenceViewModel = new ConferenceViewModel(Model, EditViewMode.Edit);
        }

        #endregion // Class Constructors
    }
}