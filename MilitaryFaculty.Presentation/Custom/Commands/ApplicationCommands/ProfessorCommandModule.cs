using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ProfessorCommandModule : EntityCommandModule<Professor>
    {
        protected override RoutedCommand AddCommand
        {
            get { return Do.Professor.Add; }
        }

        protected override RoutedCommand UpdateCommand
        {
            get { return Do.Professor.Update; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.Professor.Remove; }
        }

        public ProfessorCommandModule(IRepository<Professor> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage()
        {
            return "Вы действительно хотите удалить преподавателя? Все данные будут утеряны.";
        }
    }
}