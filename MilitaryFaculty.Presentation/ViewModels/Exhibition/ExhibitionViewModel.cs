using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ExhibitionViewModel : ComplexViewModel<Exhibition>
    {
        #region Class Properties

        public ExhibitionInfoViewModel InfoViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ExhibitionViewModel(Exhibition model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            Title = "Информация о выставке";
            InitViewModels(mode);
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected void InitViewModels(EditableViewMode mode)
        {
            InfoViewModel = new ExhibitionInfoViewModel(Model, mode);

            ViewModels.Add(InfoViewModel);
        }

        #endregion // Class Protected Methods
    }
}