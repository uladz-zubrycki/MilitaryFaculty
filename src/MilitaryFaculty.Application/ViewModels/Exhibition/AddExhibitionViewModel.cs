using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal class AddExhibitionViewModel : AddEntityViewModel<Domain.Exhibition>
    {
        public AddExhibitionViewModel(Domain.Exhibition model)
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

        protected override IEnumerable<ViewModel<Domain.Exhibition>> GetViewModels()
        {
            return new[]
                   {
                       new ExhibitionInfoViewModel(Model)
                   };
        }
    }
}