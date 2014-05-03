using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class AddProfessorViewModel : AddEntityViewModel<Professor>
    {
        public AddProfessorViewModel(Professor model)
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

        protected override IEnumerable<ViewModel<Professor>> GetViewModels()
        {
            return new ViewModel<Professor>[]
                   {
                       new ProfessorInfoViewModel(Model),
                       new ProfessorExtraInfoViewModel(Model)
                   };
        }
    }
}