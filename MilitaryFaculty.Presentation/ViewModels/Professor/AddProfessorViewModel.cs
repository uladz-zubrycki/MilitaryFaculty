using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class AddProfessorViewModel : AddEntityViewModel<Professor>
    {
        public override string Title
        {
            get { return "Добавить преподавателя:"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.Professor.Add; }
        }

        public AddProfessorViewModel(Professor model)
            : base(model)
        {
            // Empty
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