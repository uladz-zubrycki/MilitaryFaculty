using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class ConferenceAddViewModel : EntityAddViewModel<Conference>
    {
        #region Class Properties

        public override string Title
        {
            get
            {
                return "Добавить конференцию";
            }
        }

        public override ICommand AddCommand
        {
            get { return Do.Conference.Add; }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceAddViewModel(Conference model)
            : base(model)
        {
            var conferenceViewModel = new ConferenceViewModel(Model, EditableViewMode.Edit);
            ViewModels.AddRange(conferenceViewModel.ViewModels);
        }

        #endregion // Class Constructors
    }
}