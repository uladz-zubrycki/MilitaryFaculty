using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ExhibitionViewModel : ComplexViewModel<Exhibition>
    {
        #region Class Properties

        public override string Title
        {
            get
            {
                return "Информация о выставке";
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ExhibitionViewModel(Exhibition model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            ViewModels.AddRange(new[]
                                {
                                    new ExhibitionInfoViewModel(Model, mode)
                                });
        }

        #endregion // Class Constructors
    }
}