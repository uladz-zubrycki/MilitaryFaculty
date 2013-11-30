using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class ExhibitionAddViewModel : EntityAddViewModel<Exhibition>
    {
        #region Class Properties

        public ExhibitionViewModel ExhibitionViewModel { get; private set; }

        public override string Title
        {
            get
            {
                return "Добавить конференцию";
            }
        }

        public override ICommand AddCommand
        {
            get { return Do.Exhibition.Add; }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ExhibitionAddViewModel(Exhibition model)
            : base(model)
        {
            var exhibitionViewModel = new ExhibitionViewModel(Model, EditableViewMode.Edit);
            ViewModels.AddRange(exhibitionViewModel.ViewModels);
        }

        #endregion // Class Constructors
    }
}