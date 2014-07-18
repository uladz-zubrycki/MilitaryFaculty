using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal class AddProfessorViewModel : AddEntityViewModel<Domain.Professor>
    {
        public AddProfessorViewModel(Domain.Professor model)
            : base(model)
        {
            // Empty
        }

        public override string Title
        {
            get { return "Добавить преподавателя:"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.ProfessorAdd; }
        }

        protected override IEnumerable<ViewModel<Domain.Professor>> GetViewModels()
        {
            return new ViewModel<Domain.Professor>[]
                   {
                       new ProfessorInfoViewModel(Model),
                       new ProfessorExtraInfoViewModel(Model)
                   };
        }
    }
}