using System;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public class ProfessorHandlers : EntityCommandModule<Person>
    {
        public ProfessorHandlers(IRepository<Person> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage(Person professor)
        {
            return ("Вы действительно хотите удалить преподавателя? " +
                    "Все данные будут утеряны.");
        }
    }

    public class ProfessorNavigation : NavigationCommandModule
    {
        public ProfessorNavigation(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        public override void LoadModule(RoutedCommands commands)
        {
            commands.AddCommand<Cathedra>(GlobalCommands.BrowseAdd<Person>(), OnBrowseProfessorAdd);
        }

        private void OnBrowseProfessorAdd(Cathedra cathedra)
        {
            if (cathedra == null)
            {
                throw new ArgumentNullException("cathedra");
            }

            var model = new Person { Cathedra = cathedra };
            ViewModel.WorkWindow = new ProfessorView.Add(model);
        }
    }
}