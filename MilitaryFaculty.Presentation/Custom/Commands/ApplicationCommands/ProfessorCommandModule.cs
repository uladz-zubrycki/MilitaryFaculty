using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ProfessorCommandModule : EntityCommandModule<Professor>
    {
        public ProfessorCommandModule(IRepository<Professor> repository)
            : base(repository)
        {
            // Empty
        }

        protected override RoutedCommand AddCommand
        {
            get { return Do.ProfessorAdd; }
        }

        protected override RoutedCommand SaveCommand
        {
            get { return Do.ProfessorSave; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.ProfessorRemove; }
        }

        protected override string GetRemovalMessage()
        {
            return ("Вы действительно хотите удалить преподавателя? " +
                    "Все данные будут утеряны.");
        }
    }
}