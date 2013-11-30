using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class ProfessorAddViewModel : EntityAddViewModel<Professor>
    {
        public override string Title
        {
            get { return "Добавить преподавателя:"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.Professor.Add; }
        }

        public ProfessorAddViewModel(Professor model)
            : base(model)
        {
            ViewModels.AddRange(new ViewModel<Professor>[]
                                {
                                    new ProfessorInfoViewModel(Model, EditableViewMode.Edit),
                                    new ProfessorExtraInfoViewModel(Model, EditableViewMode.Edit)
                                });
        }
    }
}