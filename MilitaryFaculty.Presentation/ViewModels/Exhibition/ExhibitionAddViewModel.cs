using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ExhibitionAddViewModel : ComplexViewModel<Exhibition>
    {
        #region Class Properties

        public ExhibitionViewModel ExhibitionViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ExhibitionAddViewModel(Exhibition model)
            : base(model)
        {
            Title = "Добавить конференцию";

            ExhibitionViewModel = new ExhibitionViewModel(Model, EditableViewMode.Edit);
        }

        #endregion // Class Constructors
    }
}