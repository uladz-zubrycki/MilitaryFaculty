using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorAddViewModel : ComplexViewModel<Professor>
    {
        #region Class Properties

        public ProfessorInfoViewModel InfoViewModel { get; private set; }
        public ProfessorExtraInfoViewModel ExtraInfoViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorAddViewModel(Professor model)
            : base(model)
        {
            const string title = "Добавить преподавателя:";

            this.Title = title;

            InitViewModels();
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected void InitViewModels()
        {
            InfoViewModel = new ProfessorInfoViewModel(Model, EditableViewMode.Edit);
            ExtraInfoViewModel = new ProfessorExtraInfoViewModel(Model, EditableViewMode.Edit);

            ViewModels.Add(InfoViewModel);
            ViewModels.Add(ExtraInfoViewModel);
        }

        #endregion // Class Protected Methods
    }
}