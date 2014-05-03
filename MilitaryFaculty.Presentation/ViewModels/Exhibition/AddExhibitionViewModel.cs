using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class AddExhibitionViewModel : AddEntityViewModel<Exhibition>
    {
        public AddExhibitionViewModel(Exhibition model)
            : base(model)
        {
            // Empty
        }

        public override string Title
        {
            get { return "Добавить научную выставку "; }
        }

        public override ICommand AddCommand
        {
            get { return Do.ExhibitionAdd; }
        }

        protected override IEnumerable<ViewModel<Exhibition>> GetViewModels()
        {
            return new[]
                   {
                       new ExhibitionInfoViewModel(Model)
                   };
        }
    }
}