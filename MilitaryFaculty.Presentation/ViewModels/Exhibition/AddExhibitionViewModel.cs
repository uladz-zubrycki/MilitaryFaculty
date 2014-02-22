using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class AddExhibitionViewModel : AddEntityViewModel<Exhibition>
    {
        public override string Title
        {
            get { return "Добавить научную выставку "; }
        }

        public override ICommand AddCommand
        {
            get { return Do.Exhibition.Add; }
        }

        public AddExhibitionViewModel(Exhibition model)
            : base(model)
        {
            // Empty
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